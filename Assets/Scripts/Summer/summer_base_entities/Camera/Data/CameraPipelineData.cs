
namespace Summer
{
    /// <summary>
    /// 相机数据在通道之中的过度
    /// </summary>
    public class CameraPipelineData
    {
        /// <summary>
        /// 上一次产生的最终数据
        /// </summary>
        public CameraData _now_data;

        /// <summary>
        /// 上一次的产生的数据，大部分情况下和 _now_data相同
        /// 因为位置的计算，有些数据 不需要反馈到下一帧，比如抖动产生的数据
        /// 在计算follow的时候，判断当前的相机是不是符合safezone的时候，不需要把抖动的信息反馈给下一帧的follow
        /// </summary>
        public CameraData _now_data_witout_shake;

        /// <summary>
        /// 最终数据
        /// </summary>
        public CameraData _dest_data;

        /// <summary>
        /// 抖动数据
        /// </summary>
        public CameraData _dest_data_without_shake;


        public void OnUpdate()
        {
            _now_data = _dest_data;
            _now_data_witout_shake = _dest_data_without_shake;
        }

    }
}

