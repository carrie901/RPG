using UnityEngine;

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
        /// <param name="screenPoint">目标坐标转换的屏幕坐标</param>
        /// <param name="cam">目标摄像机,如果Canvas的 Render Mode 参数类型设置为 Screen Space - Camera时需要写摄像机参数</param>
        public static void ScreenPointToWorldPointInRectangle(RectTransform rect, Vector2 screenPoint, Camera cam)
        {
            //接收转换后的坐标，需要提前声明一个 Vector3 参数
            Vector3 worldPoint;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPoint, cam, out worldPoint))
            {
                rect.transform.position = worldPoint;
            }
        }

        /// <summary>
        /// 3D目标转2D UGUI
        /// </summary>
        public static void WorldToUgui(Transform selfTran, RectTransform selfRect, Transform target, Camera mainCamera, float height)
        {
            //
            //将目标的3D世界坐标转换为 屏幕坐标
            Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(target.position);
            targetScreenPosition.y += height;
            Vector3 worldPoint;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(selfRect, targetScreenPosition, null, out worldPoint))
            {
                selfTran.position = worldPoint;
            }
        }

        /// <summary>
        /// 3D世界坐标转成屏幕坐标
        /// </summary>
        public static Vector3 WorldToScreenPoint(Camera mainCamera, Vector3 worldPos)
        {
            Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(worldPos);
            return targetScreenPosition;
        }

        // 检测摄像机是不是物体的前面
        public static bool IsInView(Vector3 worldPos)
        {
            Transform cameraTran = Camera.main.transform;
            Vector2 viewPos = Camera.main.WorldToViewportPoint(worldPos);
            Vector3 dir = (worldPos - cameraTran.position).normalized;

            float dot = Vector3.Dot(cameraTran.forward, dir);
            return dot > 0 && viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1;
        }

        public static Vector2 WorldPositionToCanvasPosition(Camera worldCamera, RectTransform canvasRect, Vector3 worldPosition)
        {

            Vector3 screenPos = worldCamera.WorldToViewportPoint(worldPosition);
            float x = ((screenPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f));
            float y = ((screenPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
            return new Vector2(x, y);
        }

        public static Vector2 MousePositionToCanvasPosition(Camera worldCamera, RectTransform canvasRect, Vector3 mousePosition)
        {
            Vector3 screenPos = worldCamera.ScreenToViewportPoint(mousePosition);
            float x = ((screenPos.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f));
            float y = ((screenPos.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));
            return new Vector2(x, y);
        }

        public static void WorldToUgui(Transform target, RectTransform selfRect, Camera mainCamera)
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
