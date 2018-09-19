
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
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class EffectReportPanel : EComponent
    {
        #region 属性

        public ELabelInput _rootPath;
        public EButton _searchFileBtn;

        public TableView _effectTable;
        public TableViewPanel _effectTablePanel;

        public TableView _effectTexTable;
        public TableViewPanel _effectTexTablePanel;

        public List<System.Object> _shows = new List<object>();

        public List<CheckEffectReportCnf> _effcnfs = new List<CheckEffectReportCnf>();

        #endregion

        #region Public

        public EffectReportPanel(EditorWindow win, float width, float height) : base(width, height)
        {
            AddData();
            _init(win);
        }

        public void AddData()
        {
            _effcnfs.Clear();

            List<List<string>> contents =
                CnfHelper.GetContext(Application.dataPath + "/../Report/Effect_Report.csv");

            int length = contents.Count;
            for (int i = 0; i < length; i++)
            {
                CheckEffectReportCnf info = new CheckEffectReportCnf();
                info.SetInfo(contents[i]);
                _effcnfs.Add(info);
            }

            _shows.Clear();
            length = _effcnfs.Count;
            for (int i = 0; i < length; i++)
                _shows.Add(_effcnfs[i]);
        }

        #endregion

        #region Private Methods

        public void _init(EditorWindow win)
        {
            _rootPath = new ELabelInput("文件名:", 80, "", 400);
            _searchFileBtn = new EButton("文件名检索", 150);

            _searchFileBtn.OnClick += OnSearchFileByPath;

            ERect item = _rootPath;
            AddComponent(_rootPath, 5, 5);
            item = AddComponentRight(_searchFileBtn, item);

            _effectTable = new TableView(win, typeof(CheckEffectReportCnf));
            _effectTable.OnSelected += OnRuleSelected;
            _effectTable.AddColumn("_assetPath", CheckEffectReportCnf.ASSET_PATH, 0.4f);
            _effectTable.AddColumn("_effName", CheckEffectReportCnf.EFFECT_NAME, 0.15f);
            _effectTable.AddColumn("_loadTime", CheckEffectReportCnf.LOAD_TIME, 0.05f);
            _effectTable.AddColumn("_instTime", CheckEffectReportCnf.INST_TIME, 0.05f);

            _effectTable.AddColumn("_dc", CheckEffectReportCnf.DRAWCALL, 0.05f);
            _effectTable.AddColumn("_triangles", CheckEffectReportCnf.TRIANGLES, 0.05f);
            _effectTable.AddColumn("_materialCount", CheckEffectReportCnf.MATERIAL_COUNT, 0.05f);
            _effectTable.AddColumn("_totalPsCount", CheckEffectReportCnf.TOTAL_PS_COUNT, 0.05f);
            _effectTable.AddColumn("_texMemBytes", CheckEffectReportCnf.TEX_MEM_BYTES, 0.05f);
            _effectTable.AddColumn("_texMemCount", CheckEffectReportCnf.TEX_MEM_COUNT, 0.05f);
            _effectTable.AddColumn("_animationCount", CheckEffectReportCnf.ANIM_COUNT, 0.05f);


            _effectTablePanel = new TableViewPanel(Ew - 10, 200);
            _effectTablePanel.AddTableView(_effectTable);
            _effectTablePanel.RefreshData(_shows);

            AddComponent(_effectTablePanel, 5, 10 + _rootPath.Eh);

            _effectTexTable = new TableView(win, typeof(TmpStringInfo));
            _effectTexTable.AddColumn("_param1", "路径", 0.45f, TextAnchor.MiddleLeft);
            _effectTexTable.AddColumn("_param2", "内存占用", 0.05f, TextAnchor.MiddleCenter);


            _effectTexTablePanel = new TableViewPanel(Ew - 10, Eh - 10 - _rootPath.Eh - _effectTablePanel.Eh);
            _effectTexTablePanel.AddTableView(_effectTexTable);
            AddComponentDown(_effectTexTablePanel, _effectTablePanel);

        }

        private void OnSearchFileByPath(EButton button)
        {
            string searchPath = _rootPath.Text;
            OnSearchTextures(searchPath);
        }

        public void OnSearchTextures(string searchPath)
        {
            _shows.Clear();
            // TODO 难看的代码
            if (string.IsNullOrEmpty(searchPath))
            {
                _shows.Clear();
                int effLength = _effcnfs.Count;
                for (int i = 0; i < effLength; i++)
                    _shows.Add(_effcnfs[i]);

                _effectTexTablePanel.RefreshData(_shows);
                return;
            }

            int length = _effcnfs.Count;
            for (int i = 0; i < length; i++)
            {
                if (!_effcnfs[i]._assetPath.Contains(searchPath)) continue;
                _shows.Add(_effcnfs[i]);
            }

            _effectTexTablePanel.RefreshData(_shows);
        }

        // 选择某一条具体的规则
        private void OnRuleSelected(object select, int col)
        {
            CheckEffectReportCnf info = select as CheckEffectReportCnf;
            if (info == null) return;

            List<System.Object> objs = new List<object>();
            for (int i = 0; i < info._texs.Count; i++)
            {
                TmpStringInfo tmp = new TmpStringInfo();
                tmp._param1 = info._texs[i];
                tmp._param2 = info._texMems[i].ToString();
                objs.Add(tmp);
            }
            _effectTexTablePanel.RefreshData(objs);

            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(info._assetPath, typeof(UnityEngine.Object));
            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }

        #endregion
    }
}
