using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 创建GameObject的默认工场
    /// 设置工场的名字-一般为Prefab的名字 
    /// TransformPool
    ///     Factory_GameObject
    ///         Eff_GameObject
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
            GameObject go = ResManager.instance.LoadPrefab(FactoryName);
            if (go == null)
            {
                LogManager.Error("缓存池_加载[{0}]失败", _factory_name);
                return null;
            }
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
            // 创建工场GameObject
            GameObject go = GameObjectHelper.CreateGameObject(_factory_name, false);
            factory_go_root_trans = go.transform;
            // 
            GameObjectHelper.SetParent(go, TransformPool.Instance.FindTrans());
        }

        #endregion
    }

    /// <summary>
    /// 创建GameObject的默认工场
    /// 设置工场的名字-一般为Prefab的名字 
    /// TransformPool
    ///     E_GameResType
    ///         Factory_GameObject
    ///             Eff_GameObject
    /// </summary>
    /*public class GameResPrefabFactory : PoolObjectFactory
    {
        protected Transform factory_go_root_trans;
        protected E_GameResType _res_type;
        public GameResPrefabFactory(string factory_name, E_GameResType res_type) : base(factory_name)
        {
            _res_type = res_type;
            _init();
        }

        public override I_PoolObjectAbility Create()
        {
            throw new System.NotImplementedException();
        }

        public override void ExtraOpertion(I_PoolObjectAbility ability)
        {
            throw new System.NotImplementedException();
        }

        #region private 

        public void _init()
        {

            string res_type = _res_type.ToString();
            Transform res_type_trans = TransformPool.Instance.FindTransform(res_type);
            if (res_type_trans == null)
            {
                GameObject res_type_go = GameObjectHelper.CreateGameObject(res_type, false);
                res_type_trans = res_type_go.transform;
                GameObjectHelper.SetParent(res_type_go, TransformPool.Instance.FindTrans());
            }

            // 创建工场GameObject
            GameObject go = GameObjectHelper.CreateGameObject(_factory_name, false);
            factory_go_root_trans = go.transform;

            GameObjectHelper.SetParent(go, res_type_trans.gameObject);

        }

        #endregion
    }*/

}
