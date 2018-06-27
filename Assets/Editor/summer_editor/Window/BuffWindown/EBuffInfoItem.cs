
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
using Summer;

namespace SummerEditor
{
    public class EBuffInfoItem : EComponent
    {
        public ELabel _buff_id_lab;                     // buff id title
        public EInput _buff_id_input;
        public ELabel _des_title_lab;                     // buff id title
        public ETextArea _des_text_area;                // buff模板描述

        public EButton _add_effect_btn;
        public EEnumPopup _effect_popup;                // 触发事件 下拉列表

        public EScrollView _scroll_view;

        //public EEffectInfoItem _effect_info_item;
        public EBuffInfoItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
        }

        public void _init()
        {
            _buff_id_lab = new ELabel("模板Id", 70);
            _buff_id_input = new EInput("1001", 200);

            _des_title_lab = new ELabel("模板描述", 70);
            _des_text_area = new ETextArea(200, 18 * 3);

            _add_effect_btn = new EButton("添加效果", 70);
            _add_effect_btn.on_click += AddEffect;

            _effect_popup = new EEnumPopup(200);
            _effect_popup.SetData(E_Enum_Effect_Type.value);


            _scroll_view = new EScrollView(Ew - 20, Eh - 100);
            //_scroll_view.SetBg(true);
        }

        private void AddEffect(EButton button)
        {
            string data = _effect_popup.GetData();

            E_Enum_Effect_Type eff_type = (E_Enum_Effect_Type)Enum.Parse(typeof(E_Enum_Effect_Type), data);

            EEffectInfoItem effect_info_item = new EEffectInfoItem(Ew - 70, 460, eff_type);
            //AddComponent(effect_info_item, 10, _des_text_area.Ey + _des_text_area.Eh / 2 + 5);
            _scroll_view.AddItem(effect_info_item);
        }

        public void _init_position()
        {
            AddComponent(_buff_id_lab, 10, 10);
            AddComponentRight(_buff_id_input, _buff_id_lab);

            AddComponentDown(_des_title_lab, _buff_id_lab);
            AddComponentDown(_des_text_area, _buff_id_input);

            AddComponentRight(_add_effect_btn, _buff_id_input, 50);
            AddComponentRight(_effect_popup, _add_effect_btn);

            AddComponent(_scroll_view, 10, _des_text_area.Ey + _des_text_area.Eh / 2 + 5);

        }
    }
}