using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 镜头移动的相关速度
    /// </summary>
    [System.Serializable]
    public class CameraSourceLerp
    {
        [Header("rotaion移动的速度")]
        public float _rot_speed = 2;

        [Header("rotaion快速移动的速度")]
        public float _fast_rot_speed = 1;

        [Header("rotaion慢速移动的速度")]
        public float _slow_rot_speed = 0.4f;

        [Header("offset的移动速度")]
        public float _offset_speed = 1;

    }
}