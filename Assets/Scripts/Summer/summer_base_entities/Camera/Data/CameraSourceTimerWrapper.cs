namespace Summer
{
    /// <summary>
    /// 镜头移动数据的相关包装
    /// 1.镜头目标数据
    /// 2.镜头移动到目标地点的相关速度
    /// 3.以及如何绕过去
    /// </summary>
    [System.Serializable]
    public class CameraSourceTimerWrapper
    {
        public CameraSource _target;                                // 镜头源数据
        public CameraSourceLerpNew _default_source_lerp;            // 过渡方式 是直线过度，还是曲线过去
        public float _timer;                                        // 当前时间流逝
        public float _blend_priority;                               // 混合的权重

        public CameraSourceTimerWrapper(CameraSource target)
        {
            _target = target;
        }

        public CameraSourceTimerWrapper(CameraSource target, CameraSourceLerpNew lerp)
        {
            _target = target;
            _default_source_lerp = lerp;
        }

        public void SetDefaultSourceLerp(CameraSourceLerpNew source_lerp)
        {
            _default_source_lerp = source_lerp;
        }

        public CameraSourceData GetData(CameraSourceData from, float dt)
        {
            if (_default_source_lerp == null)
            {
                return CameraSourceLerpNew.Lerp(from, _target._data, dt);
            }
            return _default_source_lerp.CameraLerp(from, _target._data, dt);
        }

        public void OnUpdate(float dt)
        {
            _timer += dt;
            _blend_priority = _cal_progress();
        }

        public void OnReset(CameraSource target)
        {
            _target = target;
            OnReset();
        }

        public void OnReset()
        {
            _timer = 0;
            _blend_priority = 0;
        }

        public bool IsEnd()
        {
            return _blend_priority >= 1;
        }

        public float _cal_progress()
        {
            float progress = 0;
            if (_target._timer < 0.001f) //防止策划填的数值为0，然后dt时间为0，比如暂停，导致出问题
            {
                progress = 1;
            }
            else if (_timer > _target._timer)
            {
                progress = 1;
            }
            else
            {
                progress = _timer / _target._timer;

                if (progress >= 1)
                    progress = 1;
                else if (progress < 0)
                    progress = 0;
            }
            return progress;
        }
    }


    /*public class Cs_NewFollowTargetWrapper : CameraSourceWrapper
    {
        public Cs_NewFollowTargetWrapper(CameraSource source) : base(source)
        {
        }
    }*/


    public class CameraSourceWrapperFactory
    {
        public static CameraSourceLerpNew _default_camera_lerp = new CameraSourceLerpNew();
        public static CameraSourceTimerWrapper Create(CameraSource source)
        {
            CameraSourceTimerWrapper ret_val = null;
            ret_val = new CameraSourceTimerWrapper(source, _default_camera_lerp);
            /*switch (source._type)
            {
                case E_CameraSourceType.follow_simple:
                    ret_val = new CameraSourceWrapper(source);
                    break;

                case E_CameraSourceType.follow_target:
                    ret_val = new Cs_NewFollowTargetWrapper(source);
                    break;
                case E_CameraSourceType.follow_target_once:
                    ret_val = new Cs_NewFollowTargetWrapper(source);
                    break;

                default:
                    ret_val = new CameraSourceWrapper(source);
                    break;
            }*/
            return ret_val;
        }
    }
}