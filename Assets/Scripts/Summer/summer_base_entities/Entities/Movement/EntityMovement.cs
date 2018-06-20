using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Summer
{
    /// <summary>
    /// TODO 一个严重bug 就是一切的基础是以原点作为基础，一旦摄像头错误了。那么就会导致所有方向都有问题
    /// </summary>
    public class EntityMovement : MonoBehaviour, I_RegisterHandler
    {
        #region 属性

        public BaseEntity _base_entity;

        public const float NAVMESH_RADIUS = 0.5f;
        public float capsule_collider_radius = 0.51f;                       // Capsule Collider半径
        //public bool _reach_target_pos;                                    // 达到目的地
        //public Vector3 move_velocity = Vector3.one;                       // 当前速度
        public List<Vector3> directions = new List<Vector3>();
        [HideInInspector]
        public Vector3 _target_direction;                                   // 目标方向
        public float turn_smoothing = 10;
        public float movespeed = 5;
        public Transform trans;
        public I_Move _move_component;
        public I_Move direction_move = new DirectionMove();
        public I_Move target_pos_move = new TargetPosMove();
        #endregion

        #region 构造

        private void Awake()
        {
            trans = transform;
            Vector3 cur_position = trans.position;
            trans.position = NavMeshHelper.MakeReasonablePos(cur_position);
            _last_time = Time.realtimeSinceStartup;
        }


        public float _last_time;
        private void Update()
        {
            //OnUpdate(Time.deltaTime);
        }

        #endregion

        public void OnUpdate(float dt)
        {

            bool result = false;
            if (_move_component != null)
            {
                result = _move_component.OnMove(this, dt);
            }


            if (result)
            {
                _move_component = null;
            }

        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public void OnRotating(Vector3 target_direction)
        {
            /*Quaternion target_rotation = Quaternion.LookRotation(target_direction, Vector3.up);
            // 根据玩家的旋转创建一个更接近目标旋转的增量旋转。
            Quaternion new_rotation = Quaternion.Lerp(trans.rotation, target_rotation, turn_smoothing * Time.deltaTime);
            trans.rotation = new_rotation;*/

            /*if (move_direction != trans.eulerAngles)
            {
                trans.rotation = Quaternion.Lerp(trans.localRotation, Quaternion.LookRotation(move_direction), Time.deltaTime * turn_smoothing);
            }*/

            Quaternion target_rotation = Quaternion.LookRotation(target_direction, Vector3.up);
            trans.rotation = target_rotation;
        }

        #region 驱动移动 1.目标方向 2.目标点

        public void AddDirection(Vector2 target_direction)
        {
            if (_base_entity.GetState() != E_StateId.move)
                EntityEventFactory.ChangeInEntityState(_base_entity, E_StateId.move);
            //_reach_target_pos = true;
            DirectionMove move = direction_move as DirectionMove;
            move.move_direction = new Vector3(target_direction.x, 0, target_direction.y);
            _move_component = direction_move;
        }


        public void MoveToTargetPostion(Vector3 targtet_position, float speed)
        {
            /*if (_entity.GetState() != E_StateId.move)
                EntityEventFactory.ChangeInEntityState(_entity, E_StateId.move);*/

            Vector3 target = NavMeshHelper.MakeReasonablePos(targtet_position);
            TargetPosMove pos_move = target_pos_move as TargetPosMove;
            pos_move.target = target;
            pos_move.speed = speed;
            _move_component = target_pos_move;
        }

        /*public void AddTargetPosition(Vector3 target_pos) { }*/

        #endregion

        #region private 

        public void _update_direction()
        {
            //trans.rotation = Quaternion.LookRotation(move_direction);

            //trans.eulerAngles = move_direction;
            /*if (move_direction != trans.eulerAngles)
            {
                trans.rotation = Quaternion.Lerp(trans.localRotation, Quaternion.LookRotation(move_direction), Time.deltaTime * turn_smoothing);
            }*/


            /*Vector3 local_dir = Camera.main.transform.TransformDirection(move_direction);
            if (local_dir != trans.eulerAngles)
            {
                trans.rotation = Quaternion.Lerp(trans.rotation, Quaternion.LookRotation(local_dir), Time.deltaTime * turn_smoothing);
            }*/

        }

        #endregion

        public void OnRegisterHandler()
        {
            _base_entity.RegisterHandler(E_EntityInTrigger.move_to_target_position, OnMoveToTargetPostion);
        }

        public void UnRegisterHandler()
        {
            throw new System.NotImplementedException();
        }

        public void OnMoveToTargetPostion(EventSetData param)
        {
            MoveToTargetPositionData data = param as MoveToTargetPositionData;
            if (data == null) return;

            /*Vector3 entity_position = _base_entity.WroldPosition;
            int length = _base_entity._targets.Count;
            for (int i = 0; i < length; i++)
            {
                BaseEntity target = _base_entity._targets[i];

                Vector3 target_postion = target.WroldPosition;
                Vector3 direction = target_postion - entity_position;

                direction.y = 0;
                Vector3 distance = direction.normalized * data.distance;
                target._movement.MoveToTargetPostion(target_postion + distance, data.speed);
            }*/
        }
    }

    public interface I_Move
    {
        bool OnMove(EntityMovement entity_movement, float dt);
    }


    public class DirectionMove : I_Move
    {
        [HideInInspector]
        public Vector3 move_direction = Vector3.zero;                       // 键盘目标方向
        public bool OnMove(EntityMovement entity_movement, float dt)
        {
            if (entity_movement._base_entity.GetState() != E_StateId.move) return true;

            // 1.是否到达目的地
            //if (!_reach_target_pos) return;
            if (move_direction == Vector3.zero)
            {
                EntityEventFactory.ChangeInEntityState(entity_movement._base_entity, E_StateId.idle);
                return true;
            }
            Vector3 cur_position = entity_movement.trans.position;
            //cur_position = NavMeshHelper.MakeReasonablePos(cur_position);

            //float target_height = NavMeshHelper.GetGroundHeightY(cur_position + NAVMESH_RADIUS * move_direction);
            //if (MathHelper.IsEqual(target_height, MovementConst.error_height))
            //    return;

            // 2.移动的距离
            Vector3 distance = move_direction * dt * entity_movement.movespeed;
            //Vector3 normal = trans.forward.normalized * distance.magnitude;

            //Vector3 tp = Quaternion.Euler(move_direction)*distance;
            // 3.下一个位置
            Vector3 next_pos = cur_position + distance;

            /* float height = NavMeshHelper.GetGroundHeightY(next_pos);
             if (!MathHelper.IsEqual(height, MovementConst.error_height))
                 next_pos.y = height;*/

            next_pos = NavMeshHelper.MakeReasonablePos(next_pos);
            entity_movement.trans.position = next_pos;
            // _update_direction();
            entity_movement.OnRotating(move_direction);
            move_direction = Vector3.zero;
            /*if (!NavMesh.CalculatePath(cur_position, next_pos, NavMesh.AllAreas, nav_path))
            {
                return;
            }
            if (nav_path.corners.Length < 2)
            {
                if (nav_path.corners.Length == 1)   //有时会寻路出一个点的情况，这个还没好好研究  
                {
                    distance = nav_path.corners[0] - cur_position;
                    distance = distance.normalized * dt * 5;
                    next_pos = cur_position + distance;
                    trans.position = next_pos;
                }
                else
                {
                    //m_movement = Vector3.zero;  

                }
            }
            else
            {
                distance = nav_path.corners[1] - cur_position;  //调整方向  
                distance = distance.normalized * dt * 5;

                next_pos = cur_position + distance;

                trans.position = next_pos;
            }*/
            return false;
        }
    }

    public class TargetPosMove : I_Move
    {
        public const float MIN_DISTANCE = 0.1f;
        public Vector3 target;
        public float speed;
        public bool OnMove(EntityMovement entity_movement, float dt)
        {
            Vector3 move_to_pos = Vector3.MoveTowards(entity_movement.trans.position, target, speed * Time.deltaTime);
            entity_movement.trans.position = move_to_pos;


            var distance = (entity_movement.trans.position - target).magnitude;
            if (distance <= 0.01f)
            {
                EntityEventFactory.ChangeInEntityState(entity_movement._base_entity, E_StateId.idle);
                return true;
            }
            return false;
        }
    }
}
