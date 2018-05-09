using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.AI
{
    public class TbTreeNode
    {
        #region 属性

        public const int DEFAULT_CHILD_COUNT = -1;
        public List<TbTreeNode> _childs = new List<TbTreeNode>();
        public int _max_child_count;

        #endregion

        #region 构造

        public TbTreeNode(int max_child_count = -1)
        {
            _max_child_count = max_child_count;
            if (max_child_count >= 0)
            {
                _childs.Capacity = max_child_count;
            }
        }

        public TbTreeNode()
        {
            _max_child_count = DEFAULT_CHILD_COUNT;
            if (_max_child_count >= 0)
            {
                _childs.Capacity = _max_child_count;
            }
        }

        #endregion

        #region public

        public TbTreeNode AddChild(TbTreeNode node)
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

        public bool IsIndexValid(int index) { return index > 0 && index < _childs.Count; }

        public T GetChild<T>(int index) where T : TbTreeNode
        {
            bool result = IsIndexValid(index);
            if (!result) return null;

            return _childs[index] as T;
        }

        #endregion
    }
}


