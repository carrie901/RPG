
namespace Summer.AI
{
    /// <summary>
    /// 从左到右依次执行所有子节点，只要子节点返回success，它就继续执行后续子节点
    /// 直到有一个节点返回failure或running为止，这时它会停止后续子节点的执行，向父节点返回failure或running
    /// 若所有子节点都返回success，那么它向父节点返回success。
    /// 它与选择节点正好是相反的感觉（选择节点类似于解决怎么进入这个房间如“爆破”、“踹门”
    /// 而顺序节点类似于解决能不能成功做这一件事如“爆破”，我有没有炸弹包，引线，火源）
    /// </summary>
    public class BtActionSequence : BtAction
    {
        public bool _continue_if_error_occors;
        public BtActionSequence()
            : base(-1)
        {
            _continue_if_error_occors = false;
        }
        public BtActionSequence SetContinueIfErrorOccors(bool v)
        {
            _continue_if_error_occors = v;
            return this;
        }

        #region protected OnEvaluate.OnUpdate.OnTransition

        protected override bool OnEvaluate(BtWorkingData work_data)
        {
            BtActionSequenceContext this_context = GetContext<BtActionSequenceContext>(work_data);
            int checked_node_index = IsIndexValid(this_context.current_selected_index) ?
                this_context.current_selected_index : 0;

            if (IsIndexValid(checked_node_index))
            {
                BtAction node = GetChild<BtAction>(checked_node_index);
                if (node.Evaluate(work_data))
                {
                    this_context.current_selected_index = checked_node_index;
                    return true;
                }
            }
            return false;
        }
        protected override int OnUpdate(BtWorkingData work_data)
        {
            BtActionSequenceContext this_context = GetContext<BtActionSequenceContext>(work_data);
            int running_status = BtRunningStatus.FINISHED;

            BtAction node = GetChild<BtAction>(this_context.current_selected_index);
            running_status = node.Update(work_data);
            if (_continue_if_error_occors == false && BtRunningStatus.IsError(running_status))
            {
                this_context.current_selected_index = -1;
                return running_status;
            }
            if (BtRunningStatus.IsFinished(running_status))
            {
                this_context.current_selected_index++;
                if (IsIndexValid(this_context.current_selected_index))
                {
                    running_status = BtRunningStatus.EXECUTING;
                }
                else
                {
                    this_context.current_selected_index = -1;
                }
            }
            return running_status;
        }
        protected override void OnTransition(BtWorkingData work_data)
        {
            BtActionSequenceContext this_context = GetContext<BtActionSequenceContext>(work_data);
            BtAction node = GetChild<BtAction>(this_context.current_selected_index);
            if (node != null)
            {
                node.Transition(work_data);
            }
            this_context.current_selected_index = -1;
        }

        #endregion
    }

    public class BtActionSequenceContext : BtActionContext
    {
        internal int current_selected_index;
        public BtActionSequenceContext()
        {
            current_selected_index = -1;
        }
    }
}
