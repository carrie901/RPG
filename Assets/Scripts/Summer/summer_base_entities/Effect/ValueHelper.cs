
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

namespace Summer
{
    public class ValueHelper
    {
        #region Calc

        public const int PER = 100;

        /// <summary>
        /// 计算结果
        /// </summary>
        /// <param name="origin">原始数据</param>
        /// <param name="cur">结果</param>
        /// <param name="type">更新类型</param>
        /// <param name="param">针对百分比是100值</param>
        public static void Calc(float origin, ref float cur, E_DataUpdateType type, int param)
        {
            //这里有个bug，先把cur zero，然后再加乘一些original，则cur不为0，例如带着冰冻的buff，但是装备上加速度，角色还能跑。
            //这是因为缺乏优先级运算的先后顺序导致的
            //目前先不考虑这些东西吧。等后续慢慢上来
            switch (type)
            {
                case E_DataUpdateType.plus:
                    cur += param;
                    break;
                case E_DataUpdateType.multiply_plus:
                    cur += (origin * param) / PER;
                    break;
                case E_DataUpdateType.zero:
                    cur = 0;
                    break;
                default:
                    LogManager.Error("Value Data update E_DataUpdateType Error. type:{0}", type);
                    break;
            }
        }

        public static void Calc(AttributeIntParam info, E_DataUpdateType type, int value)
        {
            switch (type)
            {
                case E_DataUpdateType.plus:
                        info.SetPlus(value);
                    break;
                case E_DataUpdateType.multiply_plus:
                        info.SetMultiplyPlus(value);
                    break;

                case E_DataUpdateType.zero:
                        //cur = 0;
                        LogManager.Error("原来还没有做呀");
                    break;
                default:
                    LogManager.Error("Buff Data update E_DataUpdateType Error. type:{0}", type);
                    break;
            }
        }

        #endregion
    }

}
