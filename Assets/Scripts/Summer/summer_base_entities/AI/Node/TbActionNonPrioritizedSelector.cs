using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Summer.AI
{
    public class TbActionNonPrioritizedSelector : TbActionPrioritizedSelector
    {
        public TbActionNonPrioritizedSelector() : base() { }
        protected override bool OnEvaluate(TbWorkingData work_data)
        {
            TbActionPrioritizedSelectorContext this_context =
                GetContext<TbActionPrioritizedSelectorContext>(work_data);

            //check last node first
            if (IsIndexValid(this_context.current_selected_index))
            {
                TbAction node = GetChild<TbAction>(this_context.current_selected_index);
                if (node.Evaluate(work_data))
                {
                    return true;
                }
            }
            return base.OnEvaluate(work_data);
        }
    }

}


