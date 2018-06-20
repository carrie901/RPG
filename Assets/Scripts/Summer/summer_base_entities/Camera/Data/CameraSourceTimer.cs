namespace Summer
{
    /// <summary>
    /// 镜头的相关的权重信息和时间
    /// </summary>
    /*public class CameraSourceTimer
    {
        public CameraSourceWrapper _source;
        public float _timer = 0;
        public float _blend_priority = 0; //混合的权重

        public void OnUpdate(float dt)
        {
            _timer += dt;
            _blend_priority = _cal_progress();
        }

        public void OnReset(CameraSourceWrapper wrapper)
        {
            _source = wrapper;
            OnReset();
        }

        public void OnReset()
        {
            _timer = 0;
            _blend_priority = 0;
        }

        public bool IsEnd()
        {
            return _blend_priority >= 1;
        }

        public float _cal_progress()
        {
            float progress = 0;
            if (_source._source._timer < 0.001f) //防止策划填的数值为0，然后dt时间为0，比如暂停，导致出问题
            {
                progress = 1;
            }
            else if (_timer > _source._source._timer)
            {
                progress = 1;
            }
            else
            {
                progress = _timer / _source._source._timer;

                if (progress >= 1)
                    progress = 1;
                else if (progress < 0)
                    progress = 0;
            }
            return progress;
        }
    }*/
}
