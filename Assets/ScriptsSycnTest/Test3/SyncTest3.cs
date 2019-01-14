
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
    public class SyncTest3 : MonoBehaviour
    {

        #region 属性

        protected bool _start;                                      // 开始
        protected bool _isInit;                                     // 初始化
        protected int _logicFrameIndex;                             // 逻辑帧下标
        protected int _maxRunAfterFrame = 10;                       // 最大追赶帧
        protected float _logicFrameDelta;                           // 逻辑帧更新时间
        protected float _logicFrameAdd;                             // 累积时间
        protected float _nextLogicFrame;                            // 下一帧时间

        #endregion

        #region MONO Override

        void Update()
        {
            if (!_start) return;
            OnInit();
            _logicFrameAdd += Time.deltaTime;

            int runAfter = 0;
            while (CanUpdateNextFrame())
            {
                // 运行与游戏相关的具体逻辑
                Loop();

                // 一次最多连续播放10帧
                runAfter++;
                if (runAfter >= _maxRunAfterFrame) break;
            }
        }

        #endregion

        #region Public

        public void OnStart()
        {
            _start = true;
            _isInit = false;
        }

        #endregion

        #region Private Methods

        private bool CanUpdateNextFrame()
        {
            if (_logicFrameAdd >= _nextLogicFrame)
                return true;
            return false;
        }

        private void Loop()
        {
            // 计算下一个逻辑帧应有的时间
            _logicFrameIndex++;
            _nextLogicFrame = _logicFrameIndex * _logicFrameDelta;
        }

        private void OnInit()
        {
            if (_isInit || !_start) return;
            _logicFrameDelta = 0.05f;
            _isInit = true;
            _logicFrameIndex = 1;
            _nextLogicFrame = _logicFrameIndex * _logicFrameDelta;
        }

        #endregion
    }

    public interface I_GameUpdate
    {
        void UpdateLogic();
        void UpdateRenderer();
    }
}