
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
using Summer;
using UnityEngine;

public class SyncManager2
{

    #region 属性

    protected bool _start;
    protected int _logicFrameDelta;                         // 逻辑帧更新时间
    protected int _logicFrameAdd;                           // 累积时间
    #endregion

    #region Public



    #endregion

    #region Private Methods

    private void Update()
    {
        if (!_start) return;
        if (_logicFrameAdd < _logicFrameDelta)
        {
            _logicFrameAdd += (int)(Time.deltaTime * 1000);
        }
        else
        {
            int frameNum = 0;
            while (CanUpdateNextFrame() || IsFillFrame())
            {
                Loop();//主循环
                frameNum++;
                // 一次最多连续播放10帧
                if (frameNum > 10)
                    break;
            }
            _logicFrameAdd = 0;
        }
    }

    private void Loop()
    {

    }

    // 是否可以更新至下一关键帧
    private bool CanUpdateNextFrame()
    {
        return false;
    }

    //当前逻辑帧是否为填充帧
    private bool IsFillFrame()
    {
        return true;
    }
    #endregion
}
