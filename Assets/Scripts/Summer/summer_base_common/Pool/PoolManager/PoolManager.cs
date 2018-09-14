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
        public static uint _id = 0;

        protected Dictionary<string, PoolBase> _map
            = new Dictionary<string, PoolBase>();

        //protected GameObject _go_root;
        //protected Transform _go_root_trans;

        #endregion

        #region public 

        #region override

        public virtual void Init()
        {

        }

        public virtual T Pop<T>(string prefabName) where T : class, I_PoolObjectAbility
        {
            PoolBase poolBase;
            _map.TryGetValue(prefabName, out poolBase);
            if (poolBase == null)
            {
                poolBase = GetDefaultFactory(prefabName);
            }
            I_PoolObjectAbility po = poolBase.Pop();
            T t = po as T;
            if (t == null)
                LogManager.Error("po:[{0}],[{1}]", po, prefabName);
            return t;
        }

        public virtual bool Push(I_PoolObjectAbility po)
        {
            PoolBase poolBase;
            if (string.IsNullOrEmpty(po.ObjectName))
            {
                LogManager.Error("名字为空");
                return false;
            }

            _map.TryGetValue(po.ObjectName, out poolBase);
            if (poolBase == null)
            {
                LogManager.Error("子类缓存管理不存在");
                return false;
            }

            return poolBase.Push(po);
        }

        public abstract PoolBase GetDefaultFactory(string prefabName);

        #endregion

        /*public Transform FindTransform(string name)
        {
            if (_go_root_trans == null) return null;
            return _go_root_trans.Find(name);
        }*/

        #endregion
    }
}
