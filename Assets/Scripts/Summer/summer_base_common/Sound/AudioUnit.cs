using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// TODO 把数据和表现分离开启
    /// </summary>
    public class AudioUnit : PoolDefaultGameObject
    {
        #region public 

        public string sound_name;                                               // 声音的名字
        public AudioSource audio_source;                                        // 音频源
        public Transform trans;                                                 // 自身的坐标

        #endregion

        #region private 

        protected int _id;                                                      // 声音的ID
        protected bool _pause;                                                  // 暂停
        protected SoundCnf _sound_info;                                         // 声音的table数据
        protected float _length;
        protected Transform _follow_target;                                     // 跟随目标
        protected bool _is_follow_target;                                       // 是否跟随目标
        protected float _volume_rate;                                           // 音量整体比例
        protected float _current_volume;                                        // 当前音量大小 
        //private float _real_volume;                                           // 实际的音量=当前音量*音量整体比例

        protected FadeEffect effect = new FadeEffect();

        #endregion

        #region Get/Set

        public float Length { get { return _length; } }                         // 音乐长度
        public bool Pause { get { return _pause; } set { _pause = value; } }
        public int Id { get { return _id; } set { _id = value; } }

        #endregion

        #region public

        public bool OnUpdate(float dt)
        {
            if (_pause || _is_valid()) return false;

            // 淡入淡出效果，没有的情况下=1
            float rate = effect.OnUpdate(dt);
            // 2.设置音量
            _set_volume(rate);
            // 3.跟随目标
            if (_is_follow_target && _follow_target != null)
            {
                trans.position = _follow_target.position;
            }
            if (audio_source.isPlaying) return false;
            return true;
        }

        // 重新播放
        public void RePlay()
        {
            Pause = false;
            _internal_play();
        }

        // 停止=移除了音乐
        public void Stop()
        {
            Pause = true;
            if (audio_source != null)
                audio_source.Stop();
        }

        public void Play(SoundCnf info, float volume_rate = 1f, float fade_in = 0f, float fade_out = 0f)
        {

            if (audio_source == null)
            {
                LogManager.Error("声音缺少Source组件");
                return;
            }
            // 1.初始化数据
            _sound_info = info;
            _volume_rate = volume_rate;
            // 3.初始化数据
            _init_sound_info();

            // 2.设置淡入淡出效果
            //effect.Set(_length, fade_in, fade_out);
            //float rate = effect.OnUpdate(0.01f);
            //_set_volume(rate);

            // 播放
            _internal_play();

        }

        public void FollowTarget(Transform follow_target)
        {
            _is_follow_target = true;
            _follow_target = follow_target;
        }

        #endregion

        #region private 

        // 初始化音效的参数
        public void _init_sound_info()
        {
            if (_sound_info == null) return;
            _length = -1;
            sound_name = _sound_info.name;
            Id = _sound_info.ID;

            E_GameResType res_type = SoundManager.GetResType(_sound_info.type);
            ResManager.instance.LoadAudio(audio_source, _sound_info.name, res_type);
            if (audio_source.clip != null)
            {
                _length = audio_source.clip.length;
            }
            else
            {
                LogManager.Error("加载音效失败:[{0}]", _sound_info.name);
            }
        }

        // 清除跟随目标
        public void _clear_follow_target()
        {
            _is_follow_target = false;
            _follow_target = null;
        }

        // 内部播放
        public void _internal_play()
        {
            if (_is_valid()) return;

            audio_source.Play();
        }

        public void _set_volume(float volume)
        {
            if (MathHelper.IsEqual(volume, _current_volume)) return;
            _current_volume = volume;
            audio_source.volume = Mathf.Clamp01(_current_volume * _volume_rate);
        }

        public bool _is_valid() { return audio_source == null || audio_source.clip == null; }

        #endregion

        #region PoolAbility

        public override void OnInit()
        {
            Pause = false;
            base.OnInit();
        }

        public override void OnRecycled() { base.OnRecycled(); }

        public override void OnPop()
        {
            Pause = false;
            base.OnPop();
        }

        public override void OnPush()
        {
            base.OnPush();
            if (audio_source.clip != null)
            {
                audio_source.clip = null;
            }
            Pause = false;

            _clear_follow_target();
            effect.Reset();
            _length = -1;
            sound_name = string.Empty;
            _current_volume = 0f;
            _volume_rate = 0f;
            _sound_info = null;
            _length = -1;
        }

        #endregion
    }
}

