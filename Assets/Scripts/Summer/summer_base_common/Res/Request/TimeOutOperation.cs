
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
    public class TimeOutOperation : LoadOpertion
    {
        public float _timeOut;                     // 超时时间
        public float _loadTime;                    // 已经加载的时间

        public TimeOutOperation(float timeOut)
        {
            _loadTime = 0;
            _timeOut = timeOut;
        }

        public override void OnUpdate()
        {
            _loadTime += Time.timeScale * Time.deltaTime;
        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public override bool IsDone() { return _loadTime > _timeOut; }

        public void OnReset(float timeOut)
        {
            _timeOut = timeOut;
            _loadTime = 0;
        }

        public void OnReset()
        {
            OnReset(_timeOut);
        }
    }
}


