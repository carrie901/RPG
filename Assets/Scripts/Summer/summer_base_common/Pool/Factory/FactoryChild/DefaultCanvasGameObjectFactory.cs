using UnityEngine;

namespace Summer
{
    public class DefaultCanvasGameObjectFactory : PoolObjectFactory
    {

        protected GameObject factory_go_root;                              // 工场的GameObject
        protected RectTransform factory_go_root_rect;                         // 工场的GameObject.Transform

        public DefaultCanvasGameObjectFactory(string name) : base(name)
        {
            Init();
        }

        #region override

        public override I_PoolObjectAbility Create()
        {
            ResRequestInfo res_request=ResRequestFactory.CreateRequest<GameObject>(FactoryName);
            // 加载
            GameObject go = ResManager.instance.LoadPrefab(res_request);

            PoolDefaultRectTransform po = go.GetComponent<PoolDefaultRectTransform>();
            if (po == null)
            {
                po = go.AddComponent<PoolDefaultRectTransform>();
                LogManager.Error("这种形式的创建，会导致最后Pop的时候会有困难[{0}]", _factory_name);
            }

            po.SetName(_factory_name);
            po.SetParent(factory_go_root.transform);
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

            po.SetParent(factory_go_root.transform);
        }

        #endregion

        #region private

        private void Init()
        {
            // 1.创建RectTransform
            factory_go_root_rect = GameObjectHelper.CreateRectTransform(_factory_name);
            factory_go_root = factory_go_root_rect.gameObject;

            // 2.挂载在 UGUI对象池下面的Root GameObject下面的东西
            RectTransformHelper.SetParent(factory_go_root_rect, RectTransformPool.Instance.FindTrans());
        }

        #endregion
    }

}