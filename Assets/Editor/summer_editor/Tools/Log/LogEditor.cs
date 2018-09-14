
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
        private static bool OnOpenAsset(int instance_id, int line)
        {
            for (int i = LogEditorConst.LogEditorConfigs.Length - 1; i >= 0; --i)
            {
                LogEditorConfig config_tmp = LogEditorConst.LogEditorConfigs[i];
                UpdateLogInstanceId(config_tmp);
                
                if (instance_id == config_tmp.instance_id)
                {
                    string statck_track = GetStackTrace();
                    if (!string.IsNullOrEmpty(statck_track))
                    {
                        string[] file_names = statck_track.Split('\n');
                        string file_name = GetCurrentFullFileName(file_names);
                        int file_line = LogFileNameToFileLine(file_name);
                        file_name = GetRealFileName(file_name);

                        AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(file_name), file_line);
                        return true;
                    }
                    break;
                }
            }

            return false;

        }

        private static string GetStackTrace()
        {
            var console_window_type = typeof(EditorWindow).Assembly.GetType("UnityEditor.ConsoleWindow");
            var field_info = console_window_type.GetField("ms_ConsoleWindow", BindingFlags.Static | BindingFlags.NonPublic);
            if (field_info == null) return "";
            var console_window_instance = field_info.GetValue(null);

            if (null != console_window_instance)
            {
                if ((object)EditorWindow.focusedWindow == console_window_instance)
                {
                    // Get ListViewState in ConsoleWindow
                    // var listViewStateType = typeof(EditorWindow).Assembly.GetType("UnityEditor.ListViewState");
                    // fieldInfo = consoleWindowType.GetField("m_ListView", BindingFlags.Instance | BindingFlags.NonPublic);
                    // var listView = fieldInfo.GetValue(consoleWindowInstance);

                    // Get row in listViewState
                    // fieldInfo = listViewStateType.GetField("row", BindingFlags.Instance | BindingFlags.Public);
                    // int row = (int)fieldInfo.GetValue(listView);

                    // Get m_ActiveText in ConsoleWindow
                    field_info = console_window_type.GetField("m_ActiveText", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (field_info == null) return string.Empty;
                    string active_text = field_info.GetValue(console_window_instance).ToString();

                    return active_text;
                }
            }
            return "";
        }

        private static void UpdateLogInstanceId(LogEditorConfig config)
        {
            if (config.instance_id > 0)
            {
                return;
            }

            var asset_load_tmp = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(config.log_script_path);
            if (null == asset_load_tmp)
            {
                throw new Exception("not find asset by path=" + config.log_script_path);
            }
            config.instance_id = asset_load_tmp.GetInstanceID();
        }

        private static string GetCurrentFullFileName(string[] file_names)
        {
            string ret_value = "";
            int find_index = -1;

            for (int i = file_names.Length - 1; i >= 0; --i)
            {
                bool is_custom_log = false;
                for (int j = LogEditorConst.LogEditorConfigs.Length - 1; j >= 0; --j)
                {
                    if (file_names[i].Contains(LogEditorConst.LogEditorConfigs[j].log_type_name))
                    {
                        is_custom_log = true;
                        break;
                    }
                }
                if (is_custom_log)
                {
                    find_index = i;
                    break;
                }
            }

            if (find_index >= 0 && find_index < file_names.Length - 1)
            {
                ret_value = file_names[find_index + 1];
            }

            return ret_value;
        }

        private static string GetRealFileName(string file_name)
        {
            int index_start = file_name.IndexOf("(at ", StringComparison.Ordinal) + "(at ".Length;
            int index_end = ParseFileLineStartIndex(file_name) - 1;

            file_name = file_name.Substring(index_start, index_end - index_start);
            return file_name;
        }

        private static int LogFileNameToFileLine(string file_name)
        {
            int find_index = ParseFileLineStartIndex(file_name);
            string string_parse_line = "";
            for (int i = find_index; i < file_name.Length; ++i)
            {
                var char_check = file_name[i];
                if (!IsNumber(char_check))
                {
                    break;
                }
                else
                {
                    string_parse_line += char_check;
                }
            }

            return int.Parse(string_parse_line);
        }

        private static int ParseFileLineStartIndex(string file_name)
        {
            int ret_value = -1;
            for (int i = file_name.Length - 1; i >= 0; --i)
            {
                var char_check = file_name[i];
                bool is_number = IsNumber(char_check);
                if (is_number)
                {
                    ret_value = i;
                }
                else
                {
                    if (ret_value != -1)
                    {
                        break;
                    }
                }
            }
            return ret_value;
        }

        private static bool IsNumber(char c)
        {
            return c >= '0' && c <= '9';
        }
    }

    public class LogEditorConfig
    {
        public readonly string log_script_path;
        public readonly string log_type_name;
        public int instance_id;

        public LogEditorConfig(string log_script_path_tmp, System.Type log_type)
        {
            log_script_path = log_script_path_tmp;
            log_type_name = log_type.FullName;
        }
    }
}