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
        public int _iid;
        public static int _index;
        public string Error { get; protected set; }                               // 错误信息


        public LoadOpertion()
        {
            _iid = _index;
            _index++;
        }

        #region IEnumerator

        public bool MoveNext()
        {
            bool result = !IsDone();
            return result;
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

        public virtual void OnUpdate()
        {

        }

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public virtual bool IsDone() { return false; }

        #endregion
    }

    public enum E_LoadOpertion
    {
        INIT = 0,
        LOADING = 1,
        COMPLETE = 2,
        EXIT = 3,
    }
}

