using System;

namespace Summer.AI
{
    public class TbActionLoopContext : TbActionContext
    {
        internal int current_count;

        public TbActionLoopContext()
        {
            current_count = 0;
        }
    }

    public class BtActionLoop : BtAction
    {
        public const int INFINITY = -1;
       
       
        //--------------------------------------------------------
        public int loop_count;
        //--------------------------------------------------------
        public BtActionLoop()
            : base(1)
        {
            loop_count = INFINITY;
        }
        public BtActionLoop SetLoopCount(int count)
        {
            loop_count = count;
            return this;
        }
        //-------------------------------------------------------
        protected override bool OnEvaluate(BtWorkingData work_data)
        {
            TbActionLoopContext this_context = GetContext<TbActionLoopContext>(work_data);
            bool check_loop_count = (loop_count == INFINITY || this_context.current_count < loop_count);
            if (check_loop_count == false)
            {
                return false;
            }
            if (IsIndexValid(0))
            {
                BtAction node = GetChild<BtAction>(0);
                return node.Evaluate(work_data);
            }
            return false;
        }
        protected override int OnUpdate(BtWorkingData work_data)
        {
            TbActionLoopContext this_context = GetContext<TbActionLoopContext>(work_data);
            int running_status = BtRunningStatus.FINISHED;
            if (IsIndexValid(0))
            {
                BtAction node = GetChild<BtAction>(0);
                running_status = node.Update(work_data);
                if (BtRunningStatus.IsFinished(running_status))
                {
                    this_context.current_count++;
                    if (this_context.current_count < loop_count || loop_count == INFINITY)
                    {
                        running_status = BtRunningStatus.EXECUTING;
                    }
                }
            }
            return running_status;
        }
        protected override void OnTransition(BtWorkingData work_data)
        {
            TbActionLoopContext this_context = GetContext<TbActionLoopContext>(work_data);
            if (IsIndexValid(0))
            {
                BtAction node = GetChild<BtAction>(0);
                node.Transition(work_data);
            }
            this_context.current_count = 0;
        }
    }
}
