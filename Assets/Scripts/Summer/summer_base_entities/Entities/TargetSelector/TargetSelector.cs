
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
    /// <summary>
    /// 目标过滤
    /// </summary>
    public class TargetSelector : I_TargetSelector
    {
        public List<I_TargetSelector> _lists = new List<I_TargetSelector>(4);
        public void AddFilter(I_TargetSelector selector)
        {
            _lists.Add(selector);
        }

        public void FilterTarget(List<BaseEntity> targets)
        {
            int length = _lists.Count;
            for (int i = 0; i < length; i++)
            {
                _lists[i].FilterTarget(targets);
            }
        }
    }
}