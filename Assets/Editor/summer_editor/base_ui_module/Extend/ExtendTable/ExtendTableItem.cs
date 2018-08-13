
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

namespace SummerEditor
{
    public class ExtendTableItem : EComponent
    {
        public List<ELabel> _labs = new List<ELabel>();
        public int _max_size;
        public float[] _titles_width;
        public ExtendTableItem(int max_size, float[] titles_width, float width, float height) : base(width, height)
        {
            _max_size = max_size;
            _titles_width = titles_width;
            _init();
        }

        #region public 

        public virtual void SetData(System.Object data)
        {
            enable = (data != null);
        }

        public void SetContent(string[] titles)
        {
            int title_length = titles.Length;
            for (int i = 0; i < _max_size; i++)
            {
                _labs[i].text = (i < title_length) ? titles[i] : "";
            }
        }

        #endregion

        #region private

        public void _init()
        {
            ERect first = null;
            for (int i = 0; i < _max_size; i++)
            {
                ELabel lab = new ELabel("", _titles_width[i]);
                _labs.Add(lab);
                if (first == null)
                {
                    AddComponent(lab, 2, 5);
                }
                else
                {
                    AddComponentRight(lab, first, 0);
                }

                first = lab;
            }
        }

        #endregion
    }
}