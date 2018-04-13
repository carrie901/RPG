namespace Summer
{
    public class PoolVfxObject : PoolDefaultGameObject
    {
        #region 属性

        public TimeInterval _interval = new TimeInterval(1f);

        #endregion

        #region MONO

        void Update()
        {
            bool result = _interval.OnUpdate();
            if (result)
            {
                TransformPool.Instance.Push(this);
            }
        }

        #endregion

        // 开始自动回收时间
        public void SetLifeTime(float time)
        {
            _interval.Reset(time);
        }

        #region

        #endregion

        #region override I_PoolObjectAbility

        public override void OnInit()
        {
            base.OnInit();
            GameObjectHelper.SetActive(gameObject, false);
        }

        public override void OnPop()
        {
            base.OnPop();
            _interval.Pause();
        }

        public override void OnPush()
        {
            base.OnPush();
            _interval.Pause();
        }

        #endregion
    }
}

