
using Summer.Test;

namespace Summer.AI
{
    public class AiUserContextData
    {
        public float attacking_time;
    }

    public class BtEntityAttackNode : BtActionLeaf
    {
        private const float DEFAULT_WAITING_TIME = 5f;
        protected override void OnEnter(BtWorkingData work_data)
        {
            /*AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            AiUserContextData user_data = GetUserContextData<AiUserContextData>(work_data);
            user_data.attacking_time = DEFAULT_WAITING_TIME;

            this_data.EntityAnimator.CrossFade("attack", 0.2f);*/
            
            // 播放攻击动作
        }

        protected override int OnExecute(BtWorkingData work_data)
        {



            return BtRunningStatus.EXECUTING;
        }
    }
}

