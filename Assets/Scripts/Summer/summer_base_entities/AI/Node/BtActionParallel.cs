using System.Collections.Generic;

namespace Summer.AI
{
    public class TbActionParallelContext : TbActionContext
    {
        internal List<bool> evaluation_status;
        internal List<int> running_status;

        public TbActionParallelContext()
        {
            evaluation_status = new List<bool>();
            running_status = new List<int>();
        }
    }

    /// <summary>
    /// 有多个子节点，与顺序节点不同的是，这些子节点的执行是并行的——不是一次执行一个
    /// 而是同时执行，直到其中一个返回failure（或全部返回successs）为止。此时，并行节点向父节点返回failu（或success），并终止其它所有子节点的执行。
    /// </summary>
    public class BtActionParallel : BtAction
    {
        public enum ECHILDREN_RELATIONSHIP
        {
            and, or
        }

        protected ECHILDREN_RELATIONSHIP _evaluation_relationship;
        protected ECHILDREN_RELATIONSHIP _runningstatus_relationship;

        public BtActionParallel() : base(-1)
        {
            _evaluation_relationship = ECHILDREN_RELATIONSHIP.and;
            _runningstatus_relationship = ECHILDREN_RELATIONSHIP.or;
        }
        public BtActionParallel SetEvaluationRelationship(ECHILDREN_RELATIONSHIP v)
        {
            _evaluation_relationship = v;
            return this;
        }
        public BtActionParallel SetRunningStatusRelationship(ECHILDREN_RELATIONSHIP v)
        {
            _runningstatus_relationship = v;
            return this;
        }

        #region protected OnEvaluate.OnUpdate.OnTransition

        protected override bool OnEvaluate(BtWorkingData work_data)
        {
            TbActionParallelContext this_context = GetContext<TbActionParallelContext>(work_data);
            _init_list_to(this_context.evaluation_status, false);
            bool final_result = false;
            for (int i = 0; i < GetChildCount(); ++i)
            {
                BtAction node = GetChild<BtAction>(i);
                bool ret = node.Evaluate(work_data);
                //early break
                if (_evaluation_relationship == ECHILDREN_RELATIONSHIP.and && ret == false)
                {
                    final_result = false;
                    break;
                }
                if (ret)
                {
                    final_result = true;
                }
                this_context.evaluation_status[i] = ret;
            }
            return final_result;
        }
        protected override int OnUpdate(BtWorkingData work_data)
        {
            TbActionParallelContext this_context = GetContext<TbActionParallelContext>(work_data);
            //first time initialization
            if (this_context.running_status.Count != GetChildCount())
            {
                _init_list_to(this_context.running_status, BtRunningStatus.EXECUTING);
            }
            bool has_finished = false;
            bool has_executing = false;
            for (int i = 0; i < GetChildCount(); ++i)
            {
                if (this_context.evaluation_status[i] == false)
                {
                    continue;
                }
                if (BtRunningStatus.IsFinished(this_context.running_status[i]))
                {
                    has_finished = true;
                    continue;
                }
                BtAction node = GetChild<BtAction>(i);
                int running_status = node.Update(work_data);
                if (BtRunningStatus.IsFinished(running_status))
                {
                    has_finished = true;
                }
                else
                {
                    has_executing = true;
                }
                this_context.running_status[i] = running_status;
            }
            if (_runningstatus_relationship == ECHILDREN_RELATIONSHIP.or && has_finished || _runningstatus_relationship == ECHILDREN_RELATIONSHIP.and && has_executing == false)
            {
                _init_list_to(this_context.running_status, BtRunningStatus.EXECUTING);
                return BtRunningStatus.FINISHED;
            }
            return BtRunningStatus.EXECUTING;
        }
        protected override void OnTransition(BtWorkingData work_data)
        {
            TbActionParallelContext this_context = GetContext<TbActionParallelContext>(work_data);
            for (int i = 0; i < GetChildCount(); ++i)
            {
                BtAction node = GetChild<BtAction>(i);
                node.Transition(work_data);
            }
            //clear running status
            _init_list_to(this_context.running_status, BtRunningStatus.EXECUTING);
        }

        #endregion

        #region private

        public void _init_list_to<T>(List<T> list, T value)
        {
            int child_count = GetChildCount();
            if (list.Count != child_count)
            {
                list.Clear();
                for (int i = 0; i < child_count; ++i)
                {
                    list.Add(value);
                }
            }
            else
            {
                for (int i = 0; i < child_count; ++i)
                {
                    list[i] = value;
                }
            }
        }

        #endregion
    }
}