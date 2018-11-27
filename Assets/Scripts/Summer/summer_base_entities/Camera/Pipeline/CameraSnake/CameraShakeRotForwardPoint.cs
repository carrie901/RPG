using UnityEngine;

namespace Summer
{
    public class CameraShakeRotForwardPoint : CameraShake
    {

        public Vector3 _dir;
        public float _radius;
        public ShakeFunc _shakeFunc;

        public CameraShakeRotForwardPoint(Vector3 dir, ShakeFunc shakeFunc, float radius = 10)
        {
            _dir = dir;
            _dir.Normalize();
            _shakeFunc = shakeFunc;
            _radius = radius;
        }

        public override bool IsEnd()
        {
            LogManager.Assert(_shakeFunc != null, "[IsEnd] please set a shake func");
            if (_shakeFunc == null) return true;
            return _shakeFunc.IsEnd();
        }

        public override void Process(CameraPipelineData data, float t)
        {
            float shakeFuncVal = _shakeFunc.Get(t);

            CameraData destDataWithoutShake = data._dest_data_without_shake;
            Vector3 forward = destDataWithoutShake._rot * Vector3.forward;
            Vector3 tarPos = destDataWithoutShake._pos + forward * _radius;

            //forward 
            Vector3 forwardDelta = forward * _dir.z * shakeFuncVal;

            //right 
            float angleX = Mathf.Atan2(_dir.x * shakeFuncVal, _radius) * Mathf.Rad2Deg;
            //up
            float angleY = Mathf.Atan2(_dir.y * shakeFuncVal, _radius) * Mathf.Rad2Deg;
            Quaternion rotDelta = Quaternion.Euler(angleY, angleX, 0);
            Quaternion curDestRot = destDataWithoutShake._rot * rotDelta;
            Vector3 curNowCamPos = tarPos - curDestRot * Vector3.forward * _radius;
            Vector3 deltaPos = curNowCamPos - destDataWithoutShake._pos;
            deltaPos += forwardDelta;

            data._dest_data._pos += deltaPos;
            data._dest_data._rot *= rotDelta;
        }
    }
}

