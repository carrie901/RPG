using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Summer
{
    public class NavMeshHelper
    {
        protected static NavMeshPath navmesh_path = new NavMeshPath();

        #region public

        /// <summary>
        /// 计算路径
        /// </summary>
        public static void CalcPath(Vector3 source_pos, Vector3 target_pos, ref List<Vector3> path_nodes)
        {
            // 初始化数据
            if (path_nodes == null) return;
            path_nodes.Clear();

            //LogManager.Error("目标点为非法点:{0}", target_pos);
            // 非法目标
            NavMeshHit nav_hit;
            if (!NavMesh.SamplePosition(target_pos, out nav_hit, MovementConst.MIN_DISTANCE, -1)) return;

            // 最短距离
            float distance = Vector2.Distance(source_pos, target_pos);
            if (distance < MovementConst.MIN_DISTANCE) return;

            // 清除老数据，重新开始寻路
            navmesh_path.ClearCorners();
            if (!NavMesh.CalculatePath(source_pos, target_pos, -1, navmesh_path)) return;
            if (navmesh_path.status != NavMeshPathStatus.PathComplete) return;

            // 添加路径节点
            path_nodes.AddRange(navmesh_path.corners);
            navmesh_path.ClearCorners();
        }

        // 采样目标点是否有效
        public static bool IsVaildPosition(Vector3 next_post)
        {
            NavMeshHit nav_hit;
            if (!NavMesh.SamplePosition(next_post, out nav_hit, MovementConst.MIN_DISTANCE, -1)) return false;
            return true;
        }

        public static Vector3 FindClosestEdge(Vector3 source_pos)
        {
            NavMeshHit nav_hit;
            bool result = NavMesh.FindClosestEdge(source_pos, out nav_hit, -1);
            if (!result) return MovementConst.error_position;
            return nav_hit.position;
        }

        #endregion

        #region 得到当前NavMesh点的高度

        public static float GetGroundHeight(Vector3 target_pos)
        {
            RaycastHit hit;
            Vector3 origin = new Vector3(target_pos.x, MovementConst.MIN_GROUNDHEIGHT, target_pos.z);

            if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, -1))
                return hit.point.y;
            else
                return MovementConst.MIN_GROUNDHEIGHT;
        }

        #endregion

        #region 网格最近的点

        public static Vector3 GetNearestValidNavGroundPosition(Vector3 source_pos)
        {
            NavMeshHit nav_hit;

            if (NavMesh.SamplePosition(source_pos, out nav_hit, MovementConst.MIN_DISTANCE, -1))
                return nav_hit.position;
            else
            {
                return FindClosestEdge(source_pos);
            }
        }

        #endregion
    }
}

