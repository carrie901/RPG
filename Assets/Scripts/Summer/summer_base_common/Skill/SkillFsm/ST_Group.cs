using System.Collections.Generic;
using System.Linq;

namespace Summer
{
    public class ST_Group : StateTransition
    {
        public StateNodeGroup _from_group;
        public StateNodeBase _to_node;

        public List<ST_Single> trans_list = new List<ST_Single>();

        public ST_Group(StateNodeGroup from_group, int action, StateNodeBase to_node)
        {
            LogManager.Assert(!StateNodeGroup.IsNullOrEmpty(from_group), "from node is null or empty");
            LogManager.Assert(to_node != null, "to node is null");
            LogManager.Assert(action > 0, "action <= 0");
            LogManager.Assert(!string.Equals(from_group._name, to_node._name), string.Format("same type of node: [{0}]", from_group._name));

            _from_group = from_group;
            _to_node = to_node;
            _action = action;
            foreach (var node in _from_group)
            {
                trans_list.Add(new ST_Single(node, _to_node, _action));
            }

            _st_cond = ST_Condition.FullPass;
        }

        public override void Destroy()
        {
            _from_group.Destroy();
            _from_group = null;
            _to_node = null;
        }

        public override List<StateNodeBase> GetFromNodes() { return _from_group.ToList(); }
        public override StateNodeBase GetToNode() { return _to_node; }
    }
}