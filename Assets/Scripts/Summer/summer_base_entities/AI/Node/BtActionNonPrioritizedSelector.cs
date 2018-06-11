using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Summer.AI
{
    public class BtActionNonPrioritizedSelector : BtActionPrioritizedSelector
    {
        public BtActionNonPrioritizedSelector() : base() { }
        protected override bool OnEvaluate(BtWorkingData work_data)
        {
            BtActionPrioritizedSelectorContext this_context =
                GetContext<BtActionPrioritizedSelectorContext>(work_data);

            //check last node first
            if (IsIndexValid(this_context.current_selected_index))
            {
                BtAction node = GetChild<BtAction>(this_context.current_selected_index);
                if (node.Evaluate(work_data))
                {
                    return true;
                }
            }
            return base.OnEvaluate(work_data);
        }
    }

}


