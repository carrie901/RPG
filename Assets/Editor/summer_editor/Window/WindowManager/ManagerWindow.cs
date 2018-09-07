
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
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{

    public class ManagerWindow : EditorWindow
    {

        #region 静态属性

        protected static ManagerWindow _window;
        protected static float t_width = 1600;
        protected static float t_height = 800;
        [MenuItem("策划/编辑器")]
        static void Init()
        {
            _window = GetWindow<ManagerWindow>();
            _window.titleContent = new GUIContent("项目工具");
            //_window.minSize = new Vector2(t_width, t_height);
            //_window.maxSize = new Vector2(t_width + 40, t_height + 40);
            _window.Show();
            _window.GetAssets();
        }

        #endregion

        #region 属性

        public EComponent _container;                                           // 容器
        public EToolBar _bar;
        public Dictionary<int, EComponent> _panel_map
            = new Dictionary<int, EComponent>();

        public ToolWin _tool_win;

        #endregion

        #region MONO Override

        public void GetAssets()
        {
            _container = new EComponent(t_width, t_height);
            _container.SetBg(false);
            _container.ResetPosition(t_width / 2, t_height / 2);
            _container.SetBg(0, 0, 0);

            _bar = new EToolBar(new[] { "打包", "工具", "优化检测", "Buff编辑器", "技能编辑器" }, t_width / 2);
            _bar.OnSelect += on_select;
            _container.AddComponent(_bar, 0, 10);

            _tool_win = new ToolWin(t_width - 50, t_height - _bar.Size.y - 30);
            AddPanel(_tool_win);


            _panel_map.Add(1, _tool_win);

            _bar.SelectIndex = 1;
        }

        #endregion

        #region Public



        #endregion

        #region 按钮响应

        public void on_select(EToolBar tool_bar)
        {
            int select = tool_bar.SelectIndex;
            foreach (var info in _panel_map)
            {
                info.Value.Enabel = (info.Key == select);
            }

            if (select == 3)
            {
                BuffWindow.Init();
            }
        }

        #endregion

        #region Private Methods

        void OnGUI()
        {
            if (_container != null)
            {
                _container.OnDraw(0, 0);
            }
        }

        public void AddPanel(EComponent component)
        {
            component.Enabel = false;
            _container.AddComponent(component, 0, _bar.Size.y + 20);
        }

        #endregion
    }

    public class TreeMainItem : EComponent
    {
        public Action<string> on_action;
        public TreeMainItem(float width, List<string> names) : base(width, 5 + names.Count * (DEFAULT_HEIGHT + 4))
        {
            InitInfo(names);
        }


        public void InitInfo(List<string> names)
        {
            float tmp_y = 5;
            for (int i = 0; i < names.Count; i++)
            {
                EButton button = new EButton(names[i], Ew - 5);
                button.OnClick += OnClick;
                AddComponent(button, 5, tmp_y);
                tmp_y += button.Size.y + 5;
            }
        }

        public void OnClick(EButton button)
        {
            if (on_action != null)
                on_action(button._text);
        }
    }
}