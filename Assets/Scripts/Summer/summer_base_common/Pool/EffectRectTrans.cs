using UnityEngine;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-27 16:17:50
// FileName : EffectRectTrans.cs
//=============================================================================

namespace Summer
{
    public class EffectRectTrans : PoolDefaultRectTransform
    {
        public float _leftTime;
        public bool _isAvlie;

        void Update()
        {
            if (!_isAvlie) return;

            _leftTime = _leftTime - Time.deltaTime;
            if (_leftTime <= 0)
            {
                _isAvlie = false;
                OnComplete();
            }
        }

        public void SetLeftTime(float leftTime)
        {
            _isAvlie = true;
            _leftTime = leftTime;
        }

        #region PoolAbility

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnRecycled()
        {
            base.OnRecycled();
            GameObjectHelper.DestroySelf(gameObject);
        }

        public override void OnPop()
        {
            base.OnPop();
        }

        public override void OnPush()
        {
            OnInit();
            base.OnPush();
        }
        #endregion

        private void OnComplete()
        {
            RectTransformPool.Instance.Push(this);
        }
    }
}
