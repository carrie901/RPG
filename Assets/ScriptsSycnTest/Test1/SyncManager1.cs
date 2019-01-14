
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

public class SyncManager1
{

    #region 属性

    protected float _accumilatedTime;
    protected float _nextGameTime;
    protected float _frameLen = 1f / 33;
    protected float _interpolation;
    public int GameLogicFrame;
    #endregion

    #region Public

    public void Update(float dt)
    {
        _accumilatedTime = _accumilatedTime + dt;
        while (_accumilatedTime > _nextGameTime)
        {
            //运行与游戏相关的具体逻辑
            //计算下一个逻辑帧应有的时间
            _nextGameTime += _frameLen;
            // 游戏逻辑帧自增
            GameLogicFrame++;
        }

        //计算两帧的时间差,用于运行补间动画
        _interpolation = (_accumilatedTime + _frameLen - _nextGameTime) / _frameLen;
    }

    #endregion

    #region Private Methods

    private void Loop()
    {
        
    }

    #endregion
}
