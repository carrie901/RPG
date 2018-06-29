
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
    /// <summary>
    /// 属性变更
    /// </summary>
    public class EAttributeItem : EBaseffectInfoItem
    {
        public ELabel _title_lab;

        public ELabel _attribute_lab;                   // 触发事件 文本
        public EEnumPopup _value_popup;                 // 触发事件 下拉列表

        public EToggleBar _gudingzhi_toggle;            // 固定值
        public EIntInput _gudingzhi_input;              // 固定值数值
        public EToggleBar _baifenbi_toggle;             // 百分比
        public EIntInput _baifenbi_input;               // 百分比数值
        public ELabel _source_lab;                      // 数据源
        public EEnumPopup _value_source_popup;          // 触发事件 下拉列表

        public EAttributeItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
        }

        public override E_EffectType GetEffectType()
        {
            return E_EffectType.attribute;
        }

        public const string ATTRIBUTE = "AttributeType";
        public const string LIST = "Group";
        public const string DATA_TYPE = "DataUpdateType";
        public const string VALUE = "Value";
        public override TextNode GetValue()
        {
            TextNode text_node = new TextNode();
            text_node.Name = "Root";

            string result = _value_popup.GetData();
            text_node.AddAttribute(ATTRIBUTE, result);
            if (_gudingzhi_toggle.select)
            {
                TextNode child_node = text_node.AddNode(LIST);
                child_node.AddAttribute(DATA_TYPE, E_DataUpdateType.plus.ToString());
                child_node.AddAttribute(VALUE, _gudingzhi_input.GetValue().ToString());
            }

            if (_baifenbi_toggle.select)
            {
                TextNode child_node = text_node.AddNode(LIST);
                child_node.AddAttribute(DATA_TYPE, E_DataUpdateType.multiply_plus.ToString());
                child_node.AddAttribute(VALUE, _baifenbi_input.GetValue().ToString());
            }
            return text_node;
        }

        #region private

        public void _init()
        {
            _title_lab = new ELabel("属性变更", 50);

            _attribute_lab = new ELabel("指定属性:", 50);
            _value_popup = new EEnumPopup(200);
            _value_popup.SetData(E_CharAttributeRegion.anti_cri);

            _gudingzhi_toggle = new EToggleBar("固定值", 100);
            _gudingzhi_input = new EIntInput(1, 100);
            _baifenbi_toggle = new EToggleBar("百分比(100制)", 100);
            _baifenbi_input = new EIntInput(100, 100);

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
            AddComponentRight(_gudingzhi_input, _gudingzhi_toggle);

            AddComponentDown(_baifenbi_toggle, _gudingzhi_toggle);
            AddComponentRight(_baifenbi_input, _baifenbi_toggle);
            AddComponentRight(_source_lab, _baifenbi_input, 15);
            AddComponentRight(_value_source_popup, _source_lab);
        }

        #endregion
    }

    public abstract class EBaseffectInfoItem : EComponent
    {
        public EdNode _ednode;
        public EBaseffectInfoItem(float width, float height) : base(width, height)
        {

        }

        public abstract E_EffectType GetEffectType();
        public abstract TextNode GetValue();
    }
}
