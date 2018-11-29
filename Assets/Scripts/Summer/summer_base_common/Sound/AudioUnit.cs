using UnityEngine;

namespace Summer
{
    /// <summary>
    /// TODO 把数据和表现分离开启
    /// </summary>
    public class AudioUnit : PoolDefaultGameObject
    {
        #region 属性 

        public string _soundName;                                               // 声音的名字
        public AudioSource _audioSource;                                        // 音频源
        public Transform _trans;                                                // 自身的坐标

        #endregion

        #region private 

        protected int _id;                                                      // 声音的ID
        protected bool _pause;                                                  // 暂停
        protected SoundCnf _soundInfo;                                          // 声音的table数据
        protected float _length;
        protected Transform _followTarget;                                      // 跟随目标
        protected bool _isFollowTarget;                                         // 是否跟随目标
        protected float _volumeRate;                                            // 音量整体比例
        protected float _currentVolume;                                         // 当前音量大小 
        //private float _real_volume;                                           // 实际的音量=当前音量*音量整体比例

        protected FadeEffect _effect = new FadeEffect();

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
            float rate = _effect.OnUpdate(dt);
            // 2.设置音量
            _set_volume(rate);
            // 3.跟随目标
            if (_isFollowTarget && _followTarget != null)
            {
                _trans.position = _followTarget.position;
            }
            if (_audioSource.isPlaying) return false;
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
            if (_audioSource != null)
                _audioSource.Stop();
        }

        public void Play(SoundCnf info, float volumeRate = 1f, float fadeIn = 0f, float fadeOut = 0f)
        {
            LogManager.Assert(_audioSource != null, "声音缺少Source组件");
            if (_audioSource == null) return;

            // 1.初始化数据
            _soundInfo = info;
            _volumeRate = volumeRate;
            // 3.初始化数据
            _init_sound_info();

            // 2.设置淡入淡出效果
            //effect.Set(_length, fade_in, fade_out);
            //float rate = effect.OnUpdate(0.01f);
            //_set_volume(rate);

            // 播放
            _internal_play();
        }

        public void FollowTarget(Transform followTarget)
        {
            _isFollowTarget = true;
            _followTarget = followTarget;
        }

        #endregion

        #region private 

        // 初始化音效的参数
        private void _init_sound_info()
        {
            /*if (_sound_info == null) return;
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
            }*/
        }

        // 清除跟随目标
        private void _clear_follow_target()
        {
            _isFollowTarget = false;
            _followTarget = null;
        }

        // 内部播放
        private void _internal_play()
        {
            if (_is_valid()) return;

            _audioSource.Play();
        }

        private void _set_volume(float volume)
        {
            if (MathHelper.IsEqual(volume, _currentVolume)) return;
            _currentVolume = volume;
            _audioSource.volume = Mathf.Clamp01(_currentVolume * _volumeRate);
        }

        private bool _is_valid() { return _audioSource == null || _audioSource.clip == null; }

        #endregion

        #region PoolAbility

        public override void OnInit()
        {
            Pause = false;
            _trans = gameObject.transform;
            base.OnInit();
        }

        public override void OnRecycled()
        {
            base.OnRecycled();
            GameObjectHelper.DestroySelf(gameObject);
        }

        public override void OnPop()
        {
            Pause = false;
            base.OnPop();
        }

        public override void OnPush()
        {
            base.OnPush();
            if (_audioSource.clip != null)
            {
                _audioSource.clip = null;
            }
            Pause = false;

            _clear_follow_target();
            _effect.Reset();
            _length = -1;
            _soundName = string.Empty;
            _currentVolume = 0f;
            _volumeRate = 0f;
            _soundInfo = null;
            _length = -1;
        }

        #endregion
    }
}

