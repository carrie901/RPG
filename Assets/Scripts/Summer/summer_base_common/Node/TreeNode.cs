using System.Collections.Generic;

namespace Summer
{
    public class TreeNode
    {
        #region 属性
        public const int DEFAULT_CHILD_COUNT = -1;                          // 模型大小
        public List<TreeNode> _childs = new List<TreeNode>();               // 子节点列表
        public int _maxChildCount;                                          // 最大
        public string _nodeDes;                                             // 节点描述
        public TreeNode _parentNode;                                        // 节点的父类
        protected int _uniqueKey = 0;                                       // 节点的唯一标志
        #endregion

        #region 构造

        public TreeNode(int maxChildCount = -1)
        {
            _maxChildCount = maxChildCount;
            if (maxChildCount > 0)
            {
                _childs.Capacity = maxChildCount;
            }
            else
            {
                _childs.Capacity = 0;
            }
        }

        #endregion

        #region public


        public TreeNode AddChild(TreeNode node)
        {
            if (_maxChildCount > 0 && _childs.Count >= _maxChildCount)
            {
                LogManager.Error("行为树节点个数已经到达最大数");
                return this;
            }

            LogManager.Assert(node._parentNode == null, "这个节点的爸爸不为空:[{0}]", node._nodeDes);
            node._parentNode = this;
            _childs.Add(node);
            return this;
        }

        public int GetChildCount() { return _childs.Count; }

        public bool IsIndexValid(int index) { return index >= 0 && index < _childs.Count; }

        public T GetChild<T>(int index) where T : TreeNode
        {
            bool result = IsIndexValid(index);
            if (result)
                return _childs[index] as T;
            return null;
        }

        public virtual string ToDes() { return _nodeDes; }

        #endregion

        #region private

        #endregion
    }
}
