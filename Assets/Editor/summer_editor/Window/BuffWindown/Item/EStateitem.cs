
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

namespace SummerEditor
{
    public class EStateitem : EBaseffectInfoItem
    {
        public ELabel _title_lab;

        public ELabel _attribute_lab;                   // 触发事件 文本
        public EEnumPopup _value_popup;                 // 触发事件 下拉列表

        public EStateitem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
        }

        public void _init()
        {
            _title_lab = new ELabel("状态变更", 50);

            _attribute_lab = new ELabel("指定状态:", 50);
            _value_popup = new EEnumPopup(200);
            _value_popup.SetData(E_GLOBAL_EVT.buff_attach);
        }

        public void _init_position()
        {
            AddComponent(_title_lab, 10, 10);
            AddComponentDown(_attribute_lab, _title_lab);
            AddComponentRight(_value_popup, _attribute_lab);
        }

        /*public  EffectDesInfo GetValue()
        {
            throw new System.NotImplementedException();
        }*/
        public override E_EffectType GetEffectType()
        {
            throw new System.NotImplementedException();
        }

        public override TextNode GetValue()
        {
            throw new System.NotImplementedException();
        }
    }

}