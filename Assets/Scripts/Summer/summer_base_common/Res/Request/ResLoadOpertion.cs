
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

using Object = UnityEngine.Object;

namespace Summer
{
    /// <summary>
    /// 复杂化的有状态机制的LoadOperation
    /// TODO BUG 需要简化这块模式
    ///     1.缺少错误信息
    ///     2.缺少超时机制
    /// </summary>
    public abstract class ResLoadOpertion : LoadOpertion
    {
        #region 属性

        public string RequestResPath { get; protected set; }                    // 请求命令的名字
        protected E_LoadOpertion _loading = E_LoadOpertion.INIT;

        #endregion

        #region public 

        public override void OnUpdate()
        {
            if (_loading == E_LoadOpertion.INIT)
            {
                Init();
                _loading = E_LoadOpertion.LOADING;
            }

            if (_loading == E_LoadOpertion.LOADING)
            {
                bool result = Update();
                if (result)
                {
                    FinishComplete();
                }
            }

            if (_loading == E_LoadOpertion.COMPLETE)
            {
                Complete();
                _loading = E_LoadOpertion.EXIT;
            }
        }

        public override bool IsDone()
        {
            bool result = IsExit();
            return result;
        }

        public virtual void UnloadRequest()
        {

        }

        public bool IsExit() { return _loading == E_LoadOpertion.EXIT; }

        public void ForceExit(string errorMsg)
        {
            _loading = E_LoadOpertion.EXIT;
            Error = errorMsg;
        }

        public void FinishComplete() { _loading = E_LoadOpertion.COMPLETE; }

        #region 生命周期的方法
        /// <summary>
        /// 初始化
        /// </summary>
        protected abstract void Init();

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        protected abstract bool Update();

        /// <summary>
        /// 完成
        /// </summary>
        protected abstract void Complete();

        public abstract AssetInfo GetAsset<T>(string resPath) where T : Object;

        #endregion

        #endregion
    }
}

