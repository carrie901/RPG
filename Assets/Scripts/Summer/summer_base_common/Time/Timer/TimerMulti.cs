
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
                                             
namespace Summer
{
    public class TimerMulti : Timer
    {
        private int _count;
        private int _repeatCount;
        //count 如果小于等于0，相当于无限次
        public TimerMulti(float interval, int repeatCount, OnTimerHandler handler)
            : base(interval, handler)
        {
            _repeatCount = repeatCount;
            _count = 0;
        }

        public override void OnTimeout()
        {
            base.OnTimeout();
            _count++;
            //TODO 会有偶一定的问题 关于超时时间
            if (_repeatCount <= 0 || _count < _repeatCount)
            {
                ElapsedTime = 0;
                TimerManager.Instance.AddTimer(this);
            }
        }
    }
}
