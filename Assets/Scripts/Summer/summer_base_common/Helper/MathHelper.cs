﻿using UnityEngine;
using System.Collections.Generic;
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
        public static float epsilon = float.Epsilon;

        /// <summary>
        /// float 近似相等
        /// </summary>
        public static bool IsEqual(float a, float b)
        {
            return (Mathf.Abs(a - b) < float.Epsilon);
        }

        /// <summary>
        /// 是否等于0 float 精度问题
        /// </summary>
        public static bool IsZero(float a)
        {
            return (Mathf.Abs(a - 0.0f) < 0.0001f);
        }

        /// <summary>
        /// 大致相等 误差在0.05之间
        /// </summary>
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


        /// <summary>
        /// Lerp function that doesn't clamp the 'factor' in 0-1 range.
        /// </summary>
        public static float Lerp(float from, float to, float factor) { return from * (1f - factor) + to * factor; }

        /// <summary>
        /// value在0-max之间 最小=0 最大=max-1
        /// </summary>
        public static int ClampIndex(int val, int max) { return (val < 0) ? 0 : (val < max ? val : max - 1); }

        /// <summary>
        /// val的值在0-max-1之间 返回val 如果小于0 返回max-1，如果大于等级max-1 返回0
        /// </summary>
        public static int ClampIndex02(int val, int max)
        {
            if (val < 0)
                return max - 1;
            else if (val >= max)
                return 0;
            return val;
        }

        /// <summary>
        /// 在0和1之间
        /// </summary>
        public static float InZeorOne(float value)
        {
            if (value >= 1f)
                return 1;
            else if (value <= 0f)
                return 0;
            return value;
        }

        /// <summary>
        /// 取float的小数部分
        /// </summary>
        public static float WrapFloat(float val) { return val - Mathf.FloorToInt(val); }

        #region 得到两个向量之间的夹角

        /// <summary>
        /// 获取两个点间的夹角
        /// </summary>
        public static float GetAngle01(Vector3 form, Vector3 to)
        {
            Vector3 n_vector = Vector3.zero;
            n_vector.x = to.x;
            n_vector.y = form.y;
            float a = to.y - n_vector.y;
            float b = n_vector.x - form.x;
            float tan = a / b;
            return Mathf.Atan(tan) * 180.0f * ONE_DIV_PI;
        }

        public static float GetAngle02(Vector3 form, Vector3 to)
        {
            return Vector3.Angle(form, to);
        }

        public static float GetAngle03(Vector3 form, Vector3 to)
        {
            // 计算 a、b 单位向量的点积  
            float result = Vector3.Dot(form.normalized, to.normalized);
            // 通过反余弦函数获取 向量 a、b 夹角（默认为 弧度）  
            float radians = Mathf.Acos(result);
            // 将弧度转换为 角度  
            float angle = radians * Mathf.Rad2Deg;
            return angle;
        }

        public static float WrapAngle(float angle)
        {
            while (angle > 180f) angle -= 360f;
            while (angle < -180f) angle += 360f;
            return angle;
        }

        #endregion

        #region 文本转颜色

        /// <summary>
        /// #A0ff00FF 颜色  待验证
        /// </summary>
        public static Color ParseColor32(string text, int offset = 0)
        {
            int r = (HexToDecimal(text[offset]) << 4) | HexToDecimal(text[offset + 1]);
            int g = (HexToDecimal(text[offset + 2]) << 4) | HexToDecimal(text[offset + 3]);
            int b = (HexToDecimal(text[offset + 4]) << 4) | HexToDecimal(text[offset + 5]);
            //int a = (HexToDecimal(text[offset + 6]) << 4) | HexToDecimal(text[offset + 7]);
            float f = 1f / 255f;
            return new Color(f * r, f * g, f * b);
        }

        /// <summary>
        /// 二级制转换成16进制
        /// </summary>
        private static int HexToDecimal(char ch)
        {
            switch (ch)
            {
                case '0': return 0x0;
                case '1': return 0x1;
                case '2': return 0x2;
                case '3': return 0x3;
                case '4': return 0x4;
                case '5': return 0x5;
                case '6': return 0x6;
                case '7': return 0x7;
                case '8': return 0x8;
                case '9': return 0x9;
                case 'a':
                case 'A': return 0xA;
                case 'b':
                case 'B': return 0xB;
                case 'c':
                case 'C': return 0xC;
                case 'd':
                case 'D': return 0xD;
                case 'e':
                case 'E': return 0xE;
                case 'f':
                case 'F': return 0xF;
            }
            return 0xF;
        }

        #endregion

        public static float RotateTowards(float from, float to, float max_angle)
        {
            float diff = WrapAngle(to - from);
            if (Mathf.Abs(diff) > max_angle) diff = max_angle * Mathf.Sign(diff);
            return from + diff;
        }

        /// <summary>
        /// 指定点到线段的距离
        /// </summary>
        public static float DistancePointToLineSegment(Vector2 point, Vector2 a, Vector2 b)
        {
            float l2 = (b - a).sqrMagnitude;
            if (IsEqual(l2, 0f)) return (point - a).magnitude;
            float t = Vector2.Dot(point - a, b - a) / l2;
            if (t < 0f)
                return (point - a).magnitude;
            else if (t > 1f)
                return (point - b).magnitude;
            Vector2 projection = a + t * (b - a);
            return (point - projection).magnitude;
        }

        /// <summary>
        /// int[] 转到 （key,value）
        /// </summary>
        public static Dictionary<int, int> IntToDic(int[] value)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int count = value.Length;
            if (count > 0 && count % 2 == 0)
                return dic;
            int length = count % 2;
            for (int i = 0; i < length; i++)
            {
                if (!dic.ContainsKey(value[i * 2]))
                {
                    dic.Add(value[i * 2], value[i * 2 + 1]);
                }
                else
                {
                    LogManager.Log("解析错误IntToDic");
                }
            }
            return dic;
        }
    }
}

