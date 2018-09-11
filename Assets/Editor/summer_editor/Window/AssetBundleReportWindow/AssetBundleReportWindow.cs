
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
    public class AssetBundleReportWindow : EditorWindow
    {

        #region 静态属性

        protected static AssetBundleReportWindow _window;     // 自定义窗体
        protected static float _tWidth = 1600;
        protected static float _tHeight = 800;

        public static void Init()
        {
            _window = EditorWindow.GetWindow<AssetBundleReportWindow>();   // 创建自定义窗体
            _window.titleContent = new GUIContent("资源报告");         // 窗口的标题
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
            _toolBarPanel = new ToolBarPanel(new List<string>() { "AssetBundle资源列表", "动作文件", "网格", "纹理" }, _tWidth - 10, _tHeight - 10);
            float[] wh = _toolBarPanel.GetChildWh();

            AssetBundleListReportWin abWin = new AssetBundleListReportWin(this, wh[0], wh[1]);
            _toolBarPanel.AddPanel(0, abWin);

            AnimationsReportWin animWin = new AnimationsReportWin(this, wh[0], wh[1]);
            _toolBarPanel.AddPanel(1, animWin);

            MeshsReportWin meshWin = new MeshsReportWin(this, wh[0], wh[1]);
            _toolBarPanel.AddPanel(2, meshWin);

            TexturesReportWin texWin = new TexturesReportWin(this, wh[0], wh[1]);
            _toolBarPanel.AddPanel(3, texWin);

            _container.AddComponent(_toolBarPanel, 5, 10);
            _toolBarPanel.ResetSelect();
        }

        #endregion

        #region Public



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