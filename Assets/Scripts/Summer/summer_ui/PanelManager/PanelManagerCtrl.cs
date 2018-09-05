
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

        public static PanelManager panel_manager = PanelManager.Instance;

        public static void Lock(bool value)
        {
            panel_manager.Lock(value);
        }

        public static void ResetLock()
        {
            panel_manager.ResetLock();
        }


        public static void Open(E_ViewId view_id, System.Object info = null, Action<BaseView> action = null)
        {
            panel_manager.OnOpen(view_id, info, action);
        }

        public static void Close(E_ViewId view_id)
        {
            panel_manager.OnClose(view_id);
        }

        public static void ShowWait()
        {

        }

        public static void ShowConfirm(string text, PopUpEndEvent fun)
        {
            ShowConfirm(text, OK_LOC.Loc(), CANCEL_LOC.Loc(), fun);
        }

        public static void ShowConfirm(string text, string ok_text, PopUpEndEvent fun)
        {
            ShowConfirm(text, ok_text, CANCEL_LOC.Loc(), fun);
        }

        public static void ShowConfirm(string text, string ok_text, string cancel_text, PopUpEndEvent fun)
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