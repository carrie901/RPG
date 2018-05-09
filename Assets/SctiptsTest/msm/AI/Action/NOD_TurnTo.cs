using Summer.AI;
using Summer.Test;
using UnityEngine;
namespace Summer.Test
{
    public class NOD_TurnTo : TbActionLeaf
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
            if (TMathUtils.IsZero((target_pos - current_pos).sqrMagnitude))
            {
                return TbRunningStatus.FINISHED;
            }
            else
            {
                Vector3 to_target = TMathUtils.GetDirection2D(target_pos, current_pos);
                Vector3 cur_facing = this_data.EntityTrans.forward;
                float dot_v = Vector3.Dot(to_target, cur_facing);
                float delta_angle = Mathf.Acos(Mathf.Clamp(dot_v, -1f, 1f));
                if (delta_angle < 0.1f)
                {
                    this_data.EntityTrans.forward = to_target;
                    return TbRunningStatus.FINISHED;
                }
                else
                {
                    Vector3 cross_v = Vector3.Cross(cur_facing, to_target);
                    float angle_to_turn = Mathf.Min(3f * this_data.DeltaTime, delta_angle);
                    if (cross_v.y < 0)
                    {
                        angle_to_turn = -angle_to_turn;
                    }
                    this_data.EntityTrans.Rotate(Vector3.up, angle_to_turn * Mathf.Rad2Deg, Space.World);
                }
            }
            return TbRunningStatus.EXECUTING;
        }
    }
}