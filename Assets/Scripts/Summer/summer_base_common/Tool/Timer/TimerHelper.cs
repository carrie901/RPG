using UnityEngine;

namespace Summer
{
    public class TimerHelper
    {

        public static float RealtimeSinceStartup()
        {
            return Time.realtimeSinceStartup;
        }
    }
}

