namespace Summer.AI
{
    public class BtActionNonPrioritizedSelector : BtActionPrioritizedSelector
    {
        protected override bool OnEvaluate(BtWorkingData workData)
        {
            BtActionPrioritizedSelectorContext thisContext =
                GetContext<BtActionPrioritizedSelectorContext>(workData);

            //check last node first
            if (IsIndexValid(thisContext._currentSelectedIndex))
            {
                BtAction node = GetChild<BtAction>(thisContext._currentSelectedIndex);
                if (node.Evaluate(workData))
                {
                    return true;
                }
            }
            return base.OnEvaluate(workData);
        }
    }

}


