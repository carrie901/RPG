﻿using UnityEngine;

namespace Summer
{
    public class PoolDefaultGameObject : MonoBehaviour, I_PoolObjectAbility
    {
        public bool IsUse { get; set; }
        public string ObjectName { get { return _name; } }

        protected string _name;

        #region MONO

        void OnDestroy()
        {
            //SelfDestroy();
        }

        #endregion

        #region public 

        #region self vitrual 

        // 对象池中的对象的ObjectName名字
        public virtual void SetName(string goName)
        {
            _name = goName;
            gameObject.name = goName;
            //TODO QAQ
            //SetParent(TransformPool.Instance.FindTrans());
        }

        public virtual void SetParent(Transform trans)
        {
            GameObjectHelper.SetParent(gameObject, trans);
        }

        public virtual void SelfDestroy()
        {
            if (IsUse)
                LogManager.Error("[{0}]没有被回收就被销毁掉了", ObjectName);
        }

        #endregion

        #region override I_PoolObjectAbility 

        /// <summary>
        /// 初始化只执行一次 在创建的时候执行
        /// </summary>
        public virtual void OnInit()
        {
            IsUse = false;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public virtual void OnRecycled()
        {

        }
        /// <summary>
        /// 出
        /// </summary>
        public virtual void OnPop()
        {
            IsUse = true;
            GameObjectHelper.SetActive(gameObject, true);
        }
        /// <summary>
        /// 进
        /// </summary>
        public virtual void OnPush()
        {
            IsUse = false;
            GameObjectHelper.SetActive(gameObject, false);
        }

        #endregion

        #endregion
    }
}
