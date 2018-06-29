
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
    /// 触发器
    /// </summary>
    public class ETriggerItem : EComponent
    {
        public ELabel _title_lab;
        public EInput _des_input;                                           // 触发器的描述 输入框
        public ELabel _trigger_event_lab;                                   // 触发事件 文本
        public EEnumPopup _trigger_event_popup;                             // 触发事件 下拉列表

        public ELabel _condition_lab;                                       // 触发文本
        public EEnumPopup _condition_popup;                                 // 触发条件 下拉列表

        public ETriggerItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
        }

        public string GetTriggerEvt()
        {
            return "";
        }

        public EffectConditionInfo GetCondition()
        {
            return null;
        }

        #region private

        public void _init()
        {
            _title_lab = new ELabel("触发器:", 50);

            _des_input = new EInput("触发器描述", 200);


            _trigger_event_lab = new ELabel("触事件:", 50);
            _trigger_event_popup = new EEnumPopup(200);
            _trigger_event_popup.SetData(E_GLOBAL_EVT.buff_attach);

            _condition_lab = new ELabel("触发条件:", 50);
            _condition_popup = new EEnumPopup(200);
            _condition_popup.SetData(E_GLOBAL_EVT.buff_attach);
        }

        public void _init_position()
        {

            float tmp_height = 5;
            float tmp_width = 5;

            AddComponent(_title_lab, 10, 10);
            AddComponentRight(_des_input, _title_lab, tmp_width);

            AddComponentDown(_trigger_event_lab, _title_lab, tmp_height);
            AddComponentRight(_trigger_event_popup, _trigger_event_lab, tmp_width);

            AddComponentRight(_condition_lab, _trigger_event_popup, 30);
            AddComponentRight(_condition_popup, _condition_lab, tmp_height);

            Vector2 size = ERectHelper.GetSize(GetChilds());
            //Debug.Log(size);
            //SetSize(size.x + 5, size.y + 5);
        }

        public override void _on_draw()
        {
            base._on_draw();
        }

        #endregion
    }
}