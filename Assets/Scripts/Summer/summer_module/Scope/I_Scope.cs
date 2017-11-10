using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Summer
{
    public interface I_Scope 
    {

       
    }

    public struct AAB2
    {
        public Vector2 Min;
        public Vector2 Max;


        public static AAB2 CreateAAB2(Transform point0, Transform point1)
        {
            // Creates aab from two unsorted points, if you know min and max use constructor  
            return CreateFromTwoPoints(point0.position, point1.position);
        }

        public static AAB2 CreateFromTwoPoints(Vector2 point0, Vector2 point1)
        {
            AAB2 aab;
            if (point0.x < point1.x)
            {
                aab.Min.x = point0.x;
                aab.Max.x = point1.x;
            }
            else
            {
                aab.Min.x = point1.x;
                aab.Max.x = point0.x;
            }
            if (point0.y < point1.y)
            {
                aab.Min.y = point0.y;
                aab.Max.y = point1.y;
                return aab;
            }
            aab.Min.y = point1.y;
            aab.Max.y = point0.y;
            return aab;
        }

        public bool Contains(ref Vector2 point)
        {
            if (point.x < this.Min.x)
            {
                return false;
            }
            if (point.x > this.Max.x)
            {
                return false;
            }
            if (point.y < this.Min.y)
            {
                return false;
            }
            if (point.y > this.Max.y)
            {
                return false;
            }
            return true;
        }

        public void CalcVertices(out Vector2 vertex0, out Vector2 vertex1, out Vector2 vertex2, out Vector2 vertex3)
        {
            vertex0 = this.Min;
            vertex1 = new Vector2(this.Max.x, this.Min.y);
            vertex2 = this.Max;
            vertex3 = new Vector2(this.Min.x, this.Max.y);
        }
    }
}

