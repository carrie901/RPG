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
    public abstract class OloadOpertion : IEnumerator
    {
        public string error;                // 错误信息
        public float process;               // 进程

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

        public abstract bool Update();

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public abstract bool IsDone();

        /// <summary>
        /// 得到资源
        /// </summary>
        public abstract UnityEngine.Object GetAsset();

        /// <summary>
        /// 卸载
        /// </summary>
        public abstract void UnloadAssetBundle();

        #endregion
    }
}

