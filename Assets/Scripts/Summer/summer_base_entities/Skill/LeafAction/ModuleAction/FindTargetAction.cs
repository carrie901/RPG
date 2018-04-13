using UnityEngine;
namespace Summer
{
    /// <summary>
    /// 查找目标 
    /// TODO 通过依赖连来查找目标  范围/敌友/等等其他条件 一次次把结果传递
    /// </summary>
    public class FindTargetAction : SkillNodeAction
    {
        public const string DES = "==查找目标==";
        //TODO 希望能通过抽象来描述查找目标
        public float radius;        //距离
        public float degree;        //角度
        public override void OnEnter()
        {
            LogEnter();
            float angle = degree / 2;

            BaseEntities base_entity = GetTrigger().GetEntity();
            Vector3 direction = base_entity._entity_controller.Direction;
            Vector3 world_position = base_entity._entity_controller.WroldPosition;

            EntityFindTargetData data = EventDataFactory.Pop<EntityFindTargetData>();
            int length = EntiityControllerManager.Instance.entites.Count;
            for (int i = 0; i < length; i++)
            {
                BaseEntityController controller = EntiityControllerManager.Instance.entites[i];
                if (controller == base_entity._entity_controller) continue;

                float distance = MathHelper.Distance2D(controller.WroldPosition, world_position);
                // 距离大于指定长度
                if (distance > radius) continue;

                Vector3 target_direction = controller.WroldPosition - world_position;

                // 角度小于指定长度
                float tmp_angle = MathHelper.GetAngle04(target_direction, direction);
                if (tmp_angle > angle) continue;
                data._targets.Add(controller);
            }

            RaiseEvent(E_EntityInTrigger.find_targets, data);

            Finish();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
        }

        public override string ToDes()
        {
            return DES;
        }
    }

}
