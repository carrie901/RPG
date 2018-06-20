using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 镜头源 一个纯粹的镜头源数据
    /// </summary>
    [System.Serializable]
    public class CameraSource
    {
        public CameraSource()
        {
            _data._offset.z = -5;
            _data._rotaion = new Vector3(30, -90, 0);
        }
        //public E_CameraSourceType _type = E_CameraSourceType.follow_simple;
        public CameraSourceData _data;
        public float _timer = 1;                                // 过度时间                
        //public int _priority = 0;                               // 优先级别
    }
}