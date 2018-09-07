using System.Collections.Generic;

namespace Summer.AI
{
    public class BtActionParallelContext : BtActionContext
    {
        internal List<bool> _evaluationStatus;
        internal List<int> _runningStatus;

        public BtActionParallelContext()
        {
            _evaluationStatus = new List<bool>();
            _runningStatus = new List<int>();
        }
    }

    /// <summary>
    /// 有多个子节点，与顺序节点不同的是，这些子节点的执行是并行的——不是一次执行一个
    /// 而是同时执行，直到其中一个返回failure（或全部返回successs）为止。此时，并行节点向父节点返回failu（或success），并终止其它所有子节点的执行。
    /// </summary>
    public class BtActionParallel : BtAction
    {
        public enum E_ChildrenRelationShip
        {
            and, or
        }

        protected E_ChildrenRelationShip _evaluationRelationship;
        protected E_ChildrenRelationShip _runningstatusRelationship;

        public BtActionParallel() : base(-1)
        {
            _evaluationRelationship = E_ChildrenRelationShip.and;
            _runningstatusRelationship = E_ChildrenRelationShip.or;
        }
        public BtActionParallel SetEvaluationRelationship(E_ChildrenRelationShip v)
        {
            _evaluationRelationship = v;
            return this;
        }
        public BtActionParallel SetRunningStatusRelationship(E_ChildrenRelationShip v)
        {
            _runningstatusRelationship = v;
            return this;
        }

        #region protected OnEvaluate.OnUpdate.OnTransition

        protected override bool OnEvaluate(BtWorkingData workData)
        {
            BtActionParallelContext thisContext = GetContext<BtActionParallelContext>(workData);
            _init_list_to(thisContext._evaluationStatus, false);
            bool finalResult = false;
            for (int i = 0; i < GetChildCount(); ++i)
            {
                BtAction node = GetChild<BtAction>(i);
                bool ret = node.Evaluate(workData);
                //early break
                if (_evaluationRelationship == E_ChildrenRelationShip.and && ret == false)
                {
                    finalResult = false;
                    break;
                }
                if (ret)
                {
                    finalResult = true;
                }
                thisContext._evaluationStatus[i] = ret;
            }
            return finalResult;
        }
        protected override int OnUpdate(BtWorkingData workData)
        {
            BtActionParallelContext thisContext = GetContext<BtActionParallelContext>(workData);
            //first time initialization
            if (thisContext._runningStatus.Count != GetChildCount())
            {
                _init_list_to(thisContext._runningStatus, BtRunningStatus.EXECUTING);
            }
            bool hasFinished = false;
            bool hasExecuting = false;
            for (int i = 0; i < GetChildCount(); ++i)
            {
                if (thisContext._evaluationStatus[i] == false)
                {
                    continue;
                }
                if (BtRunningStatus.IsFinished(thisContext._runningStatus[i]))
                {
                    hasFinished = true;
                    continue;
                }
                BtAction node = GetChild<BtAction>(i);
                int runningStatus = node.Update(workData);
                if (BtRunningStatus.IsFinished(runningStatus))
                {
                    hasFinished = true;
                }
                else
                {
                    hasExecuting = true;
                }
                thisContext._runningStatus[i] = runningStatus;
            }
            if (_runningstatusRelationship == E_ChildrenRelationShip.or && hasFinished || _runningstatusRelationship == E_ChildrenRelationShip.and && hasExecuting == false)
            {
                _init_list_to(thisContext._runningStatus, BtRunningStatus.EXECUTING);
                return BtRunningStatus.FINISHED;
            }
            return BtRunningStatus.EXECUTING;
        }
        protected override void OnTransition(BtWorkingData workData)
        {
            BtActionParallelContext thisContext = GetContext<BtActionParallelContext>(workData);
            for (int i = 0; i < GetChildCount(); ++i)
            {
                BtAction node = GetChild<BtAction>(i);
                node.Transition(workData);
            }
            //clear running status
            _init_list_to(thisContext._runningStatus, BtRunningStatus.EXECUTING);
        }

        #endregion

        #region private

        public void _init_list_to<T>(List<T> list, T value)
        {
            int childCount = GetChildCount();
            if (list.Count != childCount)
            {
                list.Clear();
                for (int i = 0; i < childCount; ++i)
                {
                    list.Add(value);
                }
            }
            else
            {
                for (int i = 0; i < childCount; ++i)
                {
                    list[i] = value;
                }
            }
        }

        #endregion
    }
}