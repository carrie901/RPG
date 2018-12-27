using System.Collections;
using System.Collections.Generic;
using Summer.AI;
using Summer.Test;
using UnityEngine;

namespace Summer.AI
{
    public class BtAttackLeaf : BtActionLeaf
    {
        private const float DefaultWaitingTime = 5f;
        private static readonly string[] ending_anim = new string[] { "back_fall", "right_fall", "left_fall" };
        class UserContextData
        {
            internal float _attackingTime;
        }
        protected override void OnEnter(BtWorkingData workData)
        {
            AIEntityWorkingData thisData = workData.As<AIEntityWorkingData>();
            UserContextData userData = GetUserContextData<UserContextData>(workData);
            userData._attackingTime = DefaultWaitingTime;

            Debug.Log("进入攻击状态，设定Target");
            //this_data.EntityAnimator.CrossFade("attack", 0.2f);
        }
        protected override int OnExecute(BtWorkingData workData)
        {
            AIEntityWorkingData thisData = workData.As<AIEntityWorkingData>();
            UserContextData userData = GetUserContextData<UserContextData>(workData);
            if (userData._attackingTime > 0)
            {
                userData._attackingTime -= thisData.DeltaTime;
                if (userData._attackingTime <= 0)
                {
                    Debug.Log("再一次攻击");
                    //this_data.EntityAnimator.CrossFade(ending_anim[Random.Range(0, ending_anim.Length)], 0.2f);
                }
            }
            return BtRunningStatus.EXECUTING;
        }
    }
}
