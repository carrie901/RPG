
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
using Summer;
using SummerEditor;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class AssetBundleListReportWin : EComponent
    {
        public TableView _tableView;
        public TableViewPanel _panel;
        public EditorWindow Win;

        public TableView _depTableView;
        public TableViewPanel _depTabPanel;

        public AssetBundleListReportWin(EditorWindow win, float width, float height) : base(width, height)
        {
            Win = win;
            _init();
            LoadCnf();
        }

        public void LoadCnf()
        {
            List<List<string>> contents =
                CnfHelper.GetContext(Application.dataPath + "/../Report/2018_12_10__1610/AssetBundle资源列表.csv");
            SetInfo(contents);
        }

        public void SetInfo(List<List<string>> contents)
        {
            List<System.Object> objs = new List<object>();
            for (int i = 0; i < contents.Count; i++)
            {
                AssetBundleReportInfo info = new AssetBundleReportInfo();
                info.SetInfo(contents[i]);
                objs.Add(info);
            }
            _panel.RefreshData(objs);
        }

        public void OnSelectFile(object selected, int col)
        {
            TmpStringInfo texInfo = selected as TmpStringInfo;
            if (texInfo == null)
                return;
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath("Assets/StreamingAssets/rpg/" + texInfo._param1,
                typeof(UnityEngine.Object));
            if (obj == null) return;
            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }

        public void OnSelectAnimation(object selected, int col)
        {
            AssetBundleReportInfo info = selected as AssetBundleReportInfo;
            if (info == null) return;

            /*List<System.Object> objs = new List<object>();
            for (int i = 0; i < info.BeRefs.Count; i++)
            {
                TmpStringInfo tmp = new TmpStringInfo();
                tmp._param1 = info.BeRefs[i];
                objs.Add(tmp);
            }
            _depTabPanel.RefreshData(objs);*/
        }

        public void _init()
        {
            int depWidth = 400;
            _tableView = new TableView(Win, typeof(AssetBundleReportInfo));
            _tableView.OnSelected += OnSelectAnimation;
            _tableView.AddColumn("AssetBundleName", AssetBundleReport.ASSETBUNDLE_NAME, 0.4f, TextAnchor.MiddleLeft);
            _tableView.AddColumn("FileSize", AssetBundleReport.AB_MEMORY_SIZE, 0.1f, TextAnchor.MiddleCenter, "<fmt_bytes>");
            _tableView.AddColumn("CalMemSize", AssetBundleReport.CAL_MEMORY_SIZE, 0.15f, TextAnchor.MiddleCenter, "<fmt_bytes>");
            _tableView.AddColumn("RepeatMemSize", AssetBundleReport.REPEAT_MEMORY_SIZE, 0.15f,TextAnchor.MiddleCenter, "<fmt_bytes>");
            _tableView.AddColumn("DepAb", AssetBundleReport.AB_DEP, 0.1f);
            _tableView.AddColumn("RepeatAb", AssetBundleReport.BE_REF, 0.1f);
            _panel = new TableViewPanel(Ew - 10 - depWidth, Eh);
            _panel.AddTableView(_tableView);
            AddComponent(_panel, 5, 5);

            _depTableView = new TableView(Win, typeof(TmpStringInfo));
            _depTableView.AddColumn("_param1", "冗余列表", 1, TextAnchor.MiddleLeft);
            _depTableView.OnSelected += OnSelectFile;
            _depTabPanel = new TableViewPanel(depWidth, Eh);
            _depTabPanel.AddTableView(_depTableView);
            AddComponentRight(_depTabPanel, _panel);
        }
    }

}