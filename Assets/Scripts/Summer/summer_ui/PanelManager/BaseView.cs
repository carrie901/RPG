using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace Summer
{
    /// <summary>
    /// UI OnEnter 初始化UI的界面和数据
    /// 
    /// 部分小界面也
    /// </summary>
    public abstract class BaseView : MonoBehaviour
    {
        #region 属性

        protected static int base_view_id;
        public int _iid;
        public PanelInfo _data;
        public System.Object _view_info;

        public int Iid { get { return _iid; } }
        public E_ViewId GetId() { return _data._view_id; }

        #endregion

        #region 生命周期 OnEnter-->OnEnter-->OnExit-->OnDestroySelf

        /// <summary>
        /// 只执行一次 主要侧重于按钮的监听,初始化
        /// </summary>
        public virtual void OnInit()
        {
            PanelLog.Log("初始化界面[{0}]", _data.ViewId);
            _init_id();
        }

        /// <summary>
        /// 一些界面的相关内容的初始化，会被反复初始化
        /// </summary>
        public virtual void OnEnter()
        {
            PanelLog.Log("进入界面[{0}]", _data.ViewId);
        }

        public virtual void OnExit()
        {
            PanelLog.Log("退出界面[{0}]", _data.ViewId);
        }

        public virtual void OnDestroySelf() { }

        #endregion

        #region public

        public virtual void SetPanelData(PanelInfo data)
        {
            _data = data;
            _view_info = _data.Info;
        }

        //public virtual void OnResetData<T>(T t) { }

        #endregion

        #region private 

        public void _init_id()
        {
            base_view_id++;
            _iid = base_view_id;
        }

        protected void CloseView()
        {
            PanelManager.Back(_data._view_id);
        }

        protected void OpenView(E_ViewId id)
        {
            PanelManager.Open(id);
        }

        #endregion
    }
}


