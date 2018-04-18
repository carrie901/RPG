using UnityEngine;

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
        public virtual void SetName(string go_name)
        {
            _name = go_name;
            gameObject.name = go_name;
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

        public virtual void OnInit()
        {
            IsUse = false;
        }

        public virtual void OnRecycled()
        {

        }

        public virtual void OnPop()
        {
            IsUse = true;
            GameObjectHelper.SetActive(gameObject, true);
        }

        public virtual void OnPush()
        {
            IsUse = false;
            GameObjectHelper.SetActive(gameObject, false);
        }

        #endregion

        #endregion
    }
}
