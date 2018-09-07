

namespace Summer.AI
{

    /// <summary>
    /// 选择（Selector）：选择其子节点的某一个执行
    /// 序列（Sequence）：将其所有子节点依次执行，也就是说当前一个返回“完成”状态后，再运行先一个子节点
    /// 并行（Parallel）：将其所有子节点都运行一遍
    /// </summary>
    public class BtActionPrioritizedSelectorContext : BtActionContext
    {
        internal int _currentSelectedIndex;
        internal int _lastSelectedIndex;

        public BtActionPrioritizedSelectorContext()
        {
            _currentSelectedIndex = -1;
            _lastSelectedIndex = -1;
        }
    }


    /// <summary>
    /// 优先级Selector节点
    /// 从左到右依次执行所有子节点，只要子节点返回failure就继续执行后续子节点
    /// 直到有一个节点返回success或running为止,这时它会停止后续子节点的执行
    /// 向父节点返回success或running。若所有子节点都返回failure，那么它向父节点返回failure。
    /// 当子节点返回running时选择节点会“记录”返回running的这个子节点，下个迭代会直接从该节点开始执行
    /// </summary>
    public class BtActionPrioritizedSelector : BtAction
    {
        public BtActionPrioritizedSelector() : base(-1) { }

        #region protected OnEvaluate.OnUpdate.OnTransition

        protected override bool OnEvaluate(BtWorkingData workData)
        {
            BtActionPrioritizedSelectorContext thisContext = GetContext<BtActionPrioritizedSelectorContext>(workData);
            thisContext._currentSelectedIndex = -1;
            int childCount = GetChildCount();
            for (int i = 0; i < childCount; i++)
            {
                BtAction node = GetChild<BtAction>(i);
                if (node.Evaluate(workData))
                {
                    thisContext._currentSelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        protected override int OnUpdate(BtWorkingData workData)
        {
            BtActionPrioritizedSelectorContext thisContext = GetContext<BtActionPrioritizedSelectorContext>(workData);
            int runningState = BtRunningStatus.FINISHED;
            if (thisContext._currentSelectedIndex != thisContext._lastSelectedIndex)
            {
                if (IsIndexValid(thisContext._lastSelectedIndex))
                {
                    BtAction node = GetChild<BtAction>(thisContext._lastSelectedIndex);
                    node.Transition(workData);
                }
                thisContext._lastSelectedIndex = thisContext._currentSelectedIndex;
            }

            if (IsIndexValid(thisContext._lastSelectedIndex))
            {
                BtAction node = GetChild<BtAction>(thisContext._lastSelectedIndex);
                runningState = node.Update(workData);
                if (BtRunningStatus.IsFinished(runningState))
                {
                    thisContext._lastSelectedIndex = -1;
                }
            }

            return runningState;
        }

        protected override void OnTransition(BtWorkingData workData)
        {
            BtActionPrioritizedSelectorContext thisContext = GetContext<BtActionPrioritizedSelectorContext>(workData);
            BtAction node = GetChild<BtAction>(thisContext._lastSelectedIndex);
            if (node != null)
            {
                node.Transition(workData);
            }
            thisContext._lastSelectedIndex = -1;
        }

        #endregion
    }


}
