﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Test
{
    public class TMathUtils : MonoBehaviour
    {

        public const float EPSILON = 0.00001f;

        public static bool IsZero(float v, float e = EPSILON)
        {
            return Mathf.Abs(v) < EPSILON;
        }

        /// <summary>
        /// 扁平化，y轴值强制为0
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Vector3ZeroY(Vector3 v)
        {
            return new Vector3(v.x, 0, v.z);
        }
        public static Vector3 GetDirection2D(Vector3 to, Vector3 from)
        {
            Vector3 dir = to - from;
            dir.y = 0;
            return dir.normalized;
        }
        public static float GetDistance2D(Vector3 to, Vector3 from)
        {
            Vector3 dir = to - from;
            dir.y = 0;
            return dir.magnitude;
        }
    }
}
