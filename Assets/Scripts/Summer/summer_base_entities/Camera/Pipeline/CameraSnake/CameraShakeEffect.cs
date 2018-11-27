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
        public Vector3 _shakeDir = Vector3.one;
        /// <summary>
        /// 相机震动时间
        /// </summary>
        public float _shakeTime = 1.0f;

        private float _currentTime;
        private float _totalTime;

        public void Trigger()
        {
            _totalTime = _shakeTime;
            _currentTime = _shakeTime;
        }

        public void Stop()
        {
            _currentTime = 0.0f;
            _totalTime = 0.0f;
        }

        public void UpdateShake()
        {
            if (_currentTime > 0.0f && _totalTime > 0.0f)
            {
                float percent = _currentTime / _totalTime;

                Vector3 shakePos = Vector3.zero;
                shakePos.x = Random.Range(-Mathf.Abs(_shakeDir.x) * percent, Mathf.Abs(_shakeDir.x) * percent);
                shakePos.y = Random.Range(-Mathf.Abs(_shakeDir.y) * percent, Mathf.Abs(_shakeDir.y) * percent);
                shakePos.z = Random.Range(-Mathf.Abs(_shakeDir.z) * percent, Mathf.Abs(_shakeDir.z) * percent);

                Camera.main.transform.position += shakePos;

                _currentTime -= Time.deltaTime;
            }
            else
            {
                _currentTime = 0.0f;
                _totalTime = 0.0f;
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
