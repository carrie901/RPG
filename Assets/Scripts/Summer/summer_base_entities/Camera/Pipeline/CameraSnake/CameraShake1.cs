
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using UnityEngine;

namespace Summer
{
    public class CameraShake1 : MonoBehaviour
    {
        private Transform _camTransform;

        public float _shake;                                            // 持续抖动的时长
        public float _shakeAmount = 0.7f;                               // 抖动幅度（振幅）
        public float _decreaseFactor = 1.0f;                            // 振幅越大抖动越厉害

        public bool _flag = true;
        Vector3 _originalPos;

        void Awake()
        {
            if (_camTransform == null)
            {
                _camTransform = GetComponent(typeof(Transform)) as Transform;
            }
        }

        void OnEnable()
        {
            //_originalPos = _camTransform.localPosition;
            GameEventSystem.Instance.RegisterHandler(E_GLOBAL_EVT.camera_shake, OnShake);
        }

        void OnDisable()
        {
            GameEventSystem.Instance.UnRegisterHandler(E_GLOBAL_EVT.camera_shake, OnShake);
        }

        void Update()
        {
            if (_flag) return;
            if (_shake > 0)
            {
                _camTransform.localPosition = _originalPos + Random.insideUnitSphere * _shakeAmount;

                _shake -= Time.deltaTime * _decreaseFactor;
            }
            else
            {
                _shake = 0f;
                _camTransform.localPosition = _originalPos;
                _flag = true;
            }
        }

        void OnShake(System.Object param)
        {
            _flag = false;
            _shake = 0.5f;
            _originalPos = _camTransform.localPosition;
        }
    }
}