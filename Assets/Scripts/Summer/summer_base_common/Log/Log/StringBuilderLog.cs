
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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Summer
{
    public class StringBuilderLog : I_Log
    {
        private static StringBuilderLog instance;
        public static StringBuilderLog Instance
        {
            get { return instance ?? (instance = new StringBuilderLog()); }
        }


        public const string FILEPATH = "/StringBuild_Log.txt";
        public StreamWriter sw;
        public StringBuilder sb = new StringBuilder();
        public StringBuilderLog()
        {
            Init();
        }

        public void Init()
        {
            Application.logMessageReceived += OnDebugCallBackHandler;
        }

        public void OnDebugCallBackHandler(string condition, string stack_trace, LogType type)
        {
            // Error Assert Warning Log Exception
            if (type == LogType.Log || type == LogType.Warning) return;
            Error(condition);
            Error(stack_trace);
        }

        public void Log(string message)
        {
            _print(string.Format("[Log][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Log(string message, params object[] args)
        {
            _print(string.Format("[Log][{0}]:  {1}", GetCurrentTime(), String.Format(message, args)));
        }

        public void Waring(string message)
        {
            _print(string.Format("[Waring][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Warning(string message, params object[] args)
        {
            _print(string.Format("[Warning][{0}]:  {1}", GetCurrentTime(), String.Format(message, args)));
        }

        public void Error(string message)
        {
            _print(string.Format("[Error][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Error(string message, params object[] args)
        {
            _print(string.Format("[Error][{0}]:  {1}", GetCurrentTime(), String.Format(message, args)));
        }

        public void Assert(bool condition, string message)
        {
            if (condition) return;
            _print(string.Format("[Assert][{0}]:  {1}", GetCurrentTime(), message));
        }

        public void Assert(bool condition, string message, params object[] args)
        {
            if (condition) return;
            _print(string.Format("[Assert][{0}]:  {1}", GetCurrentTime(), String.Format(message, args)));
        }

        public void Quit()
        {
            //1.初始化文件路径
#if UNITY_EDITOR
            string path = Application.dataPath;
#else
            string path = Application.persistentDataPath;
#endif

            int index = path.LastIndexOf('/');
            path = path.Substring(0, index);
            path = path + FILEPATH;
            //2.开启流
            sw = new StreamWriter(path, true);
            sw.WriteLine(sb.ToString());
            sw.Flush();
            sw.Close();
            sw = null;
        }

        public void _print(string mess)
        {
            sb.AppendLine(mess);
        }

        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff ");
        }


    }
}
