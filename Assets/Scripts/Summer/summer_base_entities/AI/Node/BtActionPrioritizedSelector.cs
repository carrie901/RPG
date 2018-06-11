using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.AI
{

    /// <summary>
    /// 选择（Selector）：选择其子节点的某一个执行
    /// 序列（Sequence）：将其所有子节点依次执行，也就是说当前一个返回“完成”状态后，再运行先一个子节点
    /// 并行（Parallel）：将其所有子节点都运行一遍
    /// </summary>
    public class BtActionPrioritizedSelectorContext : BtActionContext
    {
        internal int current_selected_index;
        internal int last_selected_index;

        public BtActionPrioritizedSelectorContext()
        {
            current_selected_index = -1;
            last_selected_index = -1;
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

        protected override bool OnEvaluate(BtWorkingData work_data)
        {
            BtActionPrioritizedSelectorContext this_context = GetContext<BtActionPrioritizedSelectorContext>(work_data);
            this_context.current_selected_index = -1;
            int child_count = GetChildCount();
            for (int i = 0; i < child_count; i++)
            {
                BtAction node = GetChild<BtAction>(i);
                if (node.Evaluate(work_data))
                {
                    this_context.current_selected_index = i;
                    return true;
                }
            }
            return false;
        }

        protected override int OnUpdate(BtWorkingData work_data)
        {
            BtActionPrioritizedSelectorContext this_context = GetContext<BtActionPrioritizedSelectorContext>(work_data);
            int running_state = BtRunningStatus.FINISHED;
            if (this_context.current_selected_index != this_context.last_selected_index)
            {
                if (IsIndexValid(this_context.last_selected_index))
                {
                    BtAction node = GetChild<BtAction>(this_context.last_selected_index);
                    node.Transition(work_data);
                }
                this_context.last_selected_index = this_context.current_selected_index;
            }

            if (IsIndexValid(this_context.last_selected_index))
            {
                BtAction node = GetChild<BtAction>(this_context.last_selected_index);
                running_state = node.Update(work_data);
                if (BtRunningStatus.IsFinished(running_state))
                {
                    this_context.last_selected_index = -1;
                }
            }

            return running_state;
        }

        protected override void OnTransition(BtWorkingData work_data)
        {
            BtActionPrioritizedSelectorContext this_context = GetContext<BtActionPrioritizedSelectorContext>(work_data);
            BtAction node = GetChild<BtAction>(this_context.last_selected_index);
            if (node != null)
            {
                node.Transition(work_data);
            }
            this_context.last_selected_index = -1;
        }

        #endregion
    }


}
