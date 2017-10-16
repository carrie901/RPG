using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 相机的最终数据
    /// </summary>
    public struct CameraData
    {
        public Quaternion _rot;
        public Vector3 _pos;
        public float _fov;
        //public float _aspect;

        public void CopyData(CameraData data)
        {
            _rot = data._rot;
            _pos = data._pos;
            _fov = data._fov;
            //ignore aspect
        }
    }
}

