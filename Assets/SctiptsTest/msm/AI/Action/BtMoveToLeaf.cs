using Summer.AI;
using Summer.Test;
using UnityEngine;
namespace Summer.AI
{
    public class BtMoveToLeaf : BtActionLeaf
    {
        protected override void OnEnter(BtWorkingData work_data)
        {
            AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            Vector3 target_pos = this_data.EntityAi.GetBbValue<Vector3>(BtEntityAi.BBKEY_NEXTMOVINGPOSITION, Vector3.zero);
            //this_data.EntityAi.BaseEntity.MoveToTargetPostion(target_pos,1);
            Debug.LogFormat("进入移动状态，设定目标:[{0}]", target_pos);
        }
        protected override int OnExecute(BtWorkingData work_data)
        {
            AIEntityWorkingData this_data = work_data.As<AIEntityWorkingData>();
            Vector3 target_pos = TMathUtils.Vector3ZeroY(this_data.EntityAi.GetBbValue<Vector3>(BtEntityAi.BBKEY_NEXTMOVINGPOSITION, Vector3.zero));
            Vector3 current_pos = TMathUtils.Vector3ZeroY(this_data.EntityAi.BaseEntity.WroldPosition);
            float dist_to_target = TMathUtils.GetDistance2D(target_pos, current_pos);
            if (dist_to_target < 1f)
            {
                Debug.Log("目标足够接近");
                return BtRunningStatus.FINISHED;
            }
            else
            {
                int ret = BtRunningStatus.EXECUTING;
                Vector3 to_target = TMathUtils.GetDirection2D(target_pos, current_pos);
                float moving_step = 0.5f * this_data.DeltaTime;
                if (moving_step > dist_to_target)
                {
                    moving_step = dist_to_target;
                    ret = BtRunningStatus.FINISHED;
                }
                Transform tran = this_data.EntityAi.BaseEntity.EntityController._trans;
                tran.localPosition = tran.localPosition + to_target * moving_step;
                return ret;
            }
        }
    }
}