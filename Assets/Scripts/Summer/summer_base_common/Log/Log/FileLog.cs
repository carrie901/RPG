
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
                                             
using System.IO;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 文件的Debug工具
    /// </summary>
    public class FileLog : I_Log
    {
        private static FileLog instance;
        public static FileLog Instance
        {
            get { return instance ?? (instance = new FileLog()); }
        }


        public const string FilePath = "/Unity_Log.txt";
        public StreamWriter sw;
        public FileLog()
        {
            Init();
        }

        public void Init()
        {
            //1.初始化文件路径
            string path = Application.dataPath;
            int index = path.LastIndexOf('/');
            path = path.Substring(0, index);
            path = path + FilePath;
            //2.开启流
            sw = new StreamWriter(path, true);

            Application.logMessageReceived += OnDebugCallBackHandler;
        }

        public void OnDebugCallBackHandler(string condition, string stack_trace, LogType type)
        {
            // Error Assert Warning Log Exception
            if (type == LogType.Log) return;
            //Error(condition);
            Error(stack_trace);
        }

        public void Log(string message)
        {
            _print(string.Format("[Log][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Log(string message, params object[] args)
        {
            _print(string.Format("[Log][{0}]:  {1}", GetCurrentTime(), string.Format(message, args)));
        }

        public void Waring(string message)
        {
            _print(string.Format("[Waring][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Warning(string message, params object[] args)
        {
            _print(string.Format("[Warning][{0}]:  {1}", GetCurrentTime(), string.Format(message, args)));
        }

        public void Error(string message)
        {
            _print(string.Format("[Error][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Error(string message, params object[] args)
        {
            _print(string.Format("[Error][{0}]:  {1}", GetCurrentTime(), string.Format(message, args)));
        }

        public void Assert(bool condition, string message)
        {
            if (condition) return;
            _print(string.Format("[Assert][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Assert(bool condition, string message, params object[] args)
        {
            if (condition) return;
            _print(string.Format("[Assert][{0}]:  {1}", GetCurrentTime(), string.Format(message, args)));
            sw.Flush();
        }

        public void Quit()
        {
            if (sw != null)
            {
                sw.Close();
                sw = null;
            }
        }

        public void _print(string mess)
        {
            sw.WriteLine(mess);
            sw.Flush();
        }

        private string GetCurrentTime()
        {
            return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff ");
        }


    }
}
