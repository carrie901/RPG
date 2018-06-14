using System.Collections.Generic;
using System.Text;

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
    /// 
    /// TODO 2018.4.10
    ///     1.接受某一个事件，开始执行节点，节点执行完毕跳转到下一个节点等待新的接受新的时间
    ///     2.如果节点执行完毕之后，已经没有下一个节点。那么就跳出执行线
    ///     3.一个技能可能有多个流程线执行
    /// </summary>



    /// <summary>
    /// 序列节点
    ///     1.进行节点，直接执行叶子动作，
    ///         1.1如果是默认动作，直接跳转到下一个节点
    ///         1.2如果有接受事件。那么只有接受到指定事件之后，才会跳转到下一个节点中去
    ///2018.6.14 把节点从接受事件修改为触发前置条件
    ///     触发前置条件，可以是接受事件也可以是时间到达等等 
    ///     写了一半太麻烦
    /// 
    /// BUG 可能涉及到过度一帧的问题
    /// </summary>
    public class SkillNode
    {
        #region 运行状态的枚举

        public const int RUNING_NONE = 0;                       // 无状态
        public const int RUNING_START = 1;                      // 处于当前检测状态
        public const int RUNING_ENTER = 2;                      // Enter--->Update-->Exit
        public const int RUNING_UPDATE = 3;
        public const int RUNING_EXIT = 4;

        #endregion

        #region 属性

        public StringBuilder _des;                                                  //  描述
        public List<SkillLeafNode> _actions = new List<SkillLeafNode>(16);          //  这个节点下叶子节点
        public E_SkillTransition _start_transition = E_SkillTransition.start;       //  执行这个节点的开始运行的事件 目前只接受一个事件 默认情况下接受start事件
        //public E_SkillTransition _finish_transition = E_SkillTransition.start;
        public SkillSequence _parent_node;                                          //  属性某一个流程
        public int _runing_state;                                                   //  运行状态 0

        #endregion

        #region 构造

        public SkillNode()
        {
            _runing_state = RUNING_NONE;
        }

        public SkillNode(E_SkillTransition transition_event)
        {
            _runing_state = RUNING_NONE;
            AddTransitionEvent(transition_event);
        }

        #endregion

        #region public 

        public void SetParent(SkillSequence parent)
        {
            _parent_node = parent;
        }

        public void AddAction(SkillLeafNode action)
        {
            _actions.Add(action);
            action.BindingContext(this);
            if (LogManager.open_skill)
            {
                if (_des == null)
                    _des = new StringBuilder();
                _des.AppendFormat("{0}. {1}     ", _actions.Count, action.ToDes());
            }
        }

        // 添加触发事件
        public void AddTransitionEvent(E_SkillTransition transition_event)
        {
            _start_transition = transition_event;
        }
        // 触发指定事件
        public void ReceiveTransitionEvent(E_SkillTransition transition_event)
        {
            if (transition_event == _start_transition)
            {
                OnEnter();
            }
        }

        public void RaiseEvent(E_EntityInTrigger key, EventSetData obj_info)
        {
            // 父节点-->流程-->容器-->触发器
            _parent_node._skill_container._entity.RaiseEvent(key, obj_info);
            EventDataFactory.Push(obj_info);
        }

        public string ToDes()
        {
            if (_des == null) return string.Empty;
            return _des.ToString();
        }

        #endregion

        #region virtual OnEnter/OnExit/OnUpdate/OnReset

        public virtual void OnStart()
        {
            SkillLog.LogStart(this);
            _runing_state = RUNING_START;
        }
        protected virtual void OnEnter()
        {
            if (_runing_state != RUNING_START)
            {
                SkillLog.Log("OnEnter当前节点状态{0}错误,必须等级1", _runing_state);
                return;
            }
            _runing_state = RUNING_ENTER;

            SkillLog.LogEnter(this);
            // 1.遍历当前节点下的所有叶子节点
            int length = _actions.Count;
            // 所有动作执行的总结果 只有总结过为true的时候才会过度到下一个状态
            bool all_action_result = true;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnEnter(GetBlackboard());
                if (!_actions[i].IsFinish())
                    all_action_result = false;
            }

            // 如果所有叶子节点都已经执行完毕。那么发送结束事件
            if (all_action_result)
            {
                _on_finish_node();
            }
        }
        protected virtual void OnExit()
        {
            if (_runing_state < RUNING_ENTER)
            {
                SkillLog.Log("OnExit当前节点状态{0}错误,必须大于2", _runing_state);
                return;
            }
            _runing_state = RUNING_EXIT;
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnExit(GetBlackboard());
            }
            SkillLog.LogExit(this);
        }
        public virtual void OnUpdate(float dt)
        {
            if (_runing_state < RUNING_ENTER) return;

            _runing_state = RUNING_UPDATE;
            // 1.更新所有节点
            bool all_action_result = true;
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                // 如果节点已经结束就不在运行
                if (_actions[i].IsFinish()) continue;

                _actions[i].OnUpdate(dt, GetBlackboard());

                if (!_actions[i].IsFinish())
                    all_action_result = false;
            }

            if (all_action_result)
                _on_finish_node();
        }
        public virtual void Reset()
        {
            _runing_state = RUNING_NONE;
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].Reset();
            }
        }

        #endregion

        #region private 

        public void _on_finish_node()
        {
            OnExit();
            // 1.跳转到下一个节点
            bool result = _parent_node.DoActionNext();
            if (result)
            {
                // 2.发送默认的开启事件
                _parent_node.ReceiveWithInEvent(E_SkillTransition.start);
            }
        }

        public EntityBlackBoard _entity_blackboard;
        public EntityBlackBoard GetBlackboard()
        {
            if (_entity_blackboard == null)
            {
                _entity_blackboard= _parent_node.GetBlackboard();
            }
            return _entity_blackboard;
        }

        #endregion

        //public virtual void OnFixedUpdate() { }

        //public virtual void OnLateUpdate() { }


    }
}

