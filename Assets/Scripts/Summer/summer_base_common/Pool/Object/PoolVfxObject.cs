using UnityEngine;

namespace Summer
{
    public class PoolVfxObject : PoolDefaultGameObject
    {
        #region 属性

        protected TimeInterval _interval = new TimeInterval(1f);

        protected bool _is_bind;
        protected Transform _bind_trans;

        protected Transform trans;

        #endregion

        #region MONO

        private void Awake()
        {
            trans = transform;
        }

        void Update()
        {
            _on_update_bind_go();

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

        public void BindGameobject(GameObject go)
        {
            _is_bind = true;
            _bind_trans = go.transform;
            _on_update_bind_go();
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
            _on_reset();
        }

        public override void OnPush()
        {
            base.OnPush();
            _on_reset();
        }

        #endregion

        #region private 

        public void _on_reset()
        {
            _interval.Pause();
            _is_bind = false;
            _bind_trans = null;
        }

        public void _on_update_bind_go()
        {
            if (!_is_bind) return;
            trans.position = _bind_trans.position;
            transform.eulerAngles = _bind_trans.eulerAngles;
        }
        #endregion
    }
}

