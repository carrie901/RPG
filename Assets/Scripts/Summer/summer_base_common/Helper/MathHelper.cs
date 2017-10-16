using UnityEngine;
using System.Collections;

namespace Summer
{
    public class MathHelper
    {
        public const float ONE_DIV_PI = 1.0f / Mathf.PI;
        public static float cos_15 = Mathf.Cos(Mathf.Deg2Rad * 15.0f);
        public static float cos_35 = Mathf.Cos(Mathf.Deg2Rad * 35.0f);
        public static float cos_45 = Mathf.Cos(Mathf.Deg2Rad * 45.0f);
        public static float cos_75 = Mathf.Cos(Mathf.Deg2Rad * 75.0f);
        public static float cos_60 = Mathf.Cos(Mathf.Deg2Rad * 60.0f);
        public static float cos_30 = Mathf.Cos(Mathf.Deg2Rad * 30.0f);
        public static float cos_20 = Mathf.Cos(Mathf.Deg2Rad * 20.0f);
        public static float epsilon = 0.001f;
        /// <summary>
        /// 获取两个点间的夹角
        /// </summary>
        /// <param name="form"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float GetAngle(Vector3 form, Vector3 to)
        {
            Vector3 nVector = Vector3.zero;
            nVector.x = to.x;
            nVector.y = form.y;
            float a = to.y - nVector.y;
            float b = nVector.x - form.x;
            float tan = a / b;
            return Mathf.Atan(tan) * 180.0f * ONE_DIV_PI;
        }

        /// <summary>
        /// float 近似相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool IsEqualFloat(float a, float b)
        {
            return (Mathf.Abs(a - b) < 0.001f);
        }

        public static bool IsZero(float a)
        {
            return (Mathf.Abs(a - 0.0f) < 0.0001f);
        }


        public static bool IsEqualFloatRaw(float a, float b)
        {
            return (Mathf.Abs(a - b) < 0.05f);
        }

        ///3D空间投影到屏幕坐标
        public static Vector2 ProjectToScreen(Camera cam, Vector3 point)
        {
            Vector3 screen_point = cam.WorldToScreenPoint(point);
            return new Vector2(screen_point.x, screen_point.y);
        }

    }
}

