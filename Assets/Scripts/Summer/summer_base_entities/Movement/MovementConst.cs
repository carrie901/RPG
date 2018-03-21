using UnityEngine;

namespace Summer
{
    public class MovementConst
    {
        public const float MIN_DISTANCE = 0.5f;                         // 最短移动距离
        public const float MAX_DISTANCE = 20f;
        public const float MIN_GROUNDHEIGHT = 1000f;

        public static Vector3 error_position = new Vector3(-1000f, -1000f, -1000f);
    }
}

