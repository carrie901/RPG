using Summer.AI;
using UnityEngine;
namespace Summer.Test
{
    public class NOD_MoveTo : TbActionLeaf
    {
        protected override void OnEnter(TbWorkingData work_data)
        {
            AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            this_data.EntityAnimator.CrossFade("walk", 0.2f);
        }
        protected override int OnExecute(TbWorkingData work_data)
        {
            AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            Vector3 target_pos = TMathUtils.Vector3ZeroY(this_data.Entity.GetBbValue<Vector3>(AIEntity.BBKEY_NEXTMOVINGPOSITION, Vector3.zero));
            Vector3 current_pos = TMathUtils.Vector3ZeroY(this_data.EntityTrans.position);
            float dist_to_target = TMathUtils.GetDistance2D(target_pos, current_pos);
            if (dist_to_target < 1f)
            {
                this_data.EntityTrans.position = target_pos;
                return TbRunningStatus.FINISHED;
            }
            else
            {
                int ret = TbRunningStatus.EXECUTING;
                Vector3 to_target = TMathUtils.GetDirection2D(target_pos, current_pos);
                float moving_step = 0.5f * this_data.DeltaTime;
                if (moving_step > dist_to_target)
                {
                    moving_step = dist_to_target;
                    ret = TbRunningStatus.FINISHED;
                }
                this_data.EntityTrans.position = this_data.EntityTrans.position + to_target * moving_step;
                return ret;
            }
        }
    }
}