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



            ETreeNodeData data = new ETreeNodeData("Assets");

            ETreeNode node = new ETreeNode(data);
            List<ETreeNodeData> child_node_datas = new List<ETreeNodeData>();

            ExcelAbManager.Read();

            for (int i = 0; i < ExcelAbManager.infos.Count; i++)
            {
                EAbTreeNodeData child_data = new EAbTreeNodeData(ExcelAbManager.infos[i].asset_path, E_TreeNodeType.tree_leaf);
                child_data.info = ExcelAbManager.infos[i];
                child_node_datas.Add(child_data);
            }

            node.AddNodes(child_node_datas);
            _item._node = node;


        }

        void OnGUI()
        {
            _item.OnDraw(20, 20);
        }
    }
}

