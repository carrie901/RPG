using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class FollowCam : MonoBehaviour
    {
        [Header("要追踪的游戏对象的Transform的变量")]
        public Transform targetTr;//要追踪的游戏对象的Transform的变量

        [Header("与摄像机之间的距离")]
        public float dist = 10.0f;//与摄像机之间的距离

        [Header("设置摄像机高度")]
        public float height = 3.0f;//设置摄像机高度

        [Header("实现平滑追踪的变量")]
        public float dampTrace = 20.0f;//实现平滑追踪的变量


        private Transform tr;//摄像机本身的Transfrom变量

        void Start()
        {
            tr = GetComponent<Transform>();//将摄像机本身的Transform组件分配至tr
        }


        void LateUpdate()
        {
            //将摄像机放置 在被追踪目标后方的dist距离的位置
            //将摄像机向上抬高height
            tr.position = Vector3.Lerp(tr.position, targetTr.position - (targetTr.forward * dist) + (Vector3.up * height), Time.deltaTime * dampTrace);

            //是摄像机朝向游戏对象
            tr.LookAt(targetTr.position);
        }
    }
}