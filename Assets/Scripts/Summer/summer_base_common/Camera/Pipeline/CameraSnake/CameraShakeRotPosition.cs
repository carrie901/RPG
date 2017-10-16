using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Summer
{
    public class CameraShakeRotPosition : MonoBehaviour
    {
        public const float MIN = 0.1f;
        public bool shaking;                        // 摄像头抖动开始
        public Action shake_started;                        // 开始抖动
        public Action shake_completed;                      // 结束抖动


        public CameraShakeVbo vbo;

        private Vector3 start_pos;                          // 起始的位置
        private Quaternion start_rot;                       // 起始的角度
        private Transform cache_trans;                      // trans的缓存
        private Vector3 _seed;
        public bool test_bool;
        public bool test_start;

        void Awake()
        {
            cache_trans = transform;
            vbo = new CameraShakeVbo();
            vbo.Init();
        }

        // Update is called once per frame
        void Update()
        {
            if (test_bool)
            {
                _reset();
            }

            if (!test_start)
                return;

            if (!shaking)
            {
                shaking = true;
                if (shake_started != null)
                    shake_started();
            }

            float timer = (Time.time - vbo.start_time) * vbo.speed;

            Vector3 new_shake_position = start_pos + new Vector3(
                    _seed.x * Mathf.Sin(timer) * (vbo.shake_amount.x * vbo.shake_distance * vbo.scale),
                     _seed.y * Mathf.Cos(timer) * (vbo.shake_amount.y * vbo.shake_distance * vbo.scale),
                    _seed.z * Mathf.Sin(timer) * (vbo.shake_amount.z * vbo.shake_distance * vbo.scale));

            Quaternion new_shake_rotation = start_rot * Quaternion.Euler(
                   _seed.x * Mathf.Cos(timer) * (vbo.rotation_amount.x * vbo.rotation_strength * vbo.scale),
                   _seed.y * Mathf.Sin(timer) * (vbo.rotation_amount.y * vbo.rotation_strength * vbo.scale),
                   _seed.z * Mathf.Cos(timer) * (vbo.rotation_amount.z * vbo.rotation_strength * vbo.scale));

            NormalizeQuaternion(ref new_shake_rotation);

            cache_trans.localPosition = new_shake_position;
            cache_trans.localRotation = new_shake_rotation;

            if (timer > Mathf.PI * 2)
            {
                vbo.start_time = Time.time;
                vbo.shake_distance *= (1 - Mathf.Clamp01(vbo.decay));
                vbo.rotation_strength *= (1 - Mathf.Clamp01(vbo.decay));

                if (vbo.shake_distance <= MIN)
                {
                    _reset_original();
                    if (shake_completed != null)
                        shake_completed();
                }
            }

        }


        public void _reset()
        {
            _seed = Random.insideUnitSphere;
            test_bool = false;
            test_start = true;

            shaking = false;
            start_pos = cache_trans.localPosition;
            start_rot = cache_trans.localRotation;
        }

        // 回复到原始位置
        public void _reset_original()
        {
            cache_trans.localPosition = start_pos;
            cache_trans.localRotation = start_rot;

            test_start = false;
        }

        private static void NormalizeQuaternion(ref Quaternion q)
        {
            float sum = 0;

            for (int i = 0; i < 4; ++i)
                sum += q[i] * q[i];

            float magnitude_inverse = 1 / Mathf.Sqrt(sum);

            for (int i = 0; i < 4; ++i)
                q[i] *= magnitude_inverse;
        }

        internal class ShakeState
        {
            internal readonly Vector3 start_position;
            internal readonly Quaternion start_rotation;

            internal Vector3 shake_position;
            internal Quaternion shake_rotation;


            internal ShakeState(Vector3 position, Quaternion rotation)
            {
                start_position = position;
                start_rotation = rotation;
                shake_position = position;
                shake_rotation = rotation;
            }
        }
    }

    [System.Serializable]
    public class CameraShakeVbo
    {
        public float start_time = 0;
        public bool multiply_by_time_scale = false;     // 是否收到时间缩放的影响
        public Vector3 shake_amount = Vector3.one;      // 震动的方向 是否需要震动到Z轴
        public Vector3 rotation_amount = Vector3.one;   // 旋转角度
        public float shake_distance = 00.10f;
        public float rotation_strength = 1;             // 旋转的强度
        public float speed = 50.00f;                    // 速度
        public float decay = 00.20f;                    //
        public float scale;
        public void Init()
        {
            start_time = Time.time;
            scale = multiply_by_time_scale ? Time.timeScale : 1;
        }
    }
}

