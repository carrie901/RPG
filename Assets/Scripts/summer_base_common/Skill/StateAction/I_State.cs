using System.Collections.Generic;


namespace Summer
{
    public class SkillState
    {
        public List<ASkillAction> _actions = new List<ASkillAction>(16);
        public string _receive_event;                                      //接受事件的动作
        public StateLink _link;
        public bool _action_result;
        public const string DEAFULT_EVENT = "FINISH";                      //默认事件
        public SkillState(StateLink link)
        {
            _link = link;
        }

        public void AddAction(ASkillAction action)
        {
            _actions.Add(action);
            action.BindingContext(this);
        }

        public void RemoveAction(ASkillAction action)
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

        public void ReceiveEvent(string event_name)
        {
            _excute_finish(event_name);
        }

        #region virtual

        public virtual void OnEnter()
        {
            int length = _actions.Count;
            _action_result = true;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnEnter();
                _record_event_name(_actions[i]);
            }

            if (_receive_event == string.Empty)
                _receive_event = DEAFULT_EVENT;

            _excute_finish(DEAFULT_EVENT);
        }

        public virtual void OnExit()
        {
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnExit();
            }
        }
        public virtual void OnUpdate(float dt)
        {
            _action_result = true;
            int length = _actions.Count;
            for (int i = 0; i < length; i++)
            {
                _actions[i].OnUpdate(dt);

                //检测动作是否完成 TODO 应该可以有更好的方法来修改这一段，比较难阅读 类似&|这样的符号
                if (_action_result)
                    _action_result = _actions[i].IsFinish();
            }

            _excute_finish(DEAFULT_EVENT);
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

        public bool AutoTransNext()
        {
            return true;
        }

        public void _excute_finish(string event_name)
        {
            LogManager.Assert(_action_result, "当前子集合中有未完成的动作");
            if (!_action_result) return;

            if (_receive_event == event_name)
            {
                _link.DoActionNext();
            }

        }

        public void _record_event_name(ASkillAction action)
        {
            bool has_event = action.HasEvent();
            if (has_event)
            {
                LogManager.Assert(_receive_event == string.Empty, "状态下的子动作，只可接受一个事件", action.ToDes());
                _receive_event = action.GetReceiveEvent();
            }

            if (_action_result)
                _action_result = action.IsFinish();
        }

        //public virtual void OnFixedUpdate() { }

        //public virtual void OnLateUpdate() { }
    }
}

