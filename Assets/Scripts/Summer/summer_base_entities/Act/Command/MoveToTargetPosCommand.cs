using UnityEngine;
namespace Summer
{
    public class MoveToTargetPosCommand : I_EntityCommnad
    {
        public const float MIN_DISTANCE = 0.1f;
        public Vector3 _target;
        public float _speed;
        public bool _isFinish;
        public void OnUpdate(BaseEntity entityMovement, float dt)
        {
            if (Finish()) return;
            // TODO 外部改变他的坐标 是否可取
            Vector3 moveToPos = Vector3.MoveTowards(entityMovement.EntityController.WroldPosition, _target, _speed * Time.deltaTime);
            entityMovement.EntityController._trans.position = moveToPos;


            var distance = (entityMovement.EntityController.WroldPosition - _target).magnitude;
            if (distance <= 0.01f)
            {
                _isFinish = false;
                EntityEventFactory.ChangeInEntityState(entityMovement, E_StateId.idle);
            }
        }

        public bool Finish()
        {
            return _isFinish;
        }
    }
}
