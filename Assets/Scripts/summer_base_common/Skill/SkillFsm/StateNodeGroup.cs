using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Summer
{
    public class StateNodeGroup : IEnumerable<StateNodeBase>
    {
        public string _name;
        public HashSet<StateNodeBase> _node_set = new HashSet<StateNodeBase>();
        public StateNodeGroup(string name, params StateNodeBase[] state_nodes)
        {
            LogManager.Assert(state_nodes.Length > 0, "length of state_node <= 0");
            _name = name;
            _store_in_set(state_nodes);
        }

        public void Destroy()
        {
            _node_set.Clear();
        }

        public void _store_in_set(StateNodeBase[] state_nodes)
        {
            LogManager.Assert(_node_set.Count == 0, "StateNodeGroup duplicate init!");
            foreach (var node in state_nodes)
            {
                LogManager.Assert(!_node_set.Contains(node), string.Format("duplicate node {0}", node._name));
                _node_set.Add(node);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<StateNodeBase> GetEnumerator()
        {
            return _node_set.GetEnumerator();
        }

        public int Count() { return _node_set.Count; }
        public static bool IsNullOrEmpty(StateNodeGroup node_group)
        {
            if (node_group == null)
                return true;
            int length = node_group.Count();
            return length <= 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("node_group-[");
            foreach (var node in _node_set)
            {
                sb.AppendFormat("{0}, ", node);
            }
            sb.Append("]");

            return sb.ToString();
        }
    }
}
