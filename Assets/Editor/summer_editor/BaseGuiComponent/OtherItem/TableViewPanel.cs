
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

using System.Collections.Generic;
using UnityEngine;

namespace SummerEditor
{
    public class TableViewPanel : ERectItem
    {
        public TableView _view;
        public TableViewPanel(float width, float height) : base(width, height)
        {
        }

        public override void Draw()
        {
            //GUI.Box(_world_pos, "", EStyle.BoxStyle1);

            //GUILayout.BeginVertical();

            //GUILayout.BeginArea(_world_pos);
            if (_view != null)
                _view.Draw(_world_pos);
            //GUILayout.EndArea();

            //GUILayout.EndVertical();
        }

        public void AddTableView(TableView view)
        {
            _view = view;
        }

        public void RefreshData(List<object> entries, Dictionary<object, Color> specialTextColors = null)
        {
            if (_view != null)
                _view.RefreshData(entries, specialTextColors);
        }
    }
}