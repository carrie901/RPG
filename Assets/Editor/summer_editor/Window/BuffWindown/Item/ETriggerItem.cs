
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
        public ELabel _trigger_event_lab;                                   // 触发事件 文本
        public EStringPopup _trigger_event_popup;                           // 触发事件 下拉列表

        public ELabel _condition_lab;                                       // 触发文本
        public EEnumPopup _condition_popup;                                 // 触发条件 下拉列表

        public ETriggerItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
        }

        public void SetData(TextNode node)
        {
            string trigger_event = node.GetAttribute(EffectTemplateInfo.TRIGGER_EVENT);

            _trigger_event_popup.SetSelect(GetTriggerEvent(trigger_event));
        }

        public TextNode GetValue()
        {
            TextNode node = new TextNode(EffectTemplateInfo.TRIGGER_NODE_NAME);
            StringStringPopupInfo info = _trigger_event_popup.GetValue() as StringStringPopupInfo;
            node.AddAttribute(EffectTemplateInfo.TRIGGER_EVENT, info.value);
            return node;
        }

        #region private

        public void _init()
        {
            _trigger_event_lab = new ELabel("触发事件:", 50);
            _trigger_event_popup = new EStringPopup("", 200);

            List<StringPopupInfo> info = GetPop();
            _trigger_event_popup.SetData(info);

            _condition_lab = new ELabel("触发条件:", 50);
            _condition_popup = new EEnumPopup(200);
            _condition_popup.SetData(E_GLOBAL_EVT.none);
        }

        public void _init_position()
        {

            float tmp_height = 5;
            float tmp_width = 5;

            AddComponent(_trigger_event_lab, 10, 10);
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

        #region static 

        public static List<StringPopupInfo> _pop;
        public static List<StringPopupInfo> GetPop()
        {
            if (_pop != null)
                return _pop;
            _pop = new List<StringPopupInfo>();
            foreach (var var_info in _evt)
            {
                StringPopupInfo info = new StringStringPopupInfo
                {
                    des = var_info.Value,
                    value = var_info.Key.ToString()
                };
                _pop.Add(info);
            }
            return _pop;
        }

        public static string GetTriggerEvent(string des)
        {
            foreach (var info in _evt)
            {
                if (info.Key.ToString() == des)
                    return info.Value;
            }
            Debug.Log("Error 没找到对应的 数据");
            return string.Empty;
        }

        /*public static string[,] evt =
        {
            { TriggerEvt.buff_on_tick,"Buff 间隔触发"},
            { TriggerEvt.buff_on_attach,"Buff添加到角色身上"},
            { TriggerEvt.buff_add_layer,"Buff层级增加"},
            { TriggerEvt.buff_layer_max,"Buff层级到达最大等级"},
            { TriggerEvt.buff_remove_layer,"Buff层级减少"},
            { TriggerEvt.buff_on_detach,"Buff从角色身上移除"}
        };*/

        public static Dictionary<E_Buff_Event, string> _evt = new Dictionary<E_Buff_Event, string>()
        {
            { E_Buff_Event.buff_on_tick,"Buff 间隔触发"},
            { E_Buff_Event.buff_on_attach,"Buff添加到角色身上"},
            { E_Buff_Event.buff_add_layer,"Buff层级增加"},
            { E_Buff_Event.buff_layer_max,"Buff层级到达最大等级"},
            { E_Buff_Event.buff_remove_layer,"Buff层级减少"},
            { E_Buff_Event.buff_on_detach,"Buff从角色身上移除"}
        };




        #endregion
    }
}