
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 相机震动效果
    /// TODO 我们的震动脚本在真正的相机控制脚本执行之前执行了，是不会产生效果的,需要脚本执行顺序权值大一些Projcet Setting->Script Execution Order
    /// </summary>
    public class CameraShakeEffect : MonoBehaviour
    {
        public Vector3 shake_dir = Vector3.one;              //相机震动方向
        public float shake_time = 1.0f;                      //相机震动时间

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

                Vector3 shake_pos = Vector3.zero;
                shake_pos.x = UnityEngine.Random.Range(-Mathf.Abs(shake_dir.x) * percent, Mathf.Abs(shake_dir.x) * percent);
                shake_pos.y = UnityEngine.Random.Range(-Mathf.Abs(shake_dir.y) * percent, Mathf.Abs(shake_dir.y) * percent);
                shake_pos.z = UnityEngine.Random.Range(-Mathf.Abs(shake_dir.z) * percent, Mathf.Abs(shake_dir.z) * percent);

                Camera.main.transform.position += shake_pos;

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

