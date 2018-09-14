using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 对象池中默认的属于UGUI的对象，由于UGUI中的GameObject和正常世界中的GameObject不太一样，最好不混用
    /// </summary>
    public class PoolDefaultRectTransform : PoolDefaultGameObject
    {
        protected RectTransform _poolDefaultRect;

        #region public 

        // 设置父类，属于UGUI特有的
        public override void SetParent(Transform trans)
        {
            if (_poolDefaultRect == null)
                _poolDefaultRect = gameObject.GetComponent<RectTransform>();
            RectTransformHelper.SetParent(_poolDefaultRect, trans);
        }

        #endregion
    }

}