using System;

namespace Summer.AI
{
    public class BtActionLoopContext : BtActionContext
    {
        internal int _currentCount;

        public BtActionLoopContext()
        {
            _currentCount = 0;
        }
    }

    public class BtActionLoop : BtAction
    {
        public const int INFINITY = -1;
       
       
        //--------------------------------------------------------
        public int _loopCount;
        //--------------------------------------------------------
        public BtActionLoop()
            : base(1)
        {
            _loopCount = INFINITY;
        }
        public BtActionLoop SetLoopCount(int count)
        {
            _loopCount = count;
            return this;
        }
        //-------------------------------------------------------
        protected override bool OnEvaluate(BtWorkingData workData)
        {
            BtActionLoopContext thisContext = GetContext<BtActionLoopContext>(workData);
            bool checkLoopCount = (_loopCount == INFINITY || thisContext._currentCount < _loopCount);
            if (checkLoopCount == false)
            {
                return false;
            }
            if (IsIndexValid(0))
            {
                BtAction node = GetChild<BtAction>(0);
                return node.Evaluate(workData);
            }
            return false;
        }
        protected override int OnUpdate(BtWorkingData workData)
        {
            BtActionLoopContext thisContext = GetContext<BtActionLoopContext>(workData);
            int runningStatus = BtRunningStatus.FINISHED;
            if (IsIndexValid(0))
            {
                BtAction node = GetChild<BtAction>(0);
                runningStatus = node.Update(workData);
                if (BtRunningStatus.IsFinished(runningStatus))
                {
                    thisContext._currentCount++;
                    if (thisContext._currentCount < _loopCount || _loopCount == INFINITY)
                    {
                        runningStatus = BtRunningStatus.EXECUTING;
                    }
                }
            }
            return runningStatus;
        }
        protected override void OnTransition(BtWorkingData workData)
        {
            BtActionLoopContext thisContext = GetContext<BtActionLoopContext>(workData);
            if (IsIndexValid(0))
            {
                BtAction node = GetChild<BtAction>(0);
                node.Transition(workData);
            }
            thisContext._currentCount = 0;
        }
    }
}
