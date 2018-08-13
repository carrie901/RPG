
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

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class BuffWindow : EditorWindow
    {

        #region 属性

        public static BuffWindow window;

        public EComponent _container;
        #endregion

        #region static

        static float buff_width = 1400;
        static float buff_height = 800;

        [MenuItem("Tools/面板/Buff编辑器")]
        public static void Init()
        {
            window = EditorWindow.GetWindow<BuffWindow>();   // 创建自定义窗体
            window.titleContent = new GUIContent("构建树视图");         // 窗口的标题
                                                                   //window.minSize = new Vector2(t_width, t_height);
                                                                   //window.maxSize = new Vector2(t_width + 40, t_height + 40);
            window.OnInit();
            window.Show();

            //_instance.GetAssets();
            // 创建树
        }


        #endregion



        #region Public

        void OnInit()
        {
            _container = new EComponent(buff_width, buff_height);
            _container.SetBg(false);
            _container.show_box = false;
            _container.ResetPosition(buff_width / 2, buff_height / 2);
            _container.SetBg(0, 0, 0);


            EBuffInfoItem buff_info = new EBuffInfoItem(720, buff_height);
            _container.AddComponent(buff_info, 10, 10);
            /*
            _container.AddComponent(trigger_item, 10, 10);

            target_select_item = new ETargetSelectorItem(trigger_item.Ew, trigger_item.Eh + 150);
            _container.AddComponentDown(target_select_item, trigger_item, 10);

            EAttributeItem attribute = new EAttributeItem(550, 115);
            _container.AddComponentDown(attribute, target_select_item);

            EValueItem value_item = new EValueItem(550, 115);
            _container.AddComponentDown(value_item, attribute);

            EActionItem action_item = new EActionItem(550, 115);
            _container.AddComponentDown(action_item, value_item);

            EStateitem state_item = new EStateitem(550, 75);
            _container.AddComponentDown(state_item, action_item);*/
        }

        string[] options = new string[] { "Rigidbody", "Box Collider", "Sphere Collider" };



        void OnGUI()
        {
            if (_container != null)
            {
                _container.OnDraw(0, 0);
            }
            int index = 0;
            //index = EditorGUI.Popup(new UnityEngine.Rect(0, 0, position.width, 20), "Component:", index, options);

            /* display = EditorGUI.EnumPopup(
             Rect(3, 3, position.width - 6, 15),
             "Show:",
             display);*/
        }


        #endregion

        #region Private Methods



        #endregion
    }
}