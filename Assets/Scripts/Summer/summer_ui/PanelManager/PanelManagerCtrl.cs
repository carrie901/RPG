
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System;
using Summer;
namespace Summer
{
    public enum E_PopupEndResult
    {
        ok,
        cancel,
        alt
    }
    public delegate void PopUpEndEvent(E_PopupEndResult result);

    public class PanelManagerCtrl
    {

        public const string POPUP_DIALOG = "";
        public const string OK_LOC = "Ok";
        public const string CANCEL_LOC = "Cancel";

        public static PanelManager _panelManager = PanelManager.Instance;

        public static void Lock(bool value)
        {
            _panelManager.Lock(value);
        }

        public static void ResetLock()
        {
            _panelManager.ResetLock();
        }


        public static void Open(E_ViewId viewId, System.Object info = null, Action<BaseView> action = null)
        {
            _panelManager.OnOpen(viewId, info, action);
        }

        public static void Close(E_ViewId viewId)
        {
            _panelManager.OnClose(viewId);
        }

        public static void ShowWait()
        {

        }

        public static void ShowConfirm(string text, PopUpEndEvent fun)
        {
            ShowConfirm(text, OK_LOC.Loc(), CANCEL_LOC.Loc(), fun);
        }

        public static void ShowConfirm(string text, string okText, PopUpEndEvent fun)
        {
            ShowConfirm(text, okText, CANCEL_LOC.Loc(), fun);
        }

        public static void ShowConfirm(string text, string okText, string cancelText, PopUpEndEvent fun)
        {
            /*ShowDialog("Popup_Dlg");

            PanelPopup dlg = GetDialogComponent<PanelPopup>("Popup_Dlg");
            dlg.SetContentText(text);
            dlg.SetOkText(OKText);
            dlg.SetCancelText(cancelText);
            dlg.Show(fun);*/
        }

        public static void ShowPopup()
        {

        }

    }
}