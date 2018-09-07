
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
using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    public class EBuffInfoItem : EComponent
    {
        public ELabelIntInput _buff_id_input;
        public ELabel _des_title_lab;                   // buff id title
        public ETextArea _des_text_area;                // buff模板描述

        public EButton _add_effect_btn;
        public EEnumPopup _effect_popup;                // 触发事件 下拉列表

        public ELabelIntInput duration_input;
        public ELabelIntInput interval_time_input;
        public ELabelIntInput max_layer_input;
        public EScrollView _scroll_view;

        public EButton _save_btn;
        //public EEffectInfoItem _effect_info_item;
        public EBuffInfoItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
        }

        public void _init()
        {
            _buff_id_input = new ELabelIntInput("模板Id", 70, 1001, 200);

            _des_title_lab = new ELabel("模板描述", 70);
            _des_text_area = new ETextArea(200, 18 * 4);

            _add_effect_btn = new EButton("添加效果", 70);
            _add_effect_btn.OnClick += AddEffect;

            _effect_popup = new EEnumPopup(200);
            _effect_popup.SetData(E_EffectType.attribute);

            duration_input = new ELabelIntInput("持续时间(ms)", 100, 1000, 100);
            interval_time_input = new ELabelIntInput("间隔时间(ms)", 100, 1000, 100);
            max_layer_input = new ELabelIntInput("最大层级", 100, 1, 100);
            _scroll_view = new EScrollView(Ew - 20, Eh - 100);

            _save_btn = new EButton("保存", 50);
            _save_btn.OnClick += OnSaveInfo;
            //_scroll_view.SetBg(true);
        }

        private void AddEffect(EButton button)
        {
            string data = _effect_popup.GetData();

            E_EffectType eff_type = (E_EffectType)Enum.Parse(typeof(E_EffectType), data);

            EEffectInfoItem effect_info_item = new EEffectInfoItem(Ew - 70, 460, eff_type);
            //AddComponent(effect_info_item, 10, _des_text_area.Ey + _des_text_area.Eh / 2 + 5);
            _scroll_view.AddItem(effect_info_item);
        }

        private void OnSaveInfo(EButton button)
        {
            BuffTemplateInfo info = ScriptableObjectHelper.Create<BuffTemplateInfo>();
            info.desc = "";
            info.id = _buff_id_input.Value;
            info.duration = _buff_id_input.Value;
            info.interval_time = interval_time_input.Value;
            info.max_layer = max_layer_input.Value;
            info._effs.Clear();

            List<ERect> effect_list = _scroll_view.GetChilds();
            for (int i = 0; i < effect_list.Count; i++)
            {
                EEffectInfoItem item = effect_list[i] as EEffectInfoItem;
                if (item == null) continue;

                EffectTemplateInfo eff_info = item.GetValue();
                info._effs.Add(eff_info);
            }
            AssetDatabase.CreateAsset(info, "Assets/Resources/test_buff_dat.asset");
            //ScriptableObjectHelper.Save<BuffTemplateInfo>(info, "Assets/Resources", "test_buff_dat");
        }

        public void _init_position()
        {
            AddComponent(_buff_id_input, 10, 10);

            AddComponentDown(_des_title_lab, _buff_id_input);
            AddComponentRight(_des_text_area, _des_title_lab);

            AddComponentRight(_add_effect_btn, _buff_id_input, 50);
            AddComponentRight(_effect_popup, _add_effect_btn);
            AddComponentDown(duration_input, _add_effect_btn);
            AddComponentDown(interval_time_input, duration_input);
            AddComponentDown(max_layer_input, interval_time_input);
            AddComponent(_scroll_view, 10, _des_text_area.Ey + _des_text_area.Eh / 2 + 5);

            AddComponent(_save_btn, E_Anchor.Right | E_Anchor.UP);
        }


    }
}