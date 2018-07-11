
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
            new LogEditorConfig("Assets/Scripts/Summer/summer_base_entities/Skill/Tool/SkillLog.cs", typeof(Summer.SkillLog)),
        };
    }
}
