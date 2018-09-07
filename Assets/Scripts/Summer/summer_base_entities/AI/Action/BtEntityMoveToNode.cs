

using Summer.Test;
using UnityEngine;

namespace Summer.AI
{


    public class BtEntityMoveToNode : BtActionLeaf
    {
        public const float MIN_DISTANCE = 0.5f;
        protected override void OnEnter(BtWorkingData workData)
        {
            //AIEntityWorkingData working = work_data.As<AIEntityWorkingData>();
            UnityEngine.Debug.Log("播放移动动作");
            // 播放移动动作
        }

        protected override int OnExecute(BtWorkingData workData)
        {
            // 1.得到目标坐标target_pos
            Vector3 targetPos = Vector3.zero;
            // 2.当前坐标current_pos
            Vector3 currentPos = Vector3.zero;
            // 3.距离目标的距离是否小于一定程度
            float distToTarget = MathHelper.Distance2D(targetPos, currentPos);
            if (distToTarget < MIN_DISTANCE)
            {
                return BtRunningStatus.FINISHED;
            }
            else
            {
                int ret = BtRunningStatus.EXECUTING;
                // 下一步移动的距离
                float moveingStep = 0;
                if (moveingStep > distToTarget)
                {
                    ret = BtRunningStatus.FINISHED;
                }
                // 实际移动
                return ret;
            }
        }
    }

}

