using System.Collections.Generic;
using System.Text;

namespace Summer
{
    public abstract class StateTransition
    {
        public int _action;
        public ST_Condition _st_cond;
        public bool ConflictWith(StateTransition trans)
        {
            ST_Condition cond1 = _st_cond;
            ST_Condition cond2 = trans._st_cond;
            // 两个condition都不是fullpass，那么没有冲突的可能
            bool atleast_1_fullpass = (cond1 == ST_Condition.FullPass || cond2 == ST_Condition.FullPass);

            List<StateNodeBase> from_list_1 = GetFromNodes();
            List<StateNodeBase> from_list_2 = trans.GetFromNodes();

            StateNodeBase to_1 = GetToNode();
            StateNodeBase to_2 = trans.GetToNode();

            int action_1 = _action;
            int action_2 = trans._action;

            // 两层循环判断冲突
            foreach (var from_i in from_list_1)
            {
                foreach (var from_j in from_list_2)
                {
                    if (from_i == from_j)
                    {
                        // action一样,并且两个condition中至少有一个fullpass
                        if (action_1 == action_2 && atleast_1_fullpass)
                        {
                            LogManager.Warning("transition conflict, from[{0}] and action[{1}] to node[{2},{3}] duplicate!", from_i, action_1, to_1, to_2);
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public abstract List<StateNodeBase> GetFromNodes();
        public abstract StateNodeBase GetToNode();
        public abstract void Destroy();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            //1.from nodes
            sb.Append("from_nodes-[");
            foreach (var node in GetFromNodes())
            {
                sb.AppendFormat("{0}, ", node);
            }
            sb.Append("]");
            sb.Append("    ");

            //2.to node
            sb.AppendFormat("to_node-[{0}]", GetToNode());
            sb.Append("    ");

            //3.action
            sb.AppendFormat("action-[{0}]", _action);

            return sb.ToString();
        }
    }
}

