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
        public float _left_time;
        public bool _is_avlie;

        void Update()
        {
            if (!_is_avlie) return;

            _left_time = _left_time - Time.deltaTime;
            if (_left_time <= 0)
            {
                _is_avlie = false;
                OnComplete();
            }
        }

        public void SetLeftTime(float left_time)
        {
            _is_avlie = true;
            _left_time = left_time;
        }

        #region PoolAbility

        public override void OnInit()
        {
            base.OnInit();
        }

        public override void OnRecycled()
        {
            base.OnRecycled();
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
