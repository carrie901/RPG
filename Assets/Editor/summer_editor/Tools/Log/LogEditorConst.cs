
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

namespace SummerEditor
{
    public class LogEditorConst
    {

        //Add your custom Log class here
        public static readonly LogEditorConfig[] log_editor_configs = new LogEditorConfig[]
        {
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogManager.cs", typeof(Summer.LogManager)),
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/Log/UnityLog.cs", typeof(Summer.UnityLog)),
            
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogModules/ActionLog.cs", typeof(Summer.ActionLog)),
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogModules/AnimationLog.cs", typeof(Summer.AnimationLog)),
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogModules/BuffLog.cs", typeof(Summer.BuffLog)),
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogModules/NetLog.cs", typeof(Summer.NetLog)),
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogModules/ResLog.cs", typeof(Summer.ResLog)),
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogModules/SkillLog.cs", typeof(Summer.SkillLog)),
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_common/Log/LogModules/PanelLog.cs", typeof(Summer.PanelLog)),
        };
    }
}
