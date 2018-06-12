
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 必须要有黑箱存在
    /// </summary>
    public class MoveToTargetPositionAction : I_EntityAction
    {
        public void OnAction(BaseEntity entity, EventSetData param)
        {
            MoveToTargetPositionData data = param as MoveToTargetPositionData;
            if (data == null) return;

            Vector3 entity_position = entity.WroldPosition;
            int length = entity._targets.Count;
            for (int i = 0; i < length; i++)
            {
                BaseEntity target = entity._targets[i];

                Vector3 target_postion = target.WroldPosition;
                Vector3 direction = target_postion - entity_position;

                direction.y = 0;
                Vector3 distance = direction.normalized * data.distance;
                target._movement.MoveToTargetPostion(target_postion + distance, data.speed);
            }
        }
    }
}
