namespace Summer
{
    /// <summary>
    /// 镜头的相关的权重信息和时间
    /// </summary>
    public class CameraSourceTimer
    {
        public CameraSourceWrapper _source;
        public float _timer = 0;
        public float _blend_priority = 0; //混合的权重

        public void OnUpdate(float dt)
        {
            _timer += dt;
        }
    }
}
