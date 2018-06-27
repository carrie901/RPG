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
    public class RemoteLog : I_Log
    {
        #region Override Log

        public void Log(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Log(string message, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public void Waring(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Warning(string message, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public void Error(string message)
        {
            throw new System.NotImplementedException();
        }

        public void Error(string message, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public void Assert(bool condition, string message)
        {
            throw new System.NotImplementedException();
        }

        public void Assert(bool condition, string message, params object[] args)
        {
            throw new System.NotImplementedException();
        }

        public void Quit()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
