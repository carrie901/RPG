using System.Collections;
using System.Collections.Generic;
using Summer.AI;
using Summer.Test;
using UnityEngine;

namespace Summer.AI
{
    public class BtAttackLeaf : BtActionLeaf
    {
        private const float DEFAULT_WAITING_TIME = 5f;
        private static readonly string[] ending_anim = new string[] { "back_fall", "right_fall", "left_fall" };
        class UserContextData
        {
            internal float attacking_time;
        }
        protected override void OnEnter(BtWorkingData workData)
        {
            AIEntityWorkingData this_data = workData.As<AIEntityWorkingData>();
            UserContextData user_data = GetUserContextData<UserContextData>(workData);
            user_data.attacking_time = DEFAULT_WAITING_TIME;

            Debug.Log("进入攻击状态，设定Target");
            //this_data.EntityAnimator.CrossFade("attack", 0.2f);
        }
        protected override int OnExecute(BtWorkingData workData)
        {
            AIEntityWorkingData this_data = workData.As<AIEntityWorkingData>();
            UserContextData user_data = GetUserContextData<UserContextData>(workData);
            if (user_data.attacking_time > 0)
            {
                user_data.attacking_time -= this_data.DeltaTime;
                if (user_data.attacking_time <= 0)
                {
                    Debug.Log("再一次攻击");
                    //this_data.EntityAnimator.CrossFade(ending_anim[Random.Range(0, ending_anim.Length)], 0.2f);
                }
            }
            return BtRunningStatus.EXECUTING;
        }
    }
}
