using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
namespace SummerEditor
{
    public class AssetBundleWindow : EditorWindow
    {

        protected static AssetBundleWindow window;     // 自定义窗体
        protected static AssetBundleWindow _instance = new AssetBundleWindow();

        public ETreeNodeItem _item;
        public EOpenList open_list;

        [MenuItem("Tools/构建树视图")]
        static void Init()
        {
            window = EditorWindow.GetWindow<AssetBundleWindow>();   // 创建自定义窗体
            window.titleContent = new GUIContent("构建树视图");         // 窗口的标题
            window.Show();
            _instance.GetAssets();
            // 创建树
        }

        private void GetAssets()
        {
            _item = new ETreeNodeItem(500, 400);
            _item.ResetPosition(20, 45);

            open_list = new EOpenList(500, 25);
            open_list.ResetPosition(open_list.Ew / 2, open_list.Eh / 2);

            ETreeNodeData data = new ETreeNodeData("Assets");

            ETreeNode node = new ETreeNode(data);
            List<ETreeNodeData> child_node_datas = new List<ETreeNodeData>();

            ExcelAbManager.Read();

            for (int i = 0; i < ExcelAbManager.infos.Count; i++)
            {
                ETreeNodeData child_data = new ETreeNodeData(ExcelAbManager.infos[i].asset_path, E_TreeNodeType.tree_leaf);
                child_data.info = ExcelAbManager.infos[i];
                child_node_datas.Add(child_data);
            }

            node.AddNodes(child_node_datas);
            _item._node = node;


        }

        void OnGUI()
        {
            open_list.OnDraw(20, 20);
            _item.OnDraw(20, 40);
        }
    }
}

