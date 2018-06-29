
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
using System.Collections.Generic;

namespace SummerEditor
{
    public class ETargetSelectorItem : EComponent
    {
        public EButton _target_filter_btn;                                  // 增加目标过滤类型   
        public EStringPopup _target_popup;                                    // 目标类型 下拉列表
        public EScrollView _scrollview;
        public ETargetSelectorItem(float width, float height) : base(width, height)
        {
            _init();
            _init_position();
            _init_click();
        }

        public TextNode GetValue()
        {
            TextNode node = new TextNode();
            node.Name = "Root";
            List<ERect> items = _scrollview.GetChilds();
            for (int i = 0; i < items.Count; i++)
            {
                ETargetItem item = items[i] as ETargetItem;
                if (item == null) continue;
                node.AddNode(item.GetValue());
            }

            return node;
        }

        public List<StringPopupInfo> GetInof()
        {
            List<StringPopupInfo> infos = new List<StringPopupInfo>();
            foreach (var info in TargetSelectFactory._target_select_map)
            {
                StringPopupInfo s_info = new StringPopupInfo()
                {
                    des = info.Key
                };
                infos.Add(s_info);
            }
            return infos;
        }


        #region private 

        public void _init()
        {
            _target_filter_btn = new EButton("增加过滤类型", 100);
            _target_popup = new EStringPopup("", 200);
            _target_popup.SetData(GetInof());

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
            item.SetData(GetInof());
            _scrollview.AddItem(item);
            item.Init();
            item._remove_btn.on_click += OnRemoveItem;
        }

        private void OnRemoveItem(EButton button)
        {
            //_scrollview.Remove();
        }

        #endregion
    }

    public class ETargetItem : EComponent
    {
        public EButton _remove_btn;                                         // 移除  
        public EStringPopup _target_popup;                                    // 过滤类型   下拉列表
        public ETargetItem(float width, float height) : base(width, height)
        {
            _remove_btn = new EButton("X", 20);
        }

        public void SetData(List<StringPopupInfo> infos)
        {
            _target_popup = new EStringPopup("", 200);
            _target_popup.SetData(infos);
        }

        public TextNode GetValue()
        {
            StringPopupInfo info = _target_popup.GetValue();
            TextNode node = new TextNode();
            node.Name = info.des;
            return node;
        }

        public void Init()
        {
            AddComponent(_target_popup, 10, 10);
            AddComponent(_remove_btn, E_Anchor.right | E_Anchor.up);
            //AddComponentRight(_target_filter_btn, _title_lab, 80);

            //Vector2 size = ERectHelper.GetSize(GetChilds());
            //SetSize(size.x + 5, size.y + 5);
        }
    }
}

