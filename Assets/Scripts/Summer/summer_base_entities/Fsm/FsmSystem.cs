using System.Collections.Generic;

namespace Summer
{
    public class FsmSystem : I_Update
    {
        #region 属性

        protected List<FsmState> states = new List<FsmState>();

        // 可以改变FSM状态的唯一方法就是执行转换
        // 不要直接改变CurrentState
        protected E_StateId _current_state_id;
        public E_StateId CurrentStateId { get { return _current_state_id; } }

        private FsmState _current_state;
        public FsmState CurrentState { get { return _current_state; } }

        #endregion

        #region 构造

        public FsmSystem() { }

        #endregion

        public void Start()
        {
            PerformTransition(E_StateId.idle);
        }

        public void AddState(FsmState new_state)
        {
            // 安全检测
            if (new_state == null)
            {
                LogManager.Error("FSM ERROR: 新增加的状态为null");
                return;
            }

            // 第一个状态做为默认的初始状态
            if (states.Count == 0)
            {
                states.Add(new_state);
                _current_state = new_state;
                _current_state_id = new_state.Id;
                return;
            }

            // 检测状态是否重复
            int length = states.Count;
            for (int i = 0; i < length; i++)
            {
                if (states[i].Id == new_state.Id)
                {
                    LogManager.Error("FSM ERROR: 添加状态[{0}]失败, 因为状态已经存在了Index:[{1}]", new_state.Id, i);
                    return;
                }
            }
            states.Add(new_state);
        }

        public void DeleteState(E_StateId id)
        {
            // 遍历状态列表，如果存在则移除
            int length = states.Count;
            for (int i = 0; i < length; i++)
            {
                if (states[i].Id == id)
                {
                    states.Remove(states[i]);
                    return;
                }
            }
            LogManager.Error("删除错误: 删除状态[{0}]失败，因为不存在于列表中 ", id);
        }

        public E_StateId GetState()
        {
            return _current_state_id;
        }

        /// <summary>
        /// 根据状态进行转换
        /// </summary>
        public void PerformTransition(E_StateId id)
        {
            if (id == _current_state_id) return;

            _current_state_id = id;
            int length = states.Count;
            for (int i = 0; i < length; i++)
            {
                if (states[i].Id == _current_state_id)
                {
                    // 在设置新的状态之前进行状态的离开处理
                    _current_state.DoBeforeLeaving();
                    // 改变状态
                    _current_state = states[i];
                    // 设置新状态之后，进行状态进入处理
                    _current_state.DoBeforeEntering();
                    break;
                }
            }

        }

        public void OnUpdate(float dt)
        {
            if (_current_state != null)
                _current_state.OnUpdate(dt);
        }
    }
}
