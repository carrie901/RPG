
using System.Collections.Generic;


namespace Summer.AI
{
    public class BtTreeNode
    {
        #region 属性

        public const int DEFAULT_CHILD_COUNT = -1;
        public List<BtTreeNode> _childs = new List<BtTreeNode>();
        public int _max_child_count;

        #endregion

        #region 构造

        public BtTreeNode(int max_child_count = -1)
        {
            _max_child_count = max_child_count;
            if (max_child_count >= 0)
            {
                _childs.Capacity = max_child_count;
            }
        }

        public BtTreeNode()
        {
            _max_child_count = DEFAULT_CHILD_COUNT;
            if (_max_child_count >= 0)
            {
                _childs.Capacity = _max_child_count;
            }
        }

        #endregion

        #region public

        public BtTreeNode AddChild(BtTreeNode node)
        {
            if (_max_child_count > 0 && _childs.Count >= _max_child_count)
            {
                LogManager.Error("行为树节点个数已经到达最大数");
                return this;
            }

            _childs.Add(node);
            return this;
        }

        public int GetChildCount() { return _childs.Count; }

        public bool IsIndexValid(int index) { return index >= 0 && index < _childs.Count; }

        public T GetChild<T>(int index) where T : BtTreeNode
        {
            bool result = IsIndexValid(index);
            if (result)
                return _childs[index] as T;
            return null;
        }

        #endregion
    }
}


