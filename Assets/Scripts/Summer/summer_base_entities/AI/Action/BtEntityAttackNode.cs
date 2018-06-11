


namespace Summer.AI
{
    public class AiUserContextData
    {
        public float attacking_time;
    }

    public class BtEntityAttackNode : BtActionLeaf
    {

        protected AiUserContextData user_data;

        private const float DEFAULT_WAITING_TIME = 5f;
        protected override void OnEnter(BtWorkingData work_data)
        {
            AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            user_data = GetUserContextData<AiUserContextData>(work_data);
            user_data.attacking_time = DEFAULT_WAITING_TIME;
            LogManager.Log("播放攻击动画OnEnter,[{0}]",TimeManager.RealtimeSinceStartup);
            //this_data.EntityAnimator.CrossFade("attack", 0.2f);

            // 播放攻击动作
        }

        protected override int OnExecute(BtWorkingData work_data)
        {
            AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            if (user_data.attacking_time > 0)
            {
                user_data.attacking_time -= this_data.DeltaTime;
                if (user_data.attacking_time < 0)
                {
                    LogManager.Log("播放攻击动画OnExecute,[{0}]", TimeManager.RealtimeSinceStartup);
                }
            }
            return BtRunningStatus.EXECUTING;
        }
    }
}

