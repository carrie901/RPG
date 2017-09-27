using System.Collections;
using Object = UnityEngine.Object;

namespace Summer
{
    /// <summary>
    ///  缺少错误信息
    /// </summary>
    public abstract class OloadOpertion : IEnumerator
    {
        public bool MoveNext()
        {
            return !IsDone();
        }

        public void Reset()
        {

        }

        public object Current { get { return null; } }

        public abstract bool Update();

        public abstract bool IsDone();

        public abstract Object GetAsset();

        public abstract void UnloadAssetBundle();
    }
}

