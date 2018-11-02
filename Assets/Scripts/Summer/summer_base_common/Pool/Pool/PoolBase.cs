using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 缓存池
    /// </summary>
    public abstract class PoolBase : I_PoolBase
    {
        protected List<I_PoolObjectAbility> _out = new List<I_PoolObjectAbility>(8);
        protected List<I_PoolObjectAbility> _in = new List<I_PoolObjectAbility>(8);

        protected I_ObjectFactory _factory;

        protected int _maxCount;

        public string _poolName;

        protected PoolBase(I_ObjectFactory factory, int maxCount = 0)
        {
            _poolName = factory.FactoryName;
            _factory = factory;
            _maxCount = maxCount;
        }

        /// <summary>
        /// 从池子中拿一个资源出来
        /// </summary>
        /// <returns></returns>
        public virtual I_PoolObjectAbility Pop()
        {
            I_PoolObjectAbility pa;
            if (_in.Count == 0)
            {
                pa = _factory.Create();
                if (pa != null)
                    pa.OnInit();
            }
            else
            {
                pa = _in[_in.Count - 1];
                _in.Remove(pa);
            }
            if (pa == null) return null;
            _out.Add(pa);
            pa.OnPop();
            return pa;
        }

        /// <summary>
        /// 把资源放到池子中
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool Push(I_PoolObjectAbility obj)
        {
            if (obj == null)
            {
                LogManager.Error("对象池[{0}]回收null的资源错误", _factory.FactoryName);
                return false;
            }

            if (!obj.IsUse)
            {
                LogManager.Error("对象池[{0}]回收资源错误[{1}]错误,正在使用中", _factory.FactoryName, obj.ObjectName);
                return false;
            }

            if (_maxCount > 0 && _in.Count > _maxCount)
            {
                LogManager.Log("超过对象池大小,立马释放对象");
                obj.OnRecycled();
                obj = null;
                return false;
            }

            _factory.ExtraOpertion(obj);

            obj.IsUse = false;
            _in.Add(obj);
            _out.Remove(obj);
            obj.OnPush();
            return true;
        }

        /// <summary>
        /// 释放内存
        /// </summary>
        public void ReaycelAll()
        {

        }
    }
}
