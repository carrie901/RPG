using System;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// TODO QAQ畸形的缓存池，等待剔除
    /// </summary>
    public class EntityPool
    {
        public static EntityPool Instance = new EntityPool();
        protected List<I_EntityLife> _out = new List<I_EntityLife>(8);
        protected List<I_EntityLife> _in = new List<I_EntityLife>(8);

        protected EntityPool() { }

        /// <summary>
        /// 从池子中拿一个资源出来
        /// </summary>
        /// <returns></returns>
        public virtual BaseEntity Pop(int hero_id)
        {
            I_EntityLife pa;
            if (_in.Count == 0)
            {
                pa = new BaseEntity();
                pa.OnInit();
            }
            else
            {
                pa = _in[_in.Count - 1];
                _in.Remove(pa);
            }
            pa.IsUse = true;
            _out.Add(pa);
            pa.OnPop(hero_id);
            return pa as BaseEntity;
        }

        /// <summary>
        /// 把资源放到池子中
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public virtual bool Push(I_EntityLife obj)
        {
            if (obj == null)
            {
                LogManager.Error("对象池 [?]回收null的资源错误");
                return false;
            }

            if (!obj.IsUse)
            {
                LogManager.Error("对象池回收资源错误[{0}]错误,正在使用中", obj.ObjectName);
                return false;
            }

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

    public interface I_EntityLife
    {
        /// <summary>
        ///  只做一次
        /// </summary>
        void OnInit();

        void OnPop(int hero_id);

        void OnPush();

        bool IsUse { get; set; }

        string ObjectName { get; }
    }
}
