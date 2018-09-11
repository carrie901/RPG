/*
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
namespace SummerEditor
{
    /// <summary>
    /// 一个被遗弃的界面
    /// 原始是想统计当前的资源信息，报告类型
    /// 资源名称/资源引用类型/资源被引用数/资源类型/资源自身大小/资源评估内存/引用纹理数 这样的树形结构来
    /// </summary>
    public class AssetBundleWindow : EditorWindow
    {

        //protected static AssetBundleWindow window;     // 自定义窗体
        //protected static AssetBundleWindow _instance = new AssetBundleWindow();

        public ETreeNodeItem _item;
        public EOpenList open_list;
        static float t_width = 1000;
        static float t_height = 500;
        [MenuItem("Tools/构建树视图")]
        static void Init()
        {
           /* window = EditorWindow.GetWindow<AssetBundleWindow>();   // 创建自定义窗体
            window.titleContent = new GUIContent("构建树视图");         // 窗口的标题
            //window.minSize = new Vector2(t_width, t_height);
            //window.maxSize = new Vector2(t_width + 40, t_height + 40);
            window.Show();
            _instance.GetAssets();#1#
            // 创建树
        }

        private void GetAssets()
        {
            _item = new ETreeNodeItem(500, 400);
            _item.ResetPosition(20, 45);

            open_list = new EOpenList(500, 26);
            open_list.ResetPosition(open_list.Ew / 2, open_list.Eh / 2);

            ETreeNodeData data = new ETreeNodeData("Assets");

            EAbTreeNode node = new EAbTreeNode(data);
            List<ETreeNodeData> child_node_datas = new List<ETreeNodeData>();


            for (int i = 0; i < ExcelAbManager.infos.Count; i++)
            {
                ETreeNodeData child_data = new ETreeNodeData(ExcelAbManager.infos[i].asset_path, E_AbTreeNodeType.tree_leaf);
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

*/
