using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    public class RayHelper
    {
        public static RaycastHit RayCastCollider(Vector2 pos, float distance)
        {
            Vector3 origin = Vector3.one;
            RaycastHit hit;
            if (Physics.Raycast(origin, Vector3.up, out hit, distance, -1))
            {
                if (hit.collider != null)
                    return hit;
            }
            return default(RaycastHit);
        }

    }

}

