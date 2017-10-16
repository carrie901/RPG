using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class CameraPipelineData : MonoBehaviour
    {

        //上一次产生的最终数据
        public CameraData _now_data;

        //	上一次的产生的数据，大部分情况下和 _now_data相同
        //	因为位置的计算，有些数据 不需要反馈到下一帧，比如抖动产生的数据
        //	在计算follow的时候，判断当前的相机是不是符合safezone的时候，不需要把抖动的信息反馈给下一帧的follow
        public CameraData _now_data_witout_shake;

        //最终数据
        public CameraData _dest_data;
        public CameraData _dest_data_without_shake;
    }
}

