
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
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetFormatWin : EditorWindow
    {

        #region 静态属性

        protected static AssetFormatWin _window;
        protected static float _tWidth = 1600;
        protected static float _tHeight = 800;
        [MenuItem("策划/资源展示")]
        static void Init()
        {
            _window = GetWindow<AssetFormatWin>();
            _window.titleContent = new GUIContent("资源展示");
            _window.Show();
            _window.GetAssets();
        }

        #endregion

        #region 属性

        public EComponent _container;                                           // 容器
        public ToolBarPanel _toolBarPanel;

        #endregion

        #region MONO Override

        public void GetAssets()
        {
            _container = new EComponent(_tWidth, _tHeight);
            _container.SetBg(false);
            _container.ResetPosition(_tWidth / 2, _tHeight / 2);
            _container.SetBg(0, 0, 0);
            _toolBarPanel = new ToolBarPanel(new List<string>() { "纹理", "B", "C" }, _tWidth - 10, _tHeight - 10);
            float[] wh = _toolBarPanel.GetChildWh();
            TextureFormatWin texWin = new TextureFormatWin(this,wh[0], wh[1]);
            _toolBarPanel.AddPanel(0, texWin);
            _container.AddComponent(_toolBarPanel, 5, 10);
            _toolBarPanel.ResetSelect();
        }

        #endregion


        #region Private Methods

        void OnGUI()
        {
            if (_container != null)
            {
                _container.OnDraw(5, 5);
            }
        }
        #endregion
    }
}