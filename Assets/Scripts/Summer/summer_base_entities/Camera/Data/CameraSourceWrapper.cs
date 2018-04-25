namespace Summer
{
    /// <summary>
    /// 镜头移动数据的相关包装
    /// 1.镜头目标数据
    /// 2.镜头移动到目标地点的相关速度
    /// </summary>
    [System.Serializable]
    public class CameraSourceWrapper
    {
        public CameraSource _source;
        public CameraSourceLerp _default_source_lerp;

        public CameraSourceWrapper(CameraSource source)
        {
            _source = source;
        }

        public virtual void SetDefaultSourceLerp(CameraSourceLerp lerp)
        {
            _default_source_lerp = lerp;
        }

        public virtual CameraSourceData GetData(float dt, BaseEntity player, I_Transform target, ref CameraSourceData now_data, float fov)
        {
            return _source._data;
        }
    }


    public class Cs_NewFollowTargetWrapper : CameraSourceWrapper
    {
        public Cs_NewFollowTargetWrapper(CameraSource source) : base(source)
        {
        }
    }


    public class CameraSourceWrapperFactory
    {
        public static CameraSourceWrapper Create(CameraSource source)
        {
            CameraSourceWrapper ret_val = null;
            switch (source._type)
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
            }
            return ret_val;
        }
    }
}