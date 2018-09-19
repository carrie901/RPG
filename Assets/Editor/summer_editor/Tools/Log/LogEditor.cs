
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
using System.Reflection;
using UnityEditor;
namespace SummerEditor
{
    /// <summary>
    /// 原文https://blog.csdn.net/l449612236/article/details/76087616
    /// Log重定向
    /// </summary>
    public static class LogEditor
    {
        [UnityEditor.Callbacks.OnOpenAssetAttribute(-1)]
        private static bool OnOpenAsset(int instanceId, int line)
        {
            for (int i = LogEditorConst.LogEditorConfigs.Length - 1; i >= 0; --i)
            {
                LogEditorConfig configTmp = LogEditorConst.LogEditorConfigs[i];
                UpdateLogInstanceId(configTmp);
                
                if (instanceId == configTmp._instanceId)
                {
                    string statckTrack = GetStackTrace();
                    if (!string.IsNullOrEmpty(statckTrack))
                    {
                        string[] fileNames = statckTrack.Split('\n');
                        string fileName = GetCurrentFullFileName(fileNames);
                        int fileLine = LogFileNameToFileLine(fileName);
                        fileName = GetRealFileName(fileName);

                        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(fileName), fileLine);
                        return true;
                    }
                    break;
                }
            }

            return false;

        }

        private static string GetStackTrace()
        {
            var consoleWindowType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
            var fieldInfo = consoleWindowType.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
            if (fieldInfo == null) return "";
            var consoleWindowInstance = fieldInfo.GetValue(null);

            if (null != consoleWindowInstance)
            {
                if ((object)EditorWindow.focusedWindow == consoleWindowInstance)
                {
                    // Get ListViewState in ConsoleWindow
                    // var listViewStateType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ListViewState");
                    // fieldInfo = consoleWindowType.GetField("m_ListView", BindingFlags.Instance | BindingFlags.NonPublic);
                    // var listView = fieldInfo.GetValue(consoleWindowInstance);

                    // Get row in listViewState
                    // fieldInfo = listViewStateType.GetField("row", BindingFlags.Instance | BindingFlags.Public);
                    // int row = (int)fieldInfo.GetValue(listView);

                    // Get m_ActiveText in ConsoleWindow
                    fieldInfo = consoleWindowType.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (fieldInfo == null) return string.Empty;
                    string activeText = fieldInfo.GetValue(consoleWindowInstance).ToString();

                    return activeText;
                }
            }
            return "";
        }

        private static void UpdateLogInstanceId(LogEditorConfig config)
        {
            if (config._instanceId > 0)
            {
                return;
            }

            var assetLoadTmp = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(config.LogScriptPath);
            if (null == assetLoadTmp)
            {
                throw new Exception("not find asset by path=" + config.LogScriptPath);
            }
            config._instanceId = assetLoadTmp.GetInstanceID();
        }

        private static string GetCurrentFullFileName(string[] fileNames)
        {
            string retValue = "";
            int findIndex = -1;

            for (int i = fileNames.Length - 1; i >= 0; --i)
            {
                bool isCustomLog = false;
                for (int j = LogEditorConst.LogEditorConfigs.Length - 1; j >= 0; --j)
                {
                    if (fileNames[i].Contains(LogEditorConst.LogEditorConfigs[j].LogTypeName))
                    {
                        isCustomLog = true;
                        break;
                    }
                }
                if (isCustomLog)
                {
                    findIndex = i;
                    break;
                }
            }

            if (findIndex >= 0 && findIndex < fileNames.Length - 1)
            {
                retValue = fileNames[findIndex + 1];
            }

            return retValue;
        }

        private static string GetRealFileName(string fileName)
        {
            int indexStart = fileName.IndexOf("(at ", StringComparison.Ordinal) + "(at ".Length;
            int indexEnd = ParseFileLineStartIndex(fileName) - 1;

            fileName = fileName.Substring(indexStart, indexEnd - indexStart);
            return fileName;
        }

        private static int LogFileNameToFileLine(string fileName)
        {
            int findIndex = ParseFileLineStartIndex(fileName);
            string stringParseLine = "";
            for (int i = findIndex; i < fileName.Length; ++i)
            {
                var charCheck = fileName[i];
                if (!IsNumber(charCheck))
                {
                    break;
                }
                else
                {
                    stringParseLine += charCheck;
                }
            }

            return int.Parse(stringParseLine);
        }

        private static int ParseFileLineStartIndex(string fileName)
        {
            int retValue = -1;
            for (int i = fileName.Length - 1; i >= 0; --i)
            {
                var charCheck = fileName[i];
                bool isNumber = IsNumber(charCheck);
                if (isNumber)
                {
                    retValue = i;
                }
                else
                {
                    if (retValue != -1)
                    {
                        break;
                    }
                }
            }
            return retValue;
        }

        private static bool IsNumber(char c)
        {
            return c >= '0' && c <= '9';
        }
    }

    public class LogEditorConfig
    {
        public readonly string LogScriptPath;
        public readonly string LogTypeName;
        public int _instanceId;

        public LogEditorConfig(string logScriptPathTmp, System.Type logType)
        {
            LogScriptPath = logScriptPathTmp;
            LogTypeName = logType.FullName;
        }
    }
}