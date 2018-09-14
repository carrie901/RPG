using UnityEngine;

namespace Summer
{
    public class DefaultCanvasGameObjectFactory : PoolObjectFactory
    {

        protected GameObject _factoryGoRoot;                                // 工场的GameObject
        protected RectTransform _factoryGoRootRect;                         // 工场的GameObject.Transform

        public DefaultCanvasGameObjectFactory(string name) : base(name)
        {
            Init();
        }

        #region override

        public override I_PoolObjectAbility Create()
        {
            // 加载
            GameObject go = ResManager.instance.LoadPrefab(FactoryName);

            PoolDefaultRectTransform po = go.GetComponent<PoolDefaultRectTransform>();
            if (po == null)
            {
                po = go.AddComponent<PoolDefaultRectTransform>();
                LogManager.Error("这种形式的创建，会导致最后Pop的时候会有困难[{0}]", _factoryName);
            }

            po.SetName(_factoryName);
            po.SetParent(_factoryGoRoot.transform);
            return po;
        }

        public override void ExtraOpertion(I_PoolObjectAbility ability)
        {
            PoolDefaultRectTransform po = ability as PoolDefaultRectTransform;
            if (po == null)
            {
                LogManager.Error("对象池工场Push的内容为空或者类型不对");
                return;
            }

            po.SetParent(_factoryGoRoot.transform);
        }

        #endregion

        #region private

        private void Init()
        {
            // 1.创建RectTransform
            _factoryGoRootRect = GameObjectHelper.CreateRectTransform(_factoryName);
            _factoryGoRoot = _factoryGoRootRect.gameObject;

            // 2.挂载在 UGUI对象池下面的Root GameObject下面的东西
            RectTransformHelper.SetParent(_factoryGoRootRect, RectTransformPool.Instance.FindTrans());
        }

        #endregion
    }

}