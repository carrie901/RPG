
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
using System.Collections.Generic;

namespace SummerEditor
{
    public class ExtendTableComponent : EComponent
    {
        #region 属性

        public List<EButton> _title_btns = new List<EButton>();
        public EScrollView _scrollview;
        public ExtendPageItem _page_item;
        public List<ExtendTableItem> _items = new List<ExtendTableItem>();

        public delegate void UpdateItem(int page, int interval, List<ExtendTableItem> items);

        public event UpdateItem OnUpdateItem;
        #endregion

        #region override

        public ExtendTableComponent(float width, float height) : base(width, height)
        {
        }

        #endregion

        #region public 

        public void SetInfo(string[] titles, float[] titles_width, int max_size)
        {
            ERect first = null;
            for (int i = 0; i < titles.Length; i++)
            {
                EButton title_btn = new EButton(titles[i], titles_width[i]);
                _title_btns.Add(title_btn);
                if (first == null)
                {
                    AddComponent(title_btn, 2, 5);
                }
                else
                {
                    AddComponentRight(title_btn, first, 0);
                }

                first = title_btn;
            }

            _scrollview = new EScrollView(Ew, Eh - DEFAULT_HEIGHT * 2 - 20);
            AddComponent(_scrollview, 0, DEFAULT_HEIGHT + 10);
            _scrollview.SetHeightInterval(2);

            _page_item = new ExtendPageItem();
            _page_item.SetPageInfo(100, 10);
            AddComponent(_page_item, E_Anchor.down_center);
            _page_item.ResetPosition(_page_item.Ex, _page_item.Ey - 5);
            _page_item.OnUpdatePage += _on_update_page;
        }

        public void ResetInfo()
        {
            _page_item.ResetPageInfo();
        }

        public void AddItem(ExtendTableItem title_item)
        {
            _items.Add(title_item);
            _scrollview.AddItem1(title_item);
        }

        #endregion

        #region private

        public void _on_update_page(int page, int interval)
        {
            if (OnUpdateItem != null)
                OnUpdateItem(page, interval, _items);
        }

        #endregion

        #region draw

        #endregion
    }
}