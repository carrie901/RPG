using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class ST_Single : StateTransition
    {
        public StateNodeBase _from_node;
        public StateNodeBase _to_node;

        //from + action是【因】
        //to 是【果】
        public ST_Single(StateNodeBase from_node, StateNodeBase to_node, int action, ST_Condition st_cond = null)
        {
            LogManager.Assert(from_node != null, "from node is null");
            LogManager.Assert(to_node != null, "to node is null");
            LogManager.Assert(action > 0, "action <= 0");

            LogManager.Assert(!string.Equals(from_node._name, to_node._name), string.Format("same type of node: [{0}]", from_node._name));

            _from_node = from_node;
            _to_node = to_node;
            _action = action;

            _st_cond = st_cond ?? ST_Condition.FullPass;
        }

        public override void Destroy()
        {
            _from_node = null;
            _to_node = null;
        }

        public override List<StateNodeBase> GetFromNodes() { return new List<StateNodeBase> { _from_node }; }
        public override StateNodeBase GetToNode() { return _to_node; }
    }
}
