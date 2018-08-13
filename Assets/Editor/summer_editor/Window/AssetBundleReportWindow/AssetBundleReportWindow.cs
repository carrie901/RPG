
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
        protected static float t_width = 1600;
        protected static float t_height = 800;
        [MenuItem("Tools/UI/AssetBundleReportWindow")]
        static void Init()
        {
            _window = EditorWindow.GetWindow<AssetBundleReportWindow>();   // 创建自定义窗体
            _window.titleContent = new GUIContent("构建树视图");         // 窗口的标题
            //_window.minSize = new Vector2(t_width, t_height);
            //_window.maxSize = new Vector2(t_width + 40, t_height + 40);
            _window.Show();
            _window.GetAssets();
            // 创建树*/
        }

        #endregion

        #region 属性

        public ExtendTableComponent _table_comp;

        #endregion

        #region MONO Override

        public void GetAssets()
        {
            int max_size = 10;
            _table_comp = new ExtendTableComponent(t_width - 100, t_height - 60);
            _table_comp.ResetPosition(_table_comp.Ew / 2, _table_comp.Eh / 2);
            _table_comp.SetInfo(AssetBundleReport.titles, AssetBundleReport.titles_width, max_size);
            for (int i = 0; i < max_size; i++)
            {
                AssetBundleReportItem item = new AssetBundleReportItem(10, AssetBundleReport.titles_width, t_width - 100 - 60, ERect.DEFAULT_HEIGHT + 4);
                _table_comp.AddItem(item);
            }

            _table_comp.OnUpdateItem += OnUpdateItem;
            _table_comp.ResetInfo();
        }

        #endregion

        #region Public



        #endregion

        #region Private Methods

        void OnGUI()
        {
            _table_comp.OnDraw(20, 20);
        }

        public void OnUpdateItem(int page, int interval, List<ExtendTableItem> items)
        {
            List<EAssetBundleFileInfo> ab_infos = AssetBundleAnalyzeManager.FindAssetBundleFiles();
            int ab_length = ab_infos.Count;
            int length = items.Count;
            for (int i = 0; i < length; i++)
            {
                int index = (page - 1) * interval + i;
                if (i < ab_length && i >= 0)
                    items[i].SetData(ab_infos[index]);
                else
                {
                    items[i].SetData(null);
                }
            }
        }

        #endregion
    }
}