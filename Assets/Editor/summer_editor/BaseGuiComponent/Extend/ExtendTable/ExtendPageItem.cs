
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

namespace SummerEditor
{
    public class ExtendPageItem : EComponent
    {

        #region 属性

        public delegate void UpdatePage(int page, int interval);
        public event UpdatePage OnUpdatePage;

        public EButton _left_btn;                                                       // 上一页
        public EButton _rigth_btn;                                                      // 下一页
        public ELabel _page_lab;                                                        // 页码

        public int _current_page;
        public int _interval_count;
        public int _max_page;

        #endregion

        #region Override

        public ExtendPageItem(float width = 90, float height = DEFAULT_HEIGHT) : base(width, height)
        {
            _init();
            _init_info();
        }

        #endregion

        #region Public

        public void SetPageInfo(int max_page, int interval_count, int default_page = 1)
        {
            _max_page = max_page;
            _current_page = default_page;
            _interval_count = interval_count;
            _change_page_lab();

        }

        public void ResetPageInfo()
        {
            _change_page_lab();
        }

        #endregion

        #region 按钮响应

        public void OnLeftPage(EButton button)
        {
            if (_current_page <= 1) return;
            _current_page--;
            _change_page_lab();
        }

        public void OnRightPage(EButton button)
        {
            if (_current_page >= _max_page) return;
            _current_page++;
            _change_page_lab();
        }

        #endregion

        #region Private Methods

        public void _init()
        {
            _left_btn = new EButton("<", 20);
            _rigth_btn = new EButton(">", 20);
            _page_lab = new ELabel("0/0", 40);
        }

        public void _init_info()
        {
            AddComponent(_left_btn, 0, 0);
            AddComponentRight(_page_lab, _left_btn);
            AddComponentRight(_rigth_btn, _page_lab);

            _left_btn.OnClick += OnLeftPage;
            _rigth_btn.OnClick += OnRightPage;
        }

        public void _change_page_lab()
        {
            _page_lab.text = _current_page + "/" + _max_page;
            if (OnUpdatePage != null)
                OnUpdatePage(_current_page, _interval_count);
        }

        #endregion
    }

}


