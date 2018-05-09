using UnityEngine;
namespace Summer
{
    public class MoveToTargetPosCommand : I_EntityCommnad
    {
        public const float MIN_DISTANCE = 0.1f;
        public Vector3 target;
        public float speed;
        public bool _is_finish;
        public void OnUpdate(BaseEntity entity, float dt)
        {
            if (Finish()) return;
            // TODO 外部改变他的坐标 是否可取
            Vector3 move_to_pos = Vector3.MoveTowards(entity.EntityController.trans.position, target, speed * Time.deltaTime);
            entity.EntityController.trans.position = move_to_pos;


            var distance = (entity.EntityController.trans.position - target).magnitude;
            if (distance <= 0.01f)
            {
                _is_finish = false;
                EntityEventFactory.ChangeInEntityState(entity, E_StateId.idle);
            }
        }

        public bool Finish()
        {
            return _is_finish;
        }
    }
}
