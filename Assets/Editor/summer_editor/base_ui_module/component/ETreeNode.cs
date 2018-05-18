using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public enum E_TreeNodeType
    {
        tree_leaf,                              // 节点
        tree_container                          // 容器
    }

    // 渲染节点
    public class ETreeNode
    {
        public ETreeNodeData _data;                                                             // 当前节点数据
        public ETreeNode _parent;                                                               // 父节点
        public List<ETreeNode> _children = new List<ETreeNode>();                               // 子节点
        public bool is_open;                                                                    // 是否打开
        protected int _node_depth = 0;

        public ETreeNode(ETreeNodeData data)
        {
            _data = data;
            _node_depth = 0;
        }

        public void AddNodes(List<ETreeNodeData> datas)
        {

            int length = datas.Count;
            for (int i = 0; i < length; i++)
            {
                _add_nodes(datas[i]);
            }
            // 对所有节点进行排序 一次遍历下去
            SortNode();
        }

        public virtual float TreeNodeWith()
        {
            return 500;
        }

        public void _add_nodes(ETreeNodeData data)
        {
            List<string> segments = new List<string>(data.NodeName.Split('/'));
            if (segments.Count == 0)
            {
                Debug.LogError("路径不符合要求," + data.NodeName);
                return;
            }
            string find_node = segments[0];
            segments.RemoveAt(0);

            if (find_node != _data.draw_name)
            {
                Debug.LogError("跟目录不符合要求" + data.NodeName);
            }
            _add_node(segments, data);
        }

        public void SortNode()
        {

        }

        public void _add_node(List<string> nodes, ETreeNodeData data)
        {
            if (nodes.Count == 0)
            {
                Debug.LogError("_add_node" + data.NodeName);
                return;
            }
            else if (nodes.Count == 1)
            {
                if (HasNode(nodes[0]))
                {
                    Debug.LogError("已经存在了这样的节点:" + data.NodeName);
                    return;
                }
                ETreeNode new_node = new ETreeNode(data);
                _insert_node(new_node);
                return;
            }
            else
            {
                string find_node = nodes[0];
                nodes.RemoveAt(0);

                ETreeNode tree_node = _find_node(find_node);
                tree_node._add_node(nodes, data);
            }
        }

        public ETreeNode _find_node(string node_name)
        {
            int length = _children.Count;
            for (int i = 0; i < length; i++)
            {
                if (_children[i]._data.NodeName == node_name)
                    return _children[i];
            }
            ETreeNode new_node = new ETreeNode(new ETreeNodeData(node_name));
            _insert_node(new_node);
            return new_node;
        }

        public bool HasNode(string node_name)
        {
            int length = _children.Count;
            for (int i = 0; i < length; i++)
            {
                if (_children[i]._data.NodeName == node_name)
                    return true;
            }
            return false;
        }

        public void _insert_node(ETreeNode node)
        {
            _children.Add(node);
            node._parent = this;
            node._node_depth = _node_depth + 1;
        }

        public void OnDraw(float parent_x, float parent_y, ref int level)
        {
            float offset_x = node_depth_w * _node_depth;
            Rect rect = new Rect(parent_x + offset_x, parent_y + level * node_h, TreeNodeWith() - offset_x, node_h);
            if (_data.NodeType == E_TreeNodeType.tree_container)
            {
                is_open = EditorGUI.Foldout(rect, is_open, _data.draw_name + "(" + _node_depth + ")", true);
            }
            else
            {
                _on_draw(rect);
            }
            level++;
            if (!is_open || _children.Count == 0) return;
            int length = _children.Count;
            for (int i = 0; i < length; i++)
            {
                _children[i].OnDraw(parent_x, parent_y, ref level);
            }
        }

        public static float node_depth_w = 20;
        public static float node_h = 20;

        public  void _on_draw(Rect rect)
        {
            if (GUI.Button(rect, _data.draw_name/*, GetGuiStyle()*/))
            {
                Debug.Log("选中:" + _data.draw_name);
            }
        }

        public GUIStyle _style;
        public GUIStyle GetGuiStyle()
        {
            if (_style == null)
            {
                _style = new GUIStyle();
                _style.normal.background = null;
            }
            _style.normal.textColor = Color.white;


            return _style;
        }
    }

    /// <summary>
    /// 节点数据
    /// </summary>
    public class ETreeNodeData
    {
        public string NodeName { get { return _node_name; } }
        public string _node_name;

        public string draw_name;

        public E_TreeNodeType NodeType { get { return _node_type; } }
        public E_TreeNodeType _node_type;

        public ETreeNodeData(string node_name, E_TreeNodeType node_type = E_TreeNodeType.tree_container)
        {
            _node_name = node_name;
            _node_type = node_type;
            string[] results = node_name.Split('/');
            draw_name = results[results.Length - 1];
        }
    }


    public class EAbTreeNodeData : ETreeNodeData
    {
        public ExcelAbInfo info;
        public EAbTreeNodeData(string node_name, E_TreeNodeType node_type = E_TreeNodeType.tree_container) : base(node_name, node_type)
        {
        }
    }

    public class ETreeNodeItem : ERect
    {
        public ETreeNode _node;
        public int level = 0;
        public ETreeNodeItem(float width, float height) : base(width, height)
        {
        }

        public override void _on_draw()
        {
            level = 0;
            _node.OnDraw(Ex, Ey, ref level);
        }
    }


}

