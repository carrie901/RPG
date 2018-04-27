// 相机震动效果
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 一个简单的震屏效果
    /// 需要注意的事这个脚本的执行顺序问题，如果他在跟随之前执行了，可能就没有效果了
    /// </summary>
    public class CameraShakeEffect : MonoBehaviour
    {
        /// <summary>
        /// 相机震动方向
        /// </summary>
        public Vector3 shake_dir = Vector3.one;
        /// <summary>
        /// 相机震动时间
        /// </summary>
        public float shake_time = 1.0f;

        private float current_time = 0.0f;
        private float total_time = 0.0f;

        public void Trigger()
        {
            total_time = shake_time;
            current_time = shake_time;
        }

        public void Stop()
        {
            current_time = 0.0f;
            total_time = 0.0f;
        }

        public void UpdateShake()
        {
            if (current_time > 0.0f && total_time > 0.0f)
            {
                float percent = current_time / total_time;

                Vector3 shakePos = Vector3.zero;
                shakePos.x = Random.Range(-Mathf.Abs(shake_dir.x) * percent, Mathf.Abs(shake_dir.x) * percent);
                shakePos.y = Random.Range(-Mathf.Abs(shake_dir.y) * percent, Mathf.Abs(shake_dir.y) * percent);
                shakePos.z = Random.Range(-Mathf.Abs(shake_dir.z) * percent, Mathf.Abs(shake_dir.z) * percent);

                Camera.main.transform.position += shakePos;

                current_time -= Time.deltaTime;
            }
            else
            {
                current_time = 0.0f;
                total_time = 0.0f;
            }
        }

        void LateUpdate()
        {
            UpdateShake();
        }

        void OnEnable()
        {
            Trigger();
        }

    }
}
