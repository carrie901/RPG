using System;
using UnityEngine;
using System.Collections.Generic;


// ReSharper disable once CheckNamespace
namespace Summer
{
    /// <summary>
    /// 本类只做一件事，指示当前View ID(单一事物原则)
    /// Panel类型的界面，只会存在一个，Dialog类型的界面可以存在很多个
    /// 是否在其中插入关于界面的OnEnter或者OnExit等类型的代码
    /// </summary>
    public class PanelQueneManager : ViewProcess
    {
        private readonly Stack<ViewData> _panels = new Stack<ViewData>();
        private ViewData _curr_view;
        public PanelQueneManager(Transform trans) : base(trans) { }

        #region ViewProcess

        public override BaseView OpenView(ViewData next_view_data, System.Object info)
        {
            //1.curr view隐藏
            InternalRealCloseView(_curr_view);
            //2.显示next view
            BaseView base_view = InternalRealOpenView(next_view_data, info);
            //3.栈中添加记录
            _panels.Push(next_view_data);
            //4.更新指向
            _curr_view = next_view_data;
            return base_view;
        }

        public override void CloseView(ViewData close_view)
        {
            if (_curr_view == null || _panels.Count <= 1) return;
            if (close_view.ViewId() != _curr_view.ViewId()) return;
            //1.curr view隐藏
            InternalRealCloseView(_curr_view);
            //2.栈中移除记录
            _panels.Pop();
            ViewData next_view = _panels.Count > 0 ? _panels.Peek() : null;
            //3.显示next view
            InternalRealOpenView(next_view, null);
            //4.更新指向
            _curr_view = next_view;
        }


        #endregion

        public ViewData GetCurrView() { return _curr_view; }
    }

    /// <summary>
    /// 界面的实际栈
    /// </summary>
    public class ViewStackManager : ViewProcess
    {
        public List<E_ViewId> operation = new List<E_ViewId>();
        public E_ViewId curr_view_id;
        public ViewStackManager(Transform trans) : base(trans) { }

        public override BaseView OpenView(ViewData next_view, object info)
        {
            curr_view_id = next_view.ViewId();
            operation.Add(curr_view_id);
            return InternalRealOpenView(next_view, info);
        }

        public override void CloseView(ViewData next_view)
        {
            LogManager.Assert(curr_view_id == next_view.ViewId(), "关闭的是非当前界面，请注意查收");
            operation.RemoveAt(operation.Count - 1);
            InternalRealCloseView(next_view);
        }
    }

}
