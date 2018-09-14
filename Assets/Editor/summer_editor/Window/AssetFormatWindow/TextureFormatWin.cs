
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


using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
namespace SummerEditor
{
    public class TextureFormatWin : EComponent
    {
        #region 属性

        public ELabelInput _rootPath;
        public EButton _searchBtn;
        public EButton _serchAllBtn;
        public EButton _loadRuleBtn;
        public EButton _formatBtn;
        public TableView _texRuleTable;
        public TableViewPanel _texRuleTablePanel;

        public TableView _texShowTable;
        public TableViewPanel _texShowTablePanel;

        public AssetFormatRule _formatRule;
        public List<System.Object> _shows = new List<object>();
        #endregion

        #region Public

        public TextureFormatWin(EditorWindow win, float width, float height) : base(width, height)
        {
            _init(win);
        }

        #endregion

        #region Private Methods

        public void _init(EditorWindow win)
        {
            _rootPath = new ELabelInput("根目录:", 80, "", 400);
            _searchBtn = new EButton("根据目录检索", 150);
            _serchAllBtn = new EButton("检索所有目录", 150);
            _loadRuleBtn = new EButton("加载纹理规则", 150);
            _formatBtn = new EButton("格式化纹理", 150);
            _searchBtn.OnClick += OnSearchByPath;
            _serchAllBtn.OnClick += OnSearchAll;
            _loadRuleBtn.OnClick += OnloadRuleBtn;
            _formatBtn.OnClick += OnFormatBtn;
            ERect item = _rootPath;
            AddComponent(_rootPath, 5, 5);
            item = AddComponentRight(_searchBtn, item);
            item = AddComponentRight(_serchAllBtn, item);
            item = AddComponentRight(_loadRuleBtn, item);
            item = AddComponentRight(_formatBtn, item);
            _texRuleTable = new TableView(win, typeof(AssetFormatRule));
            _texRuleTable.OnSelected += OnRuleSelected;
            _texRuleTable.AddColumn("FilterPath", "过滤的路径", 0.45f);
            _texRuleTable.AddColumn("FilterRule", "过滤规则", 0.35f);
            _texRuleTable.AddColumn("FilterPath", "格式化", 0.25f);
            _texRuleTablePanel = new TableViewPanel(Ew - 10, 200);
            _texRuleTablePanel.AddTableView(_texRuleTable);
            AddComponent(_texRuleTablePanel, 5, 10 + _rootPath.Eh);

            _texShowTable = new TableView(win, typeof(TextureFormatInfo));
            _texShowTable.OnSelected += OnInfoSelected;
            _texShowTable.AddColumn("Path", "路径", 0.45f, TextAnchor.MiddleLeft);
            _texShowTable.AddColumn("MemSize", "内存占用", 0.05f, TextAnchor.MiddleCenter, "<fmt_bytes>");
            _texShowTable.AddColumn("ReadWriteEnable", "R/W", 0.05f);
            _texShowTable.AddColumn("MipmapEnable", "Mipmap", 0.05f);
            _texShowTable.AddColumn("AndroidFormat", "AndroidFormat", 0.1f);
            _texShowTable.AddColumn("IosFormat", "IosFormat", 0.1f);
            _texShowTable.AddColumn("ImportType", "纹理格式", 0.1f);
            _texShowTable.AddColumn("ImportShape", "ImportShape", 0.1f);

            _texShowTablePanel = new TableViewPanel(Ew - 10, Eh - 10 - _rootPath.Eh - _texRuleTablePanel.Eh);
            _texShowTablePanel.AddTableView(_texShowTable);
            AddComponentDown(_texShowTablePanel, _texRuleTablePanel);

        }

        private void OnSearchAll(EButton button)
        {
            OnSearchTextures(Application.dataPath);
        }

        private void OnSearchByPath(EButton button)
        {
            string searchPath = _rootPath.Text;
            OnSearchTextures(Application.dataPath + "/" + searchPath);
        }

        public void OnSearchTextures(string searchPath)
        {
            _shows.Clear();
            List<string> pathList = EPathHelper.GetAssetsPath(searchPath, true, "*.*");
            int length = pathList.Count;
            for (int i = 0; i < length; i++)
            {
                if (!EPathHelper.IsTexture(pathList[i])) continue;
                _shows.Add(TextureFormatInfo.Create(pathList[i]));
            }
            _texShowTablePanel.RefreshData(_shows);
            _formatRule = null;
        }

        public void OnloadRuleBtn(EButton button)
        {
            List<System.Object> rules = new List<System.Object>();
            //1. 导入并且初始化规则
            //2. 细分类型-->过滤文件-->设置规则
            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(AssetFormatConst.RULE_ASSET_PATH);
            ResMd resMd = new ResMd();
            resMd.ParseText(textAsset.text);

            EdNode ruleNode = resMd._root_node.GetNode("Rule");
            List<EdNode> nodes = ruleNode.Nodes;
            int length = nodes.Count;
            for (int i = 0; i < length; i++)
            {
                EdNode node = nodes[i];
                AssetFormatRule rule = AssetFormatRule.CreateFormatRule(node);
                if (rule != null)
                    rules.Add(rule);
            }

            _texRuleTablePanel.RefreshData(rules);
            _shows.Clear();
            _texShowTable.RefreshData(_shows);
        }

        public void OnFormatBtn(EButton button)
        {
            if (_formatRule == null) return;

            int length = _shows.Count;
            for (int i = 0; i < length; i++)
            {
                TextureFormatInfo info = _shows[i] as TextureFormatInfo;
                Texture2D tex2D = AssetDatabase.LoadAssetAtPath<Texture2D>(info.Path);
                _formatRule.ApplySettings<Texture>(tex2D, info.Path);
                EditorUtility.DisplayProgressBar("格式化纹理", info.Path, (float)(i + 1) / length);
            }
            EditorUtility.ClearProgressBar();
        }

        // 选择某一条具体的规则
        private void OnRuleSelected(object select, int col)
        {
            AssetFormatRule rule = select as AssetFormatRule;
            if (rule == null) return;
            _shows.Clear();
            _formatRule = rule;

            List<string> pathList = EPathHelper.GetAssetsPath(rule.FilterPath, true, "*.png");
            int length = pathList.Count;
            for (int i = 0; i < length; i++)
            {
                if (!EPathHelper.IsTexture(pathList[i])) continue;
                _shows.Add(TextureFormatInfo.Create(pathList[i]));
            }
            _texShowTablePanel.RefreshData(_shows);
        }

        // 选择某一条具体的信息
        private void OnInfoSelected(object selected, int col)
        {
            TextureFormatInfo texInfo = selected as TextureFormatInfo;
            if (texInfo == null)
                return;
            UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(texInfo.Path, typeof(UnityEngine.Object));
            EditorGUIUtility.PingObject(obj);
            Selection.activeObject = obj;
        }

        #endregion


    }
}