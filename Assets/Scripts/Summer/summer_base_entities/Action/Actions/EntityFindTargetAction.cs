using UnityEngine;

namespace Summer
{
    public class EntityFindTargetAction : I_EntityAction
    {
        public void OnAction(BaseEntity entity, EventSetData param)
        {
            EntityFindTargetData data = param as EntityFindTargetData;
            if (data == null) return;

            float angle = data.degree / 2;

            // 1.得到自身的方向和世界坐标
            Vector3 direction = entity.Direction;
            Vector3 world_position = entity.WroldPosition;

            int length = EntitesManager.Instance.entites.Count;
            for (int i = 0; i < length; i++)
            {
                BaseEntity tmp_entity = EntitesManager.Instance.entites[i];
                if (tmp_entity == entity) continue;
                // 2.双方之间的距离
                float distance = MathHelper.Distance2D(tmp_entity.WroldPosition, world_position);
                // 3.距离大于指定长度
                if (distance > data.radius) continue;
                // 4.自己和目标之间的方向
                Vector3 target_direction = tmp_entity.WroldPosition - world_position;
                // 5.角度小于指定长度 敌我方向和自身的正前方之间的夹角
                float tmp_angle = MathHelper.GetAngle04(target_direction, direction);
                if (tmp_angle > angle) continue;
                // 添加到目标
                entity._targets.Add(tmp_entity);
                ActionLog.Log("找到目标:{0}", tmp_entity.EntityController.gameObject.name);
            }
        }
    }
}


