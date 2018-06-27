
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
using Summer;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 动作
    /// </summary>
    public class EActionItem : EComponent
    {
        public ELabel _title_lab;

        public ELabel _attribute_lab;                   // 触发事件 文本
        public EEnumPopup _value_popup;                 // 触发事件 下拉列表

        public EToggleBar _gudingzhi_toggle;            // 固定值
        public EToggleBar _baifenbi_toggle;             // 百分比
        public ELabel _source_lab;                      // 数据源
        public EEnumPopup _value_source_popup;          // 触发事件 下拉列表

        public EActionItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
        }

        public void _init()
        {
            _title_lab = new ELabel("动作", 50);

            _attribute_lab = new ELabel("指定属性:", 50);
            _value_popup = new EEnumPopup(200);
            _value_popup.SetData(E_GLOBAL_EVT.buff_attach);

            _gudingzhi_toggle = new EToggleBar("固定值", 120);
            _gudingzhi_toggle.SetBg(true);
            _baifenbi_toggle = new EToggleBar("百分比", 120);

            _source_lab = new ELabel("数据源:", 50);
            _value_source_popup = new EEnumPopup(200);
            _value_source_popup.SetData(E_GLOBAL_EVT.buff_attach);
        }

        public void _init_position()
        {
            AddComponent(_title_lab, 10, 10);
            AddComponentDown(_attribute_lab, _title_lab);
            AddComponentRight(_value_popup, _attribute_lab);

            AddComponentDown(_gudingzhi_toggle, _attribute_lab);

            AddComponentDown(_baifenbi_toggle, _gudingzhi_toggle);
            AddComponentRight(_source_lab, _baifenbi_toggle, 15);
            AddComponentRight(_value_source_popup, _source_lab);
        }

    }

}