using System.Collections.Generic;

namespace Summer
{
    ////////////////////////////////////////////////////////////////////////
    /// Transition Graph
    //////////////////////////////////////////////////////////////////////// 
    //数据结构： {node , { action, transition}}

    //transition set with condition
    public class ST_CondSet
    {
        public Dictionary<StateNodeBase, ST_Condition> _cond_map;
        public ST_CondSet Init()
        {
            _cond_map = new Dictionary<StateNodeBase, ST_Condition>();
            return this;
        }

        public void Destroy()
        {
            _cond_map = null;
        }

        public void Add(ST_Single st_single)
        {
            LogManager.Assert(!_cond_map.ContainsKey(st_single._to_node), string.Format("duplcate add {0}", st_single));
            _cond_map.Add(st_single._to_node, st_single._st_cond);
        }

        public void Remove(ST_Single st_single)
        {
            LogManager.Assert(_cond_map.ContainsKey(st_single._to_node), string.Format("trans not exist! {0}", st_single));
            _cond_map.Remove(st_single._to_node);
        }

        public StateNodeBase GetResult()
        {
            foreach (var v in _cond_map)
            {
                if (v.Value.Valid())
                {
                    return v.Key;
                }
            }
            return null;
        }

        //只add，不remove，
    }

    public class TransitionGraph
    {
        public string _name;

        public Dictionary<StateNodeBase, Dictionary<int, ST_CondSet>> _tran_map;
        public HashSet<ST_Single> _single_tran_set;
        public HashSet<ST_Group> _group_tran_set;

        public TransitionGraph Init(string name)
        {
            _name = name;
            _tran_map = new Dictionary<StateNodeBase, Dictionary<int, ST_CondSet>>();
            _single_tran_set = new HashSet<ST_Single>();
            _group_tran_set = new HashSet<ST_Group>();

            return this;
        }

        public void Destroy()
        {
            _name = null;
            foreach (var st_single in _single_tran_set)
                st_single.Destroy();

            foreach (var st_group in _group_tran_set)
                st_group.Destroy();

            _tran_map.Clear();
            _single_tran_set.Clear();
            _group_tran_set.Clear();
        }

        public void AddTran(StateTransition trans)
        {
            //step1. check conflict
            LogManager.Assert(!_check_conflict(trans), "new trans conflict with other trans");

            //step2. add
            //condition a : single transition
            if (trans is ST_Single)
            {
                ST_Single single_tran = trans as ST_Single;
                _single_tran_set.Add(single_tran);

                _add_single_tran(single_tran);
            }
            //condition b : group transition
            else if (trans is ST_Group)
            {
                ST_Group group_tran = trans as ST_Group;
                _group_tran_set.Add(group_tran);

                foreach (var single_tran in group_tran.trans_list)
                {
                    _add_single_tran(single_tran);
                }
            }
            else
            {
                LogManager.Error("AddTran : transition type [{0}] unhandled", trans.GetType());
            }
        }

        public void RemoveTran(StateTransition trans)
        {
            if (trans is ST_Single)
            {
                ST_Single single_tran = trans as ST_Single;

                //remove from set
                LogManager.Assert(_single_tran_set.Contains(single_tran), "RemoveTran Error11");
                _single_tran_set.Remove(single_tran);

                //remove from map
                _remove_single_tran(single_tran);
            }
            else if (trans is ST_Group)
            {
                ST_Group group_tran = trans as ST_Group;

                //remove from set
                LogManager.Assert(_group_tran_set.Contains(group_tran), "RemoveTran Error22");
                _group_tran_set.Remove(group_tran);

                //remove from map
                foreach (var st_single in group_tran.trans_list)
                {
                    _remove_single_tran(st_single);
                }
            }
            else
            {
                LogManager.Error("Remove Tran : transition type [{0}] unhandled", trans.GetType());
            }
        }

        public bool GetNextTran(StateNodeBase cur_node, int action, out StateNodeBase next_node)
        {
            //find transitions of _cur_node
            Dictionary<int, ST_CondSet> action_trans_map;
            bool suc = _tran_map.TryGetValue(cur_node, out action_trans_map);
            if (!suc)
            {
                LogManager.Warning("transgraph[{0}] : cur_node[{1}] has no transitions", _name, cur_node._name);
                next_node = null;
                return false;
            }
            LogManager.Assert(action_trans_map != null, "GetNextTran Error111");
            //find transition of (_cur_node, action)
            ST_CondSet cond_set;
            suc = action_trans_map.TryGetValue(action, out cond_set);
            if (!suc)
            {
                //                my.warning("_cur_node[{0}]--action[{1}] has no transition", cur_node._name, action);
                next_node = null;
                return false;
            }
            LogManager.Assert(cond_set != null, "GetNextTran Error222");
            next_node = cond_set.GetResult();

            return next_node != null;
        }

        ////////////////////////////////////////////////////////////////////////
        /// Internal
        ////////////////////////////////////////////////////////////////////////
        public bool _check_conflict(StateTransition trans)
        {
            foreach (var st_single in _single_tran_set)
            {
                if (st_single.ConflictWith(trans))
                    return true;
            }
            foreach (var st_group in _group_tran_set)
            {
                if (st_group.ConflictWith(trans))
                    return true;
            }
            return false;
        }

        public void _add_single_tran(ST_Single single_tran)
        {
            //add action_trans_map if not exist
            if (!_tran_map.ContainsKey(single_tran._from_node))
                _tran_map.Add(single_tran._from_node, new Dictionary<int, ST_CondSet>());

            Dictionary<int, ST_CondSet> action_trans_map = _tran_map[single_tran._from_node];
            //add cond_set if not exist
            if (!action_trans_map.ContainsKey(single_tran._action))
                action_trans_map.Add(single_tran._action, new ST_CondSet().Init());

            ST_CondSet cond_set = action_trans_map[single_tran._action];

            //add
            cond_set.Add(single_tran);
        }

        public void _remove_single_tran(ST_Single single_tran)
        {
            //assert from_node exist
            string err_msg = string.Format("Remove: trans[{0}] does not exist!", single_tran);
            LogManager.Assert(_tran_map.ContainsKey(single_tran._from_node), err_msg);

            //assert from_node + action exist
            Dictionary<int, ST_CondSet> action_trans_map = _tran_map[single_tran._from_node];
            LogManager.Assert(action_trans_map.ContainsKey(single_tran._action), err_msg);

            ST_CondSet cond_set = action_trans_map[single_tran._action];
            LogManager.Assert(cond_set != null, "_remove_single_tran Error");

            cond_set.Remove(single_tran);
        }
    }
}
