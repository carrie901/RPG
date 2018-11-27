
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

namespace Summer
{
    public class TargetManager : TSingleton<TargetManager>, I_FindTarget
    {
        public static Dictionary<E_FindTarget, I_FindTarget> _map = new Dictionary<E_FindTarget, I_FindTarget>()
        {
            {E_FindTarget.MAX,new RadiusFindTarget() }
        };
        public BaseFindTarget _findTarget = new BaseFindTarget();

        protected override void OnInit()
        {

        }

        public void FindTarget(List<BaseEntity> targets)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}

