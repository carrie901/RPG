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

    public class TbActionLoop : TbAction
    {
        public const int INFINITY = -1;
       
       
        //--------------------------------------------------------
        public int loop_count;
        //--------------------------------------------------------
        public TbActionLoop()
            : base(1)
        {
            loop_count = INFINITY;
        }
        public TbActionLoop SetLoopCount(int count)
        {
            loop_count = count;
            return this;
        }
        //-------------------------------------------------------
        protected override bool OnEvaluate(TbWorkingData work_data)
        {
            TbActionLoopContext this_context = GetContext<TbActionLoopContext>(work_data);
            bool check_loop_count = (loop_count == INFINITY || this_context.current_count < loop_count);
            if (check_loop_count == false)
            {
                return false;
            }
            if (IsIndexValid(0))
            {
                TbAction node = GetChild<TbAction>(0);
                return node.Evaluate(work_data);
            }
            return false;
        }
        protected override int OnUpdate(TbWorkingData work_data)
        {
            TbActionLoopContext this_context = GetContext<TbActionLoopContext>(work_data);
            int running_status = TbRunningStatus.FINISHED;
            if (IsIndexValid(0))
            {
                TbAction node = GetChild<TbAction>(0);
                running_status = node.Update(work_data);
                if (TbRunningStatus.IsFinished(running_status))
                {
                    this_context.current_count++;
                    if (this_context.current_count < loop_count || loop_count == INFINITY)
                    {
                        running_status = TbRunningStatus.EXECUTING;
                    }
                }
            }
            return running_status;
        }
        protected override void OnTransition(TbWorkingData work_data)
        {
            TbActionLoopContext this_context = GetContext<TbActionLoopContext>(work_data);
            if (IsIndexValid(0))
            {
                TbAction node = GetChild<TbAction>(0);
                node.Transition(work_data);
            }
            this_context.current_count = 0;
        }
    }
}
