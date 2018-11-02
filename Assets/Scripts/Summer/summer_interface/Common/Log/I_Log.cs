using UnityEngine;
//=============================================================================
// Author : 
// CreateTime : 2017-5-31 15:27:25
// FileName : Ilog.cs
//=============================================================================

namespace Summer
{
    public interface I_Log
    {
        void Log(string message);
        void Log(string message, params object[] args);

        void Waring(string message);
        void Warning(string message, params object[] args);

        void Error(string message);
        void Error(string message, params object[] args);

        void Assert(bool condition, string message);
        void Assert(bool condition, string message, params object[] args);

        void Quit();
    }
}
