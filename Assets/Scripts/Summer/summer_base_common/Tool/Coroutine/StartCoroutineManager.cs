using System.Collections;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 协程管理器
    /// </summary>
    /// Create by Mike Chai
    public class StartCoroutineManager : MonoBehaviour
    {
        #region 属性
        /// <summary>
        /// 单例
        /// </summary>
        private static StartCoroutineManager _instance = null;

        #endregion

        #region static public

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="ienumerator"></param>
        public static Coroutine Start(IEnumerator ienumerator)
        {
            Init();
            return _instance.StartCoroutineSoon(ienumerator);
        }
        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="ienumerator"></param>
        public static void Stop(IEnumerator ienumerator)
        {
            Init();
            _instance.StopCoroutineSoon(ienumerator);
        }

        public static void StopAll()
        {
            Init();
            _instance.StopAllCorotineSoon();
        }

        #endregion

        #region private

        private Coroutine StartCoroutineSoon(IEnumerator ienumerator)
        {
            return StartCoroutine(ienumerator);
        }

        private void StopCoroutineSoon(IEnumerator ienumerator)
        {
            StopCoroutine(ienumerator);
        }

        private void StopAllCorotineSoon()
        {
            StopAllCoroutines();
        }

        private static void Init()
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("StartCoroutine");
                _instance = obj.AddComponent<StartCoroutineManager>();
                DontDestroyOnLoad(obj);
            }
        }

        #endregion
    }
}
