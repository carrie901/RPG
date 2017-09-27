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
        /// <summary>
        /// 单例
        /// </summary>
        private static StartCoroutineManager m_instance = null;


        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="ienumerator"></param>
        public static Coroutine Start(IEnumerator ienumerator)
        {
            if (m_instance == null)
            {
                GameObject obj = new GameObject("StartCoroutine");
                m_instance = obj.AddComponent<StartCoroutineManager>();
            }
            return m_instance.StartCoroutineSoon(ienumerator);
        }
        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="ienumerator"></param>
        public static void Stop(IEnumerator ienumerator)
        {
            if (m_instance == null)
            {
                GameObject obj = new GameObject("StartCoroutine");
                m_instance = obj.AddComponent<StartCoroutineManager>();
            }
            m_instance.StopCoroutineSoon(ienumerator);
        }

        public static void StopAll()
        {
            if (m_instance == null)
            {
                GameObject obj = new GameObject("StartCoroutine");
                m_instance = obj.AddComponent<StartCoroutineManager>();
            }
            m_instance.StopAllCorotineSoon();
        }

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
    }
}
