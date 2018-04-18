using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    public abstract class FsmState
    {
        #region 属性

        protected Dictionary<E_Transition, E_StateId> map = new Dictionary<E_Transition, E_StateId>();

        protected E_StateId _state_id;
        public E_StateId Id { get { return _state_id; } }

        #endregion

        #region public

        #region 增 / 删 / 查 条件和状态

        public void AddTransition(E_Transition trans, E_StateId id)
        {
            // Check if anyone of the args is invalid
            bool result = LogManager.Assert(trans == E_Transition.null_transition,
                "FsmState Error: NullTransition is not allowed for a real transition");
            if (result) return;


            result = LogManager.Assert(id == E_StateId.null_state_id,
                "FsmState Error: NullStateID is not allowed for a real ID");
            if (result) return;

            if (map.ContainsKey(trans))
            {
                LogManager.Error("FsmState Error: State " + _state_id.ToString() + " already has transition " + trans.ToString() +
                               "Impossible to assign to another state");
                return;
            }

            map.Add(trans, id);
        }

        public void DeleteTransition(E_Transition trans)
        {
            // Check for NullTransition
            if (trans == E_Transition.null_transition)
            {
                LogManager.Error("FsmState ERROR: NullTransition is not allowed");
                return;
            }

            // Check if the pair is inside the map before deleting
            if (map.ContainsKey(trans))
            {
                map.Remove(trans);
                return;
            }
            LogManager.Error("FSMState ERROR: Transition " + trans.ToString() + " passed to " + _state_id.ToString() +
                           " was not on the state's transition list");
        }

        public E_StateId GetOutputState(E_Transition trans)
        {
            // Check if the map has this transition
            if (map.ContainsKey(trans))
            {
                return map[trans];
            }
            return E_StateId.null_state_id;
        }

        #endregion

        #region 状态切换

        /// <summary>
        /// 在进入之前设置状态的条件
        /// 由FsmSystem自动条用
        /// </summary>
        public virtual void DoBeforeEntering() { }

        public virtual void DoBeforeLeaving() { }

        #endregion

        #region Update

        public abstract void Reason(GameObject player, GameObject npc);

        public abstract void Act(GameObject player, GameObject npc);

        #endregion

        #endregion
    }
}

