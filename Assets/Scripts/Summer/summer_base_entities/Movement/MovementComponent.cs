using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Summer
{
    public class MovementComponent
    {
        public Vector3 move_velocity = Vector3.one;             // 当前速度
        public Vector3 move_direction;                        // 目标方向

        public Transform tran;

        public void OnUpdate(float dt)
        {
            Vector3 distance = move_velocity * dt;

            Vector3 cur_position = tran.position;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(cur_position, out hit, 0.5f, -1))
                cur_position = hit.position;    //校准起始点  

            Vector3 tmp = cur_position + distance;
            if (NavMesh.SamplePosition(tmp, out hit, 0.5f, -1))
                tmp = hit.position;    //校准目标点  

            move_direction = move_velocity.normalized;
        }


        #region 驱动移动 1.目标方向 2.目标点

        public void AddDirection(Vector3 target_direction)
        {

        }

        public void AddTargetPosition(Vector3 target_pos)
        {

        }

        #endregion
    }
}

