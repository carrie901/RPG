
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
        public bool _continueIfErrorOccors;
        public BtActionSequence()
            : base(-1)
        {
            _continueIfErrorOccors = false;
        }
        public BtActionSequence SetContinueIfErrorOccors(bool v)
        {
            _continueIfErrorOccors = v;
            return this;
        }

        #region protected OnEvaluate.OnUpdate.OnTransition

        protected override bool OnEvaluate(BtWorkingData workData)
        {
            BtActionSequenceContext thisContext = GetContext<BtActionSequenceContext>(workData);
            int checkedNodeIndex = IsIndexValid(thisContext.current_selected_index) ?
                thisContext.current_selected_index : 0;

            if (IsIndexValid(checkedNodeIndex))
            {
                BtAction node = GetChild<BtAction>(checkedNodeIndex);
                if (node.Evaluate(workData))
                {
                    thisContext.current_selected_index = checkedNodeIndex;
                    return true;
                }
            }
            return false;
        }
        protected override int OnUpdate(BtWorkingData workData)
        {
            BtActionSequenceContext thisContext = GetContext<BtActionSequenceContext>(workData);
            int runningStatus = BtRunningStatus.FINISHED;

            BtAction node = GetChild<BtAction>(thisContext.current_selected_index);
            runningStatus = node.Update(workData);
            if (_continueIfErrorOccors == false && BtRunningStatus.IsError(runningStatus))
            {
                thisContext.current_selected_index = -1;
                return runningStatus;
            }
            if (BtRunningStatus.IsFinished(runningStatus))
            {
                thisContext.current_selected_index++;
                if (IsIndexValid(thisContext.current_selected_index))
                {
                    runningStatus = BtRunningStatus.EXECUTING;
                }
                else
                {
                    thisContext.current_selected_index = -1;
                }
            }
            return runningStatus;
        }
        protected override void OnTransition(BtWorkingData workData)
        {
            BtActionSequenceContext this_context = GetContext<BtActionSequenceContext>(workData);
            BtAction node = GetChild<BtAction>(this_context.current_selected_index);
            if (node != null)
            {
                node.Transition(workData);
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
