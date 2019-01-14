
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
    public class FrameData
    {
        public int FrameIndex { get; private set; }

        public FrameData(int frame)
        {
            FrameIndex = frame;
        }
    }

    public class TrunData
    {
        public List<FrameData> Datas = new List<FrameData>();

        public int TrunIndex { get; private set; }
        public TrunData(int trun)
        {
            TrunIndex = trun;
        }

        public void ResetTrunIndex(int trun)
        {
            TrunIndex = trun;
        }

        public void NextTrun() { TrunIndex++; }

        public bool AddFrame(FrameData data)
        {
            if (data == null) return false;

            int trunIndex = data.FrameIndex / 5;
            bool inTrun = trunIndex == (TrunIndex + 1);

            LogManager.Assert(inTrun, "当前帧回合:[{0}],逻辑回合:[{1}],两个不匹配", TrunIndex, data.FrameIndex);
            if (!inTrun) return false;

            Datas.Add(data);
            return true;
        }

    }
}