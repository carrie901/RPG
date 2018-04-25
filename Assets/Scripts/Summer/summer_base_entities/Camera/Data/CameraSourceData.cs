using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    [System.Serializable]
    public struct CameraSourceData
    {
        [Tooltip("相机的位置偏移")]
        public Vector3 _offset;
        [Tooltip("相机的角度大小")]
        public Vector3 _rotaion;
       
        public static CameraSourceData Lerp(CameraSourceData from, CameraSourceData to, float dt)
        {
            CameraSourceData d;

            d._rotaion.x = Mathf.LerpAngle(from._rotaion.x, to._rotaion.x, dt);
            d._rotaion.y = Mathf.LerpAngle(from._rotaion.y, to._rotaion.y, dt);
            d._rotaion.z = Mathf.LerpAngle(from._rotaion.z, to._rotaion.z, dt);

            d._offset = Vector3.Lerp(from._offset, to._offset, dt);
            return d;
        }
    }
}

