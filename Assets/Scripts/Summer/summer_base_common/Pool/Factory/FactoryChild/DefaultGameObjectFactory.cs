using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 创建GameObject的默认工场
    /// 设置工场的名字-一般为Prefab的名字 
    /// </summary>
    public class DefaultGameObjectFactory : PoolObjectFactory
    {
        protected Transform factory_go_root_trans;

        public DefaultGameObjectFactory(string name) : base(name)
        {
            _init();
        }

        public override I_PoolObjectAbility Create()
        {
            GameObject go = ResManager.instance.LoadPrefab(FactoryName, E_GameResType.quanming);

            PoolDefaultGameObject po = go.GetComponent<PoolDefaultGameObject>();
            if (po == null)
            {
                po = go.AddComponent<PoolDefaultGameObject>();
                LogManager.Error("这种形式的创建，会导致最后Pop的时候会有困难[{0}]", _factory_name);
            }

            po.SetName(FactoryName);
            po.SetParent(factory_go_root_trans);
            return po;
        }

        public override void ExtraOpertion(I_PoolObjectAbility ability)
        {
            PoolDefaultGameObject po = ability as PoolDefaultGameObject;
            if (po == null)
            {
                LogManager.Error("对象池工场Push的内容为空");
                return;
            }
            GameObjectHelper.SetParent(po.gameObject, factory_go_root_trans);
        }

        #region private

        public void _init()
        {
            GameObject go = GameObjectHelper.CreateGameObject(_factory_name, false);
            factory_go_root_trans = go.transform;
            GameObjectHelper.SetParent(go, TransformPool.Instance.FindTrans());
        }

        #endregion
    }

}
