
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

using Summer;
using UnityEngine;

namespace SummerEditor
{

    public enum E_Enum_Effect_Type
    {
        attribute,
        value,
        state,
        action,
    }
    /// <summary>
    /// 效果
    /// </summary>
    public class EEffectInfoItem : EComponent
    {
        public ELabel _title_lab;
        public ETextArea _des_text_area;
        public ETriggerItem _trigger_item;
        public ETargetSelectorItem _target_select_item;
        public EComponent _e_component;
        public E_Enum_Effect_Type _eff_type;

        public ELabel _overlay_lab;                                 // 叠加文本
        public EEnumPopup _overlay_popup;                           // 叠加效果
        public EButton _remove_btn;
        public EEffectInfoItem(float width, float height, E_Enum_Effect_Type effect_type) : base(width, height)
        {
            _eff_type = effect_type;
            _init();
            _init_position();
        }

        public void _init()
        {
            _title_lab = new ELabel("效果描述:", 70);
            _des_text_area = new ETextArea(270, 18 * 3);

            _trigger_item = new ETriggerItem(Ew - 20, 55);
            _target_select_item = new ETargetSelectorItem(Ew - 20, 200);

            _overlay_lab = new ELabel("叠加效果:", 70);
            _overlay_popup = new EEnumPopup(200);
            _overlay_popup.SetData(E_GLOBAL_EVT.camera_effect_motion_blur);

            _remove_btn = new EButton("X", 20);

            if (_eff_type == E_Enum_Effect_Type.attribute)
            {
                _e_component = new EAttributeItem(Ew - 20, 115);
            }
            else if (_eff_type == E_Enum_Effect_Type.value)
            {
                _e_component = new EValueItem(Ew - 20, 115);
            }
            else if (_eff_type == E_Enum_Effect_Type.action)
            {
                _e_component = new EActionItem(Ew - 20, 115);
            }
            else if (_eff_type == E_Enum_Effect_Type.state)
            {
                _e_component = new EStateitem(Ew - 20, 75);
            }
        }

        public void _init_position()
        {
            AddComponent(_title_lab, 10, 10);
            AddComponent(_des_text_area, 5 + _title_lab.Ew, 10);

            //_overlay_lab.SetPositionRight(_des_text_area);
            AddComponentRight(_overlay_lab, _des_text_area);
            AddComponentRight(_overlay_popup, _overlay_lab);

            AddComponent(_remove_btn, E_Anchor.right | E_Anchor.up);
            AddComponent(_trigger_item, 10, 10 + _des_text_area.Eh + 5);
            AddComponentDown(_target_select_item, _trigger_item);

            AddComponentDown(_e_component, _target_select_item);
        }

        public override void _on_draw()
        {
            base._on_draw();
        }
    }
}
