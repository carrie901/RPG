using UnityEngine;
//=============================================================================
// Author : 
// CreateTime : 2017-5-31 15:27:25
// FileName : Ilog.cs
//=============================================================================

using System;
using System.IO;

namespace Summer
{
    public interface ILog
    {
        void Log(string message);
        void Log(string message, params object[] args);

        void Waring(string message);
        void Warning(string message, params object[] args);

        void Error(string message);
        void Error(string message, params object[] args);

        void Assert(bool condition, string message);
        void Assert(bool condition, string message, params object[] args);
    }

    /// <summary>
    /// 文件的Debug工具
    /// </summary>
    public class FileLog : ILog
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
            sw.Flush();
        }

        public void _print(string mess)
        {
            sw.WriteLine(mess);
            sw.Flush();
        }

        private string GetCurrentTime()
        {
            return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }


    }

    /// <summary>
    /// Unity的Debug工具
    /// </summary>
    public class UnityLog : ILog
    {

        private static UnityLog instance;
        public static UnityLog Instance
        {
            get { return instance ?? (instance = new UnityLog()); }
        }


        public void Log(string message)
        {
            Debug.Log(message);
        }

        public void Log(string message, params object[] args)
        {
            Debug.LogFormat(message, args);
        }

        public void Waring(string message)
        {
            Debug.LogWarning(message);
        }

        public void Warning(string message, params object[] args)
        {
            Debug.LogWarningFormat(message, args);
        }

        public void Error(string message)
        {
            Debug.LogError(message);
        }

        public void Error(string message, params object[] args)
        {
            Debug.LogErrorFormat(message, args);
        }

        public void Assert(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }

        public void Assert(bool condition, string message, params object[] args)
        {
            Debug.AssertFormat(condition, message, args);
        }
    }
}
