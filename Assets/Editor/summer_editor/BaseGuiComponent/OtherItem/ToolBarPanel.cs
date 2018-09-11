
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
    public class ToolBarPanel : EComponent
    {
        #region 属性

        public EToolBar _bar;
        public Dictionary<int, EComponent> _panelMap
            = new Dictionary<int, EComponent>();

        public float[] _childWh;
        public event OnToolBarSelect OnSelect;
        #endregion

        #region Public

        public ToolBarPanel(List<string> labs, float width, float height) : base(width, height)
        {
            _init(labs);
        }

        public void AddPanel(int index, EComponent comp)
        {
            comp.Enabel = false;
            AddComponent(comp, 0, _bar.Size.y + 20);
            _panelMap.Add(index, comp);
        }

        public float[] GetChildWh()
        {
            return _childWh;
        }

        public void ResetSelect()
        {
            _bar.SelectIndex = 0;
            _onSelect(_bar);
        }

        #endregion

        #region Private Methods

        public void _init(List<string> labs)
        {
            _bar = new EToolBar(labs.ToArray(), Ew);

            _bar.OnSelect += _onSelect;
            AddComponent(_bar, 0, 5);
            _bar.SelectIndex = 0;
            _childWh = new[] { Ew, Eh - _bar.Size.y - 21 };
        }

        public void _onSelect(EToolBar toolBar)
        {
            int select = toolBar.SelectIndex;
            foreach (var info in _panelMap)
            {
                info.Value.Enabel = (info.Key == select);
            }
            if (OnSelect != null)
                OnSelect(toolBar);
        }

        #endregion


    }
}