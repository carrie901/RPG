
namespace Summer.AI
{
    public class AiUserContextData
    {
        public float _attackingTime;
    }

    public class BtEntityAttackNode : BtActionLeaf
    {

        protected AiUserContextData _userData;

        private const float DefaultWaitingTime = 5f;
        protected override void OnEnter(BtWorkingData workData)
        {
            //AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            _userData = GetUserContextData<AiUserContextData>(workData);
            _userData._attackingTime = DefaultWaitingTime;
            LogManager.Log("播放攻击动画OnEnter,[{0}]", TimeModule.RealtimeSinceStartup);
            //this_data.EntityAnimator.CrossFade("attack", 0.2f);

            // 播放攻击动作
        }

        protected override int OnExecute(BtWorkingData workData)
        {
            AIEntityWorkingData thisData = workData.As<AIEntityWorkingData>();
            if (_userData._attackingTime > 0)
            {
                _userData._attackingTime -= thisData.DeltaTime;
                if (_userData._attackingTime < 0)
                {
                    LogManager.Log("播放攻击动画OnExecute,[{0}]", TimeModule.RealtimeSinceStartup);
                }
            }
            return BtRunningStatus.EXECUTING;
        }
    }
}

