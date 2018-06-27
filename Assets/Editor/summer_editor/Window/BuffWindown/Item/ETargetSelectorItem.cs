
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
    public class ETargetSelectorItem : EComponent
    {
        public EButton _target_filter_btn;                                  // 增加目标过滤类型   
        public EEnumPopup _target_popup;                                    // 目标类型 下拉列表
        public EScrollView _scrollview;
        public ETargetSelectorItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
            _init_click();
        }

        public void _init()
        {
            _target_filter_btn = new EButton("增加过滤类型", 100);
            _target_popup = new EEnumPopup(200);
            _target_popup.SetData(E_GLOBAL_EVT.buff_attach);

            _scrollview = new EScrollView(Ew - 20, Eh - 50);
            _scrollview.SetColor(Color.black);
        }

        public void _init_position()
        {
            AddComponent(_target_filter_btn, 10, 10);
            AddComponentRight(_target_popup, _target_filter_btn, 10);

            AddComponent(_scrollview, 10, 40);

            //AddComponentDown(_scrollview, _title_lab, 10);

            //Vector2 size = ERectHelper.GetSize(GetChilds());
            //SetSize(size.x + 5, size.y + 5);
        }

        public void _init_click()
        {
            _target_filter_btn.on_click += AddTargetFilter;
        }

        private void AddTargetFilter(EButton button)
        {
            ETargetItem item = new ETargetItem(_scrollview.Ew - 30, 30);
            item.SetData("测试:", E_GLOBAL_EVT.buff_attach);
            _scrollview.AddItem(item);
            item.Init();
            item._remove_btn.on_click += OnRemoveItem;
        }

        private void OnRemoveItem(EButton button)
        {
            //_scrollview.Remove();
        }
    }

    public class ETargetItem : EComponent
    {
        public ELabel _title_lab;
        public EButton _remove_btn;                                         // 移除  
        public EEnumPopup _target_popup;                                    // 过滤类型   下拉列表
        public ETargetItem(float width, float height) : base(width, height)
        {
            _remove_btn = new EButton("X", 20);
        }

        public void SetData(string text, E_GLOBAL_EVT type)
        {
            _title_lab = new ELabel(text, 50);
            _target_popup = new EEnumPopup(200);
            _target_popup.SetData(type);
        }

        public void Init()
        {
            AddComponent(_title_lab, 10, 10);
            AddComponentRight(_target_popup, _title_lab, 10);

            AddComponent(_remove_btn, E_Anchor.right | E_Anchor.up);
            //AddComponentRight(_target_filter_btn, _title_lab, 80);

            //Vector2 size = ERectHelper.GetSize(GetChilds());
            //SetSize(size.x + 5, size.y + 5);
        }


    }
}

