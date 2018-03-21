using UnityEngine;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-2 15:30:4
// FileName : DialogQueneManager.cs
//=============================================================================

namespace Summer
{
    public class DialogQueneManager : ViewProcess
    {
        private readonly Stack<ViewData> _dialogs = new Stack<ViewData>();
        private ViewData _curr_view;
        public DialogQueneManager(Transform trans) : base(trans) { }

        public override BaseView OpenView(ViewData data, System.Object info)
        {
            _curr_view = data;
            _dialogs.Push(_curr_view);
            BaseView t = InternalRealOpenView(_curr_view, info);

            return t;
        }

        public override void CloseView(ViewData data)
        {
            if (_curr_view == null) return;
            if (data.ViewId() != _curr_view.ViewId()) return;
            InternalRealCloseView(_curr_view);
            _dialogs.Pop();
            _curr_view = _dialogs.Count > 0 ? _dialogs.Peek() : null;
        }

        public void CloseAll()
        {
            if (_curr_view == null) return;

            while (_dialogs.Count > 0)
            {
                InternalRealCloseView(_curr_view);
                _dialogs.Pop();
                _curr_view = _dialogs.Count > 0 ? _dialogs.Peek() : null;
            }

        }
    }
}
