using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class NavMeshHelper
    {
        protected static UnityEngine.AI.NavMeshPath navmesh_path = new UnityEngine.AI.NavMeshPath();

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
            UnityEngine.AI.NavMeshHit nav_hit;
            if (!UnityEngine.AI.NavMesh.SamplePosition(target_pos, out nav_hit, MovementConst.MIN_DISTANCE, UnityLayerConst.Instance.layer_nav_mask)) return;

            // 最短距离
            float distance = Vector2.Distance(source_pos, target_pos);
            if (distance < MovementConst.MIN_DISTANCE) return;

            // 清除老数据，重新开始寻路
            navmesh_path.ClearCorners();
            if (!UnityEngine.AI.NavMesh.CalculatePath(source_pos, target_pos, -1, navmesh_path)) return;
            if (navmesh_path.status != UnityEngine.AI.NavMeshPathStatus.PathComplete) return;

            // 添加路径节点
            path_nodes.AddRange(navmesh_path.corners);
            navmesh_path.ClearCorners();
        }


        /// <summary>
        /// 使目标点合理化
        /// </summary>
        public static Vector3 MakeReasonablePos(Vector3 source_pos)
        {
            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(source_pos, out hit, 0.5f, -1))
                source_pos = hit.position;    //校准起始点  
            return source_pos;
        }

        public static Vector3 ReasonablePos(Vector3 source_pos)
        {
            float height = GetGroundHeightY(source_pos);
            if (!MathHelper.IsEqual(height, MovementConst.error_height))
            {
                return new Vector3(source_pos.x, height, source_pos.z);
            }
            return source_pos;
        }

        public static bool IsCloseEdge(Vector3 source_pos)
        {
            Vector3 close_pos = FindClosestEdge(source_pos);
            Vector3 distance = close_pos - source_pos;

            if (distance.sqrMagnitude <= 0.02 * 0.02) return true;
            return false;
        }

        public static Vector3 FindClosestEdge(Vector3 source_pos)
        {
            UnityEngine.AI.NavMeshHit nav_hit;
            bool result = UnityEngine.AI.NavMesh.FindClosestEdge(source_pos, out nav_hit, UnityEngine.AI.NavMesh.AllAreas);
            if (!result) return MovementConst.error_position;
            return nav_hit.position;
        }

        public static Vector3 FindClosestEdgePos(Transform tran, float distance)
        {
            // tran朝向
            Vector3 forward = tran.forward.normalized;
            Vector3 left = tran.up.normalized;
            Vector3 right = -left;
            for (float f = 0; f <= 1.0f; f += 0.1f)
            {
                Vector3 dir = Vector3.Slerp(forward, left, f).normalized;
                Vector3 tempos = tran.position + dir * distance;

                Vector3 pos_height = NavMeshHelper.GetGroundHeight(tempos);
                if (pos_height != MovementConst.error_position)
                    return dir;

                dir = Vector3.Slerp(forward, right, f).normalized;
                tempos = tran.position + dir * distance;

                pos_height = NavMeshHelper.GetGroundHeight(tempos);
                if (pos_height != MovementConst.error_position)
                    return dir;
            }
            return MovementConst.error_position;
        }

        public static Vector3 FindCloseLinePos(Vector3 pos, Vector3 distance)
        {
            Vector3 next_pos = pos;
            for (float f = 0; f <= 1.0f; f += 0.1f)
            {
                Vector3 tmp_pos = pos + distance * f;
                Vector3 new_pos = GetGroundHeight(tmp_pos);
                if (new_pos == MovementConst.error_position)
                    break;
                else
                    next_pos = tmp_pos;
            }
            return next_pos;
        }

        #endregion

        #region

        public static bool IsColliderDirection(Vector3 target_pos, Vector3 direction, float distance, float radius = 0.0f)
        {
            RaycastHit hit;
            Vector3 origin = new Vector3(target_pos.x, target_pos.y + 1f, target_pos.z);

            if (Physics.Raycast(origin, direction, out hit, distance + radius,
                UnityLayerConst.Instance.layer_sceneobject_mask /*| UnityLayerConst.Instance.layer_defalut_mask*/))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector3 FindCloseCollider(Vector3 target_pos, Vector3 direction, float distance, float radius = 0.0f)
        {
            RaycastHit hit;
            Vector3 next_pos = target_pos;

            float average = 0.02f;
            if (distance > 0.2)
                average = distance / 10f;

            for (int f = 0; f <= 1.0f; f++)
            {
                float add_distance = average * f;
                if (add_distance > distance)
                    break;

                Vector3 tmp_pos = target_pos + add_distance * direction;


                if (Physics.Raycast(tmp_pos, direction, out hit, distance + radius,
                    UnityLayerConst.Instance.layer_sceneobject_mask))
                {
                    break;
                }
                next_pos = tmp_pos;
            }
            return next_pos;
        }


        #endregion

        #region 得到当前NavMesh点的高度

        public static Vector3 GetGroundHeight(Vector3 target_pos)
        {
            RaycastHit hit;
            Vector3 origin = new Vector3(target_pos.x, MovementConst.MIN_GROUNDHEIGHT, target_pos.z);

            if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, UnityLayerConst.Instance.layer_nav_mask))
                return hit.point;
            else
                return MovementConst.error_position;
        }

        public static float GetGroundHeightY(Vector3 target_pos)
        {
            RaycastHit hit;
            Vector3 origin = new Vector3(target_pos.x, MovementConst.MIN_GROUNDHEIGHT, target_pos.z);

            if (Physics.Raycast(origin, Vector3.down, out hit, Mathf.Infinity, UnityLayerConst.Instance.layer_nav_mask))
                return hit.point.y;
            else
                return MovementConst.error_height;
        }

        #endregion

        #region 网格最近的点

        public static Vector3 GetNearestValidNavGroundPosition(Vector3 source_pos)
        {
            UnityEngine.AI.NavMeshHit nav_hit;

            if (UnityEngine.AI.NavMesh.SamplePosition(source_pos, out nav_hit, MovementConst.MIN_DISTANCE, UnityLayerConst.Instance.layer_nav_mask))
                return nav_hit.position;
            else
            {
                return FindClosestEdge(source_pos);
            }
        }

        #endregion
    }
}

