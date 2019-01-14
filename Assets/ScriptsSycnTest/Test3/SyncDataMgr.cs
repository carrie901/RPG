
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
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 1.每一个帧回合提交一次
    /// 2.
    /// </summary>
    public class SyncDataMgr
    {

        #region 属性

        private List<FrameData> _datas = new List<FrameData>();                     // 当前的接收到的数据
        private TrunData _trunData = new TrunData(1);                               // 当前提交的数据

        #endregion

        #region Public


        public void AddCommand(FrameData data)
        {

        }

        #endregion

        #region Private Methods



        #endregion
    }

}

