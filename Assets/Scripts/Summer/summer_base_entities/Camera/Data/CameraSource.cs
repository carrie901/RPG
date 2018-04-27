using UnityEngine;

namespace Summer
{
    //根据CameraSource 产生CameraData的数据的方法放在外面
    [System.Serializable]
    public class CameraSource
    {
        public CameraSource()
        {
            _data._offset.z = -5;
            _data._rotaion = new Vector3(30, -90, 0);
        }
        public E_CameraSourceType _type;
        public CameraSourceData _data;

        public float _timer = 1;                                // 过度时间                 
        public int _priority = 0;                               // 优先级别
    }
}