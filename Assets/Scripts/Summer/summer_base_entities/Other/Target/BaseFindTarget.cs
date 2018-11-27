
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

namespace Summer
{
    public class BaseFindTarget : I_FindTarget
    {
        public List<I_FindTarget> _filters = new List<I_FindTarget>(8);

        public void AddFilter(I_FindTarget fiter)
        {
            _filters.Add(fiter);
        }

        public void FindTarget(List<BaseEntity> targets)
        {
            int length = _filters.Count;
            for (int i = 0; i < length; i++)
            {
                _filters[i].FindTarget(targets);
            }
            Clear();
        }

        public void Clear()
        {
            int length = _filters.Count;
            for (int i = 0; i < length; i++)
            {
                _filters[i].Clear();
            }
        }
    }
}

