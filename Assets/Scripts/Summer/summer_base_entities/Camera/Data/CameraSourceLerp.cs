using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 镜头过度的方式，默认直线过度
    /// </summary>
    public class CameraSourceLerp
    {
        public virtual CameraSourceData CameraLerp(CameraSourceData from, CameraSourceData to, float dt)
        {
            return Lerp(from, to, dt);
        }

        public static CameraSourceData Lerp(CameraSourceData from, CameraSourceData to, float dt)
        {
            CameraSourceData d;

            d._rotaion.x = Mathf.LerpAngle(from._rotaion.x, to._rotaion.x, dt);
            d._rotaion.y = Mathf.LerpAngle(from._rotaion.y, to._rotaion.y, dt);
            d._rotaion.z = Mathf.LerpAngle(from._rotaion.z, to._rotaion.z, dt);

            d._offset = Vector3.Lerp(from._offset, to._offset, dt);
            return d;
        }
    }
}