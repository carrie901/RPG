
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
    [System.Serializable]
    public class CameraSourceSpeed
    {
        [Header("rotaion移动的速度")]
        public float _rot_speed = 2;

        [Header("rotaion快速移动的速度")]
        public float _fast_rot_speed = 1;

        [Header("rotaion慢速移动的速度")]
        public float _slow_rot_speed = 0.4f;

        [Header("offset的移动速度")]
        public float _offset_speed = 1;
    }
}

