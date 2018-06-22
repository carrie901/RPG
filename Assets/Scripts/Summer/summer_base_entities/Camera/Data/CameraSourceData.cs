using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 镜头的位置和角度
    ///     1.偏移量
    ///     2.角度
    /// 相对人物的数据
    /// </summary>
    [System.Serializable]
    public struct CameraSourceData
    {

        //[Header("与摄像机之间的距离")]
        //public float dist = 10.0f;//与摄像机之间的距离  
        //[Header("设置摄像机高度")]
        //public float height = 3.0f;//设置摄像机高度  
        [Header("相机的位置偏移")]
        public Vector3 _offset;
        [Header("相机的角度大小")]
        public Vector3 _rotaion;
    }
}

