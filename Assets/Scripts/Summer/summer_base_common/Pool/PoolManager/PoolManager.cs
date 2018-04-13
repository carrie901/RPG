using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 缓存工场
    /// </summary>
    public abstract class PoolManager
    {
        #region Porp

        //所有内存池对象id，每使用一个则计数加一
        public static uint id = 0;

        protected Dictionary<string, PoolBase> _map
            = new Dictionary<string, PoolBase>();

        protected GameObject _go_root;
        protected Transform _go_root_trans;

        #endregion

        #region public 

        #region override

        public virtual void Init()
        {

        }

        public virtual T Pop<T>(string prefab_name) where T : class, I_PoolObjectAbility
        {
            PoolBase pool_base;
            _map.TryGetValue(prefab_name, out pool_base);
            if (pool_base == null)
            {
                pool_base = GetDefaultFactory(prefab_name);
            }
            I_PoolObjectAbility po = pool_base.Pop();
            T t = po as T;
            if (t == null)
                LogManager.Error("po:[{0}]", po);
            return t;
        }

        public virtual bool Push(I_PoolObjectAbility po)
        {
            PoolBase pool_base;
            _map.TryGetValue(po.ObjectName, out pool_base);
            if (pool_base == null)
            {
                LogManager.Error("子类缓存管理不存在");
                return false;
            }

            return pool_base.Push(po);
        }

        public abstract PoolBase GetDefaultFactory(string prefab_name);

        #endregion

        public Transform FindTransform(string name)
        {
            if (_go_root_trans == null) return null;
            return _go_root_trans.Find(name);
        }

        #endregion
    }
}
