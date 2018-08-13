
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

namespace SummerEditor
{
    public class ToolWin : EComponent
    {

        #region 属性

        public EToolBar _bar;
        public Dictionary<int, EComponent> _panel_map
            = new Dictionary<int, EComponent>();

        public CsvPanel _csv_panel;
        public AnimationPanel _animation_panel;
        public EffPerformancePanel _eff_perfor_panel;
        #endregion

        #region Override

        public ToolWin(float width, float height) : base(width, height)
        {
            _init();
        }

        #endregion

        #region Public



        #endregion

        #region Private Methods

        public void _init()
        {
            _bar = new EToolBar(new[] { "表格工具", "Animation优化", "AB资源报告", "特效检测" }, Ew / 2);
            _bar.on_select += on_select;
            AddComponent(_bar, 0, 10);

            float panel_width = Ew - 50;
            float panel_height = Eh - _bar.Size.y - 30;
            _csv_panel = new CsvPanel(panel_width, panel_height);
            AddPanel(_csv_panel, 0);

            _animation_panel = new AnimationPanel(panel_width, panel_height);
            AddPanel(_animation_panel, 1);


            _eff_perfor_panel = new EffPerformancePanel(panel_width, panel_height);
            AddPanel(_eff_perfor_panel, 3);
            _bar.SelectIndex = 1;
        }

        public void on_select(EToolBar tool_bar)
        {
            int select = tool_bar.SelectIndex;
            foreach (var info in _panel_map)
            {
                info.Value.Enabel = (info.Key == select);
            }

            if (select == 2)
            {
                AssetBundleReportWindow.Init();
            }
        }

        public void AddPanel(EComponent component, int index)
        {
            component.Enabel = false;
            AddComponent(component, 0, _bar.Size.y + 20);
            _panel_map.Add(index, component);
        }
        #endregion


    }
}