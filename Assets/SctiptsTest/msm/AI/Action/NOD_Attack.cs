using System.Collections;
using System.Collections.Generic;
using Summer.AI;
using Summer.Test;
using UnityEngine;

public class NOD_Attack : TbActionLeaf
{
    private const float DEFAULT_WAITING_TIME = 5f;
    private static readonly string[] ending_anim = new string[] { "back_fall", "right_fall", "left_fall" };
    class UserContextData
    {
        internal float attacking_time;
    }
    protected override void OnEnter(TbWorkingData work_data)
    {
        AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
        UserContextData user_data = GetUserContextData<UserContextData>(work_data);
        user_data.attacking_time = DEFAULT_WAITING_TIME;
        this_data.EntityAnimator.CrossFade("attack", 0.2f);
    }
    protected override int OnExecute(TbWorkingData work_data)
    {
        AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
        UserContextData user_data = GetUserContextData<UserContextData>(work_data);
        if (user_data.attacking_time > 0)
        {
            user_data.attacking_time -= this_data.DeltaTime;
            if (user_data.attacking_time <= 0)
            {
                this_data.EntityAnimator.CrossFade(ending_anim[Random.Range(0, ending_anim.Length)], 0.2f);
            }
        }
        return TbRunningStatus.EXECUTING;
    }
}