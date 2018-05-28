using System.Collections;
//using Object = UnityEngine.Object;

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
        public string RequestName { get; protected set; }       // 请求命令的名字
        public string Error { get; private set; }               // 错误信息
        protected bool _is_complete_request;                    // 结束请求
        protected bool _is_start_init;                          // 开始初始化

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

        #region abstract

        /// <summary>
        /// 开始加载请求
        /// </summary>
        public virtual void OnInit()
        {
            Error = string.Empty;
            _is_complete_request = false;
            _is_start_init = true;
            OnUpdate();
        }

        public bool OnUpdate()
        {
            if (!_is_start_init) return false;
            return Update();
        }

        protected abstract bool Update();

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public abstract bool IsDone();

        /// <summary>
        /// 请求结束
        /// </summary>
        /// <returns></returns>
        public virtual void OnCompleteRequest(string msg)
        {
            _is_complete_request = true;
            Error = msg;
        }

        /// <summary>
        /// 得到资源
        /// </summary>
        public abstract UnityEngine.Object GetAsset();

        /// <summary>
        /// 卸载
        /// </summary>
        public abstract void UnloadRequest();

        #endregion
    }
}

