

using Summer.Test;
using UnityEngine;

namespace Summer.AI
{


    public class BtEntityMoveToNode : BtActionLeaf
    {
        public const float MIN_DISTANCE = 0.5f;
        protected override void OnEnter(BtWorkingData work_data)
        {
            //AIEntityWorkingData working = work_data.As<AIEntityWorkingData>();
            UnityEngine.Debug.Log("播放移动动作");
            // 播放移动动作
        }

        protected override int OnExecute(BtWorkingData work_data)
        {
            // 1.得到目标坐标target_pos
            Vector3 target_pos = Vector3.zero;
            // 2.当前坐标current_pos
            Vector3 current_pos = Vector3.zero;
            // 3.距离目标的距离是否小于一定程度
            float dist_to_target = MathHelper.Distance2D(target_pos, current_pos);
            if (dist_to_target < MIN_DISTANCE)
            {
                return BtRunningStatus.FINISHED;
            }
            else
            {
                int ret = BtRunningStatus.EXECUTING;
                // 下一步移动的距离
                float moveing_step = 0;
                if (moveing_step > dist_to_target)
                {
                    ret = BtRunningStatus.FINISHED;
                }
                // 实际移动
                return ret;
            }
        }
    }

}

