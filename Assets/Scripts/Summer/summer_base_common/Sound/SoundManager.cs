using System;
using UnityEngine;
using System.Collections.Generic;
using Summer;

/// <summary>
/// 需求解析
/// 1.控制音乐的暂停
/// 2.控制音乐的音量 音量大小
/// 3.音乐(BGM)和音效分开
/// 4.重复播放某一个音乐
/// 5.不可重复播放某一个音乐
/// 6.2D音乐和3D人物音乐
/// 7.超大型的加载
/// 8.声音的淡入淡出
/// 9.环境音
/// 10. 声音的格式
/// 
/// 解决方案：
/// 1.对象池
/// 2.淡入淡出数据类
/// 3.音量大小/控制，暂停,类型，是否可重复播放()
/// 4.加载
/// 
/// 
/// TODO BUG 需要对数据AudioUnit中的UISound和SoundObj 进行数据的剥离，不然这就不是这个独立的模块，无法单独的拿出来使用
/// </summary>
public class SoundManager : MonoBehaviour
{
    #region 静态变量

    public const string INVALID_SOUND = "-1";

    public static SoundManager instance = null;

    #endregion

    #region Csv表格数据

    public Dictionary<int, E_GameResType> _sound_type
        = new Dictionary<int, E_GameResType>();                                     // Key = （BGM,Voice,Sound）
    private Dictionary<int, SoundCnf> _sound_map_id;                                // 原始数据 csv表格声音数据 Key=sound_Id，value=声音信息
    private Dictionary<string, SoundCnf> _sound_map_name;                           // 根据原始数据进行转换 csv表格声音数据 Key=sound_name，value=声音信息

    #endregion

    #region Bgm数据

    public int _prev_bgm = -1;
    public AudioUnit _bgm_unit;                             // Bgm 唯一

    #endregion

    #region 本地保存变量 PlayerPrefs 声音的开关和音量

    public const string KEY_SOUND_VOLUME = "KEY_SOUND_VOLUME";
    public const string KEY_BGM_VOLUME = "KEY_BGM_VOLUME";

    public float SoundVolume { get; private set; }
    public float BgmVolume { get; private set; }

    #endregion


    public GameObject sound_parent;
    public AudioUnit prefab_audio;

    protected PoolBaseSimpleObject _pool_factory;
    public List<AudioUnit> _list = new List<AudioUnit>();

    #region MONO

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Debug.LogError("多个SoundManager");
        }
        I_ObjectFactory factory = new SoundGameObjectFactory("SoundManager", prefab_audio.gameObject);
        _pool_factory = new PoolBaseSimpleObject(factory);
        _init_csv_sound();
        ButtonSoundManager.Instance.Init();
        _read_audio_setting();
    }

    void Update()
    {
        float dt = Time.deltaTime;
        _bgm_unit.OnUpdate(dt);
        int length = _list.Count;
        for (int i = length - 1; i >= 0; i--)
        {
            AudioUnit unit = _list[i];
            if (unit == null)
            {
                LogManager.Error("其他地方销毁了这个音乐");
                continue;
            }
            bool result = unit.OnUpdate(dt);

            if (result)
            {
                _list.Remove(unit);
                _pool_factory.Push(unit);
            }
        }
    }

    #endregion

    #region public



    public void ResetSceneBgm()
    {
        if (_prev_bgm != -1)
            PlayBgm(_prev_bgm);
    }

    public static E_GameResType GetResType(int type)
    {
        E_GameResType res_type = E_GameResType.MUSIC_SOUND;
        if (instance == null) return res_type;
        instance._sound_type.TryGetValue(type, out res_type);
        return res_type;
    }


    #endregion

    #region Play

    /// <summary>
    /// TODO 零时增加了一个接口，为了零时
    /// </summary>
    public AudioUnit Play(int id, int sound_max = 100, float fade_in = 0f, float fade_out = 0f)
    {

        if (id == -1) return null;
        SoundCnf info = _find_sound_by_id(id);
        if (info == null || info.name == INVALID_SOUND) return null;
        Debug.Log(info.name);
        // TODO 是有问题的，多个重复的声音的情况下，得到的会是同一个
        if (sound_max == 1 && _contains(id) >= 0)
        {
            int index = _contains(id);
            return _list[index];
        }
        else if (sound_max == 100) //不做如何处理
        {

        }
        else // 计算最大数量 
        {
            int count = _contains_count(id);
            if (count > sound_max) //TODO 吊柜的逻辑哦
            {
                return null;
            }
        }
        return _internal_play(info, fade_in, fade_out);
    }

    public AudioUnit Play(string sound_name)
    {
        SoundCnf info = _find_sound_by_name(sound_name);
        if (info == null || info.name == INVALID_SOUND) return null;

        return _internal_play(info, 0f, 0f);
    }

    public AudioUnit Play3D(int id, Transform follow_target)
    {
        AudioUnit unit = Play(id);
        if (unit != null)
            unit.FollowTarget(follow_target);
        return unit;
    }

    public void PlayBgm(int id, bool is_scene = false, float fade_in = 1.5f, float fade_out = 1.5f)
    {
        if (id == -1) return;
        if (id == _prev_bgm) return;
        SoundCnf sound_cnf = _find_sound_by_id(id);
        if (sound_cnf == null) return;
        _bgm_unit.Play(sound_cnf, BgmVolume, fade_in, fade_out);
        if (is_scene)
            _prev_bgm = id;
    }

    #endregion

    #region VolumeChange

    public void OnSoundVolumeChange()
    {

    }

    public void OnBgmVolumeChange()
    {

    }

    #endregion

    #region 初始化数据
    public void _init_csv_sound()
    {
        _sound_map_id = StaticCnf.FindMap<SoundCnf>();
        _sound_map_name = new Dictionary<string, SoundCnf>();

        foreach (var info in _sound_map_id)
        {
            if (_sound_map_name.ContainsKey(info.Value.name))
                continue;
            _sound_map_name.Add(info.Value.name, info.Value);
        }

        _sound_type.Add(1, E_GameResType.MUSIC_BGM);
        _sound_type.Add(3, E_GameResType.MUSIC_SOUND);
        _sound_type.Add(2, E_GameResType.MUSIC_VOICE);
    }


    // 根据Id查找声音的信息
    public SoundCnf _find_sound_by_id(int id)
    {
        if (_sound_map_id.ContainsKey(id))
            return _sound_map_id[id];
        LogManager.Error("找不到对应的声音.ID：[{0}]", id);
        return null;
    }

    // 根据名字查找声音的信息
    public SoundCnf _find_sound_by_name(string sound_name)
    {
        if (_sound_map_name.ContainsKey(sound_name))
            return _sound_map_name[sound_name];
        //LogManager.Error("找不到对应的声音.Name：[{0}]", id);
        return null;
    }

    #endregion

    #region private 
    public int _contains(int id)
    {
        int index = -1;
        int length = _list.Count;
        for (int i = 0; i < length; i++)
        {
            if (_list[i].Id == id)
                return i;
        }
        return index;
    }

    public int _contains_count(int id)
    {
        int count = 0;
        int length = _list.Count;
        for (int i = 0; i < length; i++)
        {
            if (_list[i].Id == id)
                count++;
        }
        return count;
    }

    public AudioUnit _internal_play(SoundCnf info, float fade_in, float fade_out)
    {
        I_PoolObjectAbility object_ability = _pool_factory.Pop();
        AudioUnit unit = object_ability as AudioUnit;
        if (unit != null)
        {
            _list.Add(unit);
            unit.Play(info, SoundVolume, fade_in, fade_out);
        }
        else
        {
            LogManager.Error("_internal_play Error");
        }

        return unit;
    }

    /// <summary>
    /// 读取音频数据
    /// </summary>
    public void _read_audio_setting()
    {
        SoundVolume = PlayerPrefs.GetFloat(KEY_SOUND_VOLUME, 1.0f);
        BgmVolume = PlayerPrefs.GetFloat(KEY_BGM_VOLUME, 1.0f);
        // BgmVolume = 0.0f;
    }

    /// <summary>
    /// 保存音频数据
    /// </summary>
    public void _save_audio_setting()
    {
        PlayerPrefs.SetFloat(KEY_SOUND_VOLUME, SoundVolume);
        PlayerPrefs.SetFloat(KEY_BGM_VOLUME, BgmVolume);
    }

    #endregion
}


