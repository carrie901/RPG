
using System.Collections.Generic;


namespace Summer.AI
{
    public class BtTreeNode
    {
        #region 属性

        public const int DEFAULT_CHILD_COUNT = -1;
        public List<BtTreeNode> _childs = new List<BtTreeNode>();
        public int _maxChildCount;

        #endregion

        #region 构造

        public BtTreeNode(int maxChildCount = -1)
        {
            _maxChildCount = maxChildCount;
            if (maxChildCount >= 0)
            {
                _childs.Capacity = maxChildCount;
            }
        }

        public BtTreeNode()
        {
            _maxChildCount = DEFAULT_CHILD_COUNT;
            if (_maxChildCount >= 0)
            {
                _childs.Capacity = _maxChildCount;
            }
        }

        #endregion

        #region public

        public BtTreeNode AddChild(BtTreeNode node)
        {
            if (_maxChildCount > 0 && _childs.Count >= _maxChildCount)
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


