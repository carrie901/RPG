
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
    public class PoolPanelCache<TKey, TValue> : PoolCache<TKey, TValue> where TValue : I_PoolCacheRef
    {
        public Dictionary<TKey, int> _ignoreKey = new Dictionary<TKey, int>();

        public PoolPanelCache(int size) : base(size) { }


        public override void Set(TKey key, TValue value)
        {
            if (_ignoreKey.ContainsKey(key)) return;
            base.Set(key, value);
        }

        public void AddIgnoreKey(TKey key)
        {
            _ignoreKey.Add(key, 1);
        }
    }
}
