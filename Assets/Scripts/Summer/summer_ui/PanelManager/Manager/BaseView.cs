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
        public ViewData _data;
        public System.Object _view_info;

        #region 生命周期

        /// <summary>
        /// 只执行一次 主要侧重于按钮的监听,初始化
        /// </summary>
        public virtual void OnInit() { }

        /// <summary>
        /// 一些界面的相关内容的初始化，会被反复初始化
        /// </summary>
        public virtual void OnEnter()
        {

        }

        public virtual void OnExit() { }

        public virtual void OnDestroyView() { }

        public virtual void OnResetData<T>(T t)
        {

        }

        public virtual E_ViewId GetViewId() { return E_ViewId.invaild; }

        #endregion

        public virtual void CloseView()
        {
            PanelManager.Back(GetId());
        }

        public void OpenView(E_ViewId id)
        {
            PanelManager.Open(id);
        }

        public E_ViewId GetId() { return _data._view_id; }
        public void OpenExtraOpertion(ViewData data)
        {
            _data = data;

            if (_data.show_mode == E_PanelBgType.col)
            {
                /* RectTransform rect_trans = new RectTransform();
                 rect_trans.sizeDelta = new Vector2(1280, 720);
                 rect_trans.gameObject.AddComponent<EmptyGraphics>();
                 GameObjectHelper.AddChild(rect_trans, transform, true);*/
            }
            else if (_data.show_mode == E_PanelBgType.col_and_img)
            {

                /*  RectTransform rect_trans = new RectTransform();
                  rect_trans.rect = new Rect(0,0,1280,720);
                  Image img = rect_trans.gameObject.AddComponent<Image>();
                  img.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
                  UiHelper.AddChild(rect_trans, transform, true);*/
            }
        }

        public virtual void SetData(System.Object data)
        {

        }

        public void CloseExtraOpertion()
        {

        }
    }
}


