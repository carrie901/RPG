using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 两种思路
    ///     1.当前序列节点所有的叶子动作完成了自动跳转到下一个节点中
    ///     2.叶子节点会接受一些事件驱动，才会完成动作
    /// 序列节点之间的跳转判断是依次执行的
    /// 
    /// 每一个序列节点有默认事件Finished，只要当前所有动作完成了就默认跳转到下一个节点中
    /// 如果当前节点有特殊事件(可能是多个事件)    
    ///     接受到特殊事件之后才会跳转
    /// 
    /// 
    /// 两者之间的差别是2是接受到某一个事件之后，跳转到下一个序列节点，所有的东西是外部驱动
    /// 1.跳转到某一个节点之后，只有接受到了某一个事件，才开始执行动作
    /// 
    /// 
    /// 思路好混轮哦
    /// 目前方案执行2
    /// </summary>



    /// <summary>
    /// 序列节点
    ///     1.进行节点，直接执行叶子动作，
    ///         1.1如果是默认动作，直接跳转到下一个节点
    ///         1.2如果有接受事件。那么只有接受到指定事件之后，才会跳转到下一个节点中去
    /// </summary>
    public class SkillNode
    {
        public string _des = string.Empty;
        public List<SkillNodeAction> _actions
            = new List<SkillNodeAction>(16);
        public E_SkillTransitionEvent _transition_event
            = E_SkillTransitionEvent.finish;                            //过度到下一个节点的过度事件 目前只接受一个事件
        public SkillSequence _parent_node;
        public bool _all_action_result;                                 //所有动作执行的总结果 只有总结过为true的时候才会过度到下一个状态
        public SkillNode() { }
        public SkillNode(string des) { _des = des; }

        public void SetParent(SkillSequence parent)
        {
            _parent_node = parent;
        }

        public void AddAction(SkillNodeAction action)
        {
            _actions.Add(action);
            action.BindingContext(this);
        }

        public void RemoveAction(SkillNodeAction action)
        {
            int length = _actions.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (_actions[i] == action)
                {
                    _actions.Remove(_actions[i]);
                }
            }
        }

        public void AddTransitionEvent(E_SkillTransitionEvent transition_event)
        {
            _transition_event = transition_event;
        }

        public void ReceiveTransitionEvent(E_SkillTransitionEvent event_name)
        {
            if (event_name == _transition_event)
                _transition_next_state(event_name);
            else
                LogManager.Log("接收到[{0}],但不跳转序列节点,本状态接受事件为:[{1}]", event_name, _transition_event);
        }

        #region virtual OnEnter/OnExit/OnUpdate/OnReset

        public virtual void OnEnter()
        {
            LogEnter();
            int length = _actions.Count;
            _all_action_result = true;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnEnter();
                if (!_actions[i].IsFinish())
                    _all_action_result = false;
            }
            if (_all_action_result)
                _transition_next_state(E_SkillTransitionEvent.finish);
        }

        public virtual void OnExit()
        {
            LogExit();
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnExit();
            }
        }
        public virtual void OnUpdate(float dt)
        {
            _all_action_result = true;
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnUpdate(dt);

                if (!_actions[i].IsFinish())
                    _all_action_result = false;
            }

            if (_all_action_result)
                _transition_next_state(E_SkillTransitionEvent.finish);
        }
        public virtual void Reset()
        {
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].Reset();
            }
        }

        #endregion

        public string ToDes()
        {
            return _des;
        }

        public void LogEnter()
        {
            LogManager.Log("Time: {1}    Enter [{0}]", ToDes(), _node_time());
        }

        public void LogExit()
        {
            LogManager.Log("Time: {1}    Exit 节点 [{0}]", ToDes(), _node_time());
        }

        public void _transition_next_state(E_SkillTransitionEvent transition_event)
        {
            if (transition_event == _transition_event)
            {
                LogManager.Assert(_all_action_result, "当前子集合中有未完成的动作,{0}", ToDes());
                _parent_node.DoActionNext();
            }

        }

        public string _node_time()
        {
            return TimeManager.FrameCount.ToString();
        }

        //public virtual void OnFixedUpdate() { }

        //public virtual void OnLateUpdate() { }
    }
}

