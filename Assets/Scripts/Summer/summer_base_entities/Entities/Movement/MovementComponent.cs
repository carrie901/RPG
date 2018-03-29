using UnityEngine;

namespace Summer
{
    public class MovementComponent : MonoBehaviour
    {
        #region 属性

        public const float NAVMESH_RADIUS = 0.5f;

        public float capsule_collider_radius = 0.51f;                   // Capsule Collider半径

        public bool _reach_target_pos;                                  // 达到目的地

        //public Vector3 move_velocity = Vector3.one;             // 当前速度
        public Vector3 move_direction;                                  // 目标方向
        public float turnspeed = 10;
        public Transform trans;

        public Transform cube_tran;
        public UnityEngine.AI.NavMeshPath nav_path = new UnityEngine.AI.NavMeshPath();
        #endregion

        #region 构造

        /*public MovementComponent(Transform tran)
        {
            trans = tran;
            MovementManager.AddComponent(this);
        }*/

        private void Awake()
        {
            trans = transform;
            Vector3 cur_position = trans.position;
            trans.position = NavMeshHelper.MakeReasonablePos(cur_position);
        }

        #endregion

        public void OnUpdate(float dt)
        {
            // 1.是否到达目的地
            if (!_reach_target_pos) return;
            Vector3 cur_position = trans.position;
            cur_position = NavMeshHelper.MakeReasonablePos(cur_position);

            //float target_height = NavMeshHelper.GetGroundHeightY(cur_position + NAVMESH_RADIUS * move_direction);
            //if (MathHelper.IsEqual(target_height, MovementConst.error_height))
            //    return;

            // 2.移动的距离
            Vector3 distance = move_direction * dt * 5;

            // 3.下一个位置
            Vector3 next_pos = cur_position + distance;

            float height = NavMeshHelper.GetGroundHeightY(next_pos);
            if (!MathHelper.IsEqual(height, MovementConst.error_height))
                next_pos.y = height;

            next_pos = NavMeshHelper.MakeReasonablePos(next_pos);

            trans.position = next_pos;

            if (move_direction != trans.eulerAngles)
            {
                trans.rotation = Quaternion.Lerp(trans.rotation, Quaternion.LookRotation(move_direction), Time.deltaTime * turnspeed);
            }

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
        }

        #region 驱动移动 1.目标方向 2.目标点

        public void AddDirection(Vector3 target_direction)
        {
            _reach_target_pos = true;
            move_direction = target_direction;
        }

        public void RemoveDirection()
        {
            move_direction = Vector3.zero;
            //move_velocity = Vector3.zero;
            _reach_target_pos = false;
        }

        public void AddTargetPosition(Vector3 target_pos)
        {

        }

        public void RemoveTargetPosition()
        {

        }

        #endregion
    }
}

