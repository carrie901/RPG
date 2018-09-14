using UnityEngine;

namespace Summer
{
    public class PoolVfxObject : PoolDefaultGameObject, I_Update
    {
        #region 属性

        protected TimeInterval _interval = new TimeInterval(1f);

        protected bool _isBind;
        protected Transform _bindTrans;

        protected Transform _trans;

        #endregion

        #region MONO

        void Awake()
        {
            _trans = transform;
        }


        void Update()
        {
            OnUpdate(0);
        }

        #endregion

        #region

        public void OnUpdate(float dt)
        {
            if (!IsUse) return;
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
            _isBind = true;
            _bindTrans = go.transform;
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
            _isBind = false;
            _bindTrans = null;
        }

        public void _on_update_bind_go()
        {
            if (!_isBind) return;
            _trans.position = _bindTrans.position;
            transform.eulerAngles = _bindTrans.eulerAngles;
        }
        #endregion
    }
}

