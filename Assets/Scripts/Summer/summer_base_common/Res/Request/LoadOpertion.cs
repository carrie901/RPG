using System.Collections;

namespace Summer
{
    /// <summary>
    ///  加载操作
    /// TODO
    ///     1.缺少错误信息
    ///     2.缺少超时机制
    /// </summary>
    public abstract class LoadOpertion : IEnumerator
    {
        #region 属性

        public string RequestResPath { get; protected set; }                    // 请求命令的名字
        public string Error { get; private set; }                               // 错误信息
        protected E_LoadOpertion _loading = E_LoadOpertion.init;
        protected AssetInfo _asset_info;

        #endregion

        #region IEnumerator

        public bool MoveNext()
        {
            return !IsDone();
        }

        public void Reset()
        {

        }

        public object Current { get { return null; } }

        #endregion

        #region public 

        /// <summary>
        /// 生命周期
        /// </summary>
        public void OnUpdate()
        {
            if (_loading == E_LoadOpertion.init)
            {
                Init();
                _loading = E_LoadOpertion.loading;
            }

            if (_loading == E_LoadOpertion.loading)
            {
                bool result = Update();
                if (result)
                    FinishLoading();
            }

            if (_loading == E_LoadOpertion.complete)
            {
                Complete();
                _loading = E_LoadOpertion.exit;
            }
        }

        public bool IsExit()
        {
            return _loading == E_LoadOpertion.exit;
        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public bool IsDone() { return IsExit(); }

        public void FinishLoading() { _loading = E_LoadOpertion.complete; }

        public void ForceExit(string error_msg)
        {
            _loading = E_LoadOpertion.exit;
            Error = error_msg;
        }

        /// <summary>
        /// 得到资源
        /// </summary>
        public AssetInfo GetAsset() { return _asset_info; }

        #endregion

        #region abstract

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

        #endregion

        /// <summary>
        /// 卸载加载请求
        /// </summary>
        public virtual void UnloadRequest()
        {
            _asset_info = null;
        }

        #endregion

    }

    public enum E_LoadOpertion
    {
        init,
        loading,
        complete,
        exit,
    }
}

