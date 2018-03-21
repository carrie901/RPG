using UnityEngine;

namespace Summer
{
    public class CameraShakeRotForwardPoint : CameraShake
    {

        public Vector3 _dir;
        public float _radius;
        public ShakeFunc _shake_func;

        public CameraShakeRotForwardPoint(Vector3 dir, ShakeFunc shake_func, float radius = 10)
        {
            _dir = dir;
            _dir.Normalize();
            _shake_func = shake_func;
            _radius = radius;
        }

        public override bool IsEnd()
        {
            LogManager.Assert(_shake_func != null, "[IsEnd] please set a shake func");
            if (_shake_func == null) return true;
            return _shake_func.IsEnd();
        }

        public override void Process(CameraPipelineData data, float t)
        {
            float shake_func_val = _shake_func.Get(t);

            CameraData dest_data_without_shake = data._dest_data_without_shake;
            Vector3 forward = dest_data_without_shake._rot * Vector3.forward;
            Vector3 tar_pos = dest_data_without_shake._pos + forward * _radius;

            //forward 
            Vector3 forward_delta = forward * _dir.z * shake_func_val;

            //right 
            float angle_x = Mathf.Atan2(_dir.x * shake_func_val, _radius) * Mathf.Rad2Deg;
            //up
            float angle_y = Mathf.Atan2(_dir.y * shake_func_val, _radius) * Mathf.Rad2Deg;
            Quaternion rot_delta = Quaternion.Euler(angle_y, angle_x, 0);
            Quaternion cur_dest_rot = dest_data_without_shake._rot * rot_delta;
            Vector3 cur_now_cam_pos = tar_pos - cur_dest_rot * Vector3.forward * _radius;
            Vector3 delta_pos = cur_now_cam_pos - dest_data_without_shake._pos;
            delta_pos += forward_delta;

            data._dest_data._pos += delta_pos;
            data._dest_data._rot *= rot_delta;
        }
    }
}

