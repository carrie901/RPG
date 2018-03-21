using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-15 20:39:18
// FileName : RectTransformHelper.cs
//=============================================================================

namespace Summer
{
    public class RectTransformHelper
    {
        // 需要归一化
        public static void SetParent(RectTransform rect, Transform parent)
        {
            rect.SetParent(parent);
            rect.transform.localPosition = Vector3.zero;
            rect.transform.localScale = Vector3.one;
            rect.transform.localEulerAngles = Vector3.zero;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect">RectTransform 组件</param>
        /// <param name="screen_point">目标坐标转换的屏幕坐标</param>
        /// <param name="cam">目标摄像机,如果Canvas的 Render Mode 参数类型设置为 Screen Space - Camera时需要写摄像机参数</param>
        public static void ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screen_point, Camera cam)
        {
            //接收转换后的坐标，需要提前声明一个 Vector3 参数
            Vector3 world_point;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screen_point, cam, out world_point))
            {
                rect.transform.position = world_point;
            }
        }

        /// <summary>
        /// 3D目标转2D UGUI
        /// </summary>
        public static void WorldToUgui(Transform self_tran, RectTransform self_rect, Transform target, Camera main_camera, float height)
        {
            //
            //将目标的3D世界坐标转换为 屏幕坐标
            Vector3 target_screen_position = main_camera.WorldToScreenPoint(target.position);
            target_screen_position.y += height;
            Vector3 world_point;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(self_rect, target_screen_position, null, out world_point))
            {
                self_tran.position = world_point;
            }
        }

        /// <summary>
        /// 3D世界坐标转成屏幕坐标
        /// </summary>
        public static Vector3 WorldToScreenPoint(Camera main_camera, Vector3 world_pos)
        {
            Vector3 target_screen_position = main_camera.WorldToScreenPoint(world_pos);
            return target_screen_position;
        }

        public static void WorldToUgui(Transform target, RectTransform self_rect, Camera main_camera)
        {
           /* Vector3 target_screen_position = cMainCameraManager.mMainCamera.WorldToScreenPoint(target.position);

            float half_width = (float)Screen.width / 2;
            float half_height = (float)Screen.height / 2;
            Vector2 point = new Vector2(target_screen_position.x - half_width, target_screen_position.y - half_height);

            self_rect.localPosition = new Vector3(point.x, point.y, 0);*/
        }

        #region 设置RectTransform 的宽高

        public static void SetRectWidth(RectTransform rt, float width)
        {
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }

        public static void SetRectHeight(RectTransform rt, float height)
        {
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        }

        public static void SetRectWidthHeight(RectTransform rt, RectTransform.Edge edge, float width, float height)
        {
            rt.SetInsetAndSizeFromParentEdge(edge, width, height);
        }

        #endregion

        #region

        public static void SetSiblingIndex(RectTransform current, RectTransform begin, bool after)
        {
            int index = begin.GetSiblingIndex();
            if (after)
                index++;
            current.SetSiblingIndex(index);
        }

        #endregion
    }
}
