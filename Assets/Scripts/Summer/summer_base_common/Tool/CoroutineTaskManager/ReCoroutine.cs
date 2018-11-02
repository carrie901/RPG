using System;
using UnityEngine;
using System.Collections.Generic;
using System.Threading;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:25:14
// FileName : ReCoroutine.cs
//=============================================================================

namespace Summer
{
    public enum E_CoroutineType
    {
        Update,
        FixedUpdate,
        LateUpdate
    }

    /// <summary>
    /// 协同对象，包含了基本的协同标识符/迭代器/回调行为/以及基础的暂停，开始，取消等行为
    /// </summary>
    public class ReCoroutine
    {
        #region Property

        public long Id { get; private set; }                                        // 协程标识符Id
        public E_CoroutineType ECoroutineType { get; private set; }                 // 迭代类型
        public IEnumerator<float> Ie { get; private set; }                          // 迭代器
        public float CurrentTime { get; private set; }                              // 距离协程开始的当前时间
        public float UntilTime { get; private set; }                                // 等待到该时间并继续执行
        public bool IsDone { get; private set; }                                    // 是否已经完成
        private ReCoroutine WaitingCoroutine { get; set; }                          // 正在等待的别的迭代器

        private static readonly object lock_object = new object();                  // 加锁对象
        private static long _baseId;

        #endregion

        public ReCoroutine(IEnumerator<float> ie, E_CoroutineType type)
        {
            Ie = ie;
            ECoroutineType = type;
            CurrentTime = 0;
            UntilTime = 0;
            IsDone = false;
            Id = _baseId++;
        }

        #region Update/LateUpdate/FixedUpdate

        public void Update()
        {
            if (ECoroutineType == E_CoroutineType.Update)
            {
                CommonUpdate();
            }
        }

        public void LateUpdate()
        {
            if (ECoroutineType == E_CoroutineType.LateUpdate)
            {
                CommonUpdate();
            }
        }

        public void FixedUpdate()
        {
            if (ECoroutineType == E_CoroutineType.FixedUpdate)
            {
                CommonUpdate();
            }
        }

        private void CommonUpdate()
        {
            CurrentTime += ReCoroutineManager.GetDeltaTime(this);

            // 1.是否处于等待
            if (!IsWaiting())
            {
                // 2.判断是否结束
                if (!Ie.MoveNext())
                {
                    IsDone = true;
                }
                // 3.更新等待时间
                _update_wait_time(Ie.Current);

                if (Ie.Current.Equals(float.NaN))
                {
                    WaitingCoroutine = ReplaceCoroutine;
                }
            }
            else
            {
                if (WaitingCoroutine != null && WaitingCoroutine.IsDone)
                {
                    WaitingCoroutine = null;
                }
            }
        }


        #endregion

        #region Public

        /// <summary>
        /// 是否正在等待
        /// </summary>
        public bool IsWaiting()
        {
            if (UntilTime > CurrentTime)
                return true;
            return WaitingCoroutine != null;
        }

        /// <summary>
        /// 等待了多少时间
        /// </summary>
        public void _update_wait_time(float wait_time)
        {
            if (float.IsNaN(wait_time)) wait_time = 0;
            UntilTime = CurrentTime + wait_time;
        }

        #region static Wait

        /// <summary>
        /// 等待www返回
        /// </summary>
        public static float WaitWww(WWW www)
        {
            lock (lock_object)
            {
                //  E_CoroutineType.update
                ReplaceCoroutine = ReCoroutineManager.AddCoroutine(GetReplaceCoroutine(() => www.isDone));
            }
            return float.NaN;
        }

        /// <summary>
        /// 等待异步操作
        /// </summary>
        public static float WaitAsynOperation(AsyncOperation operation)
        {
            lock (lock_object)
            {
                // E_CoroutineType.update
                ReplaceCoroutine = ReCoroutineManager.AddCoroutine(GetReplaceCoroutine(() => operation.isDone));
            }
            return float.NaN;
        }

        /// <summary>
        /// 等待其他协程
        /// </summary>
        public static float Wait(ReCoroutine coroutine)
        {
            lock (lock_object)
                ReplaceCoroutine = coroutine;
            return float.NaN;
        }

        /// <summary>
        /// 等待其他协程
        /// </summary>
        public static float Wait(IEnumerator<float> e)
        {
            lock (lock_object)
                ReplaceCoroutine = ReCoroutineManager.AddCoroutine(e);
            return float.NaN;
        }

        /// <summary>
        /// 等待所有其他携程
        /// </summary>
        public static float WaitForAllCoroutines(params ReCoroutine[] coroutines)
        {
            // E_CoroutineType.update
            lock (lock_object)
                ReplaceCoroutine = ReCoroutineManager.AddCoroutine(
                GetReplaceCoroutine(() =>
                {
                    int length = coroutines.Length;
                    for (int i = 0; i < length; i++)
                    {
                        if (!coroutines[i].IsDone)
                            return false;
                    }
                    return true;
                }));
            return float.NaN;
        }

        /// <summary>
        /// 等待知道
        /// </summary>
        public static float WaitUntil(Func<bool> condition)
        {
            // E_CoroutineType.update
            lock (lock_object)
                ReplaceCoroutine = ReCoroutineManager.AddCoroutine(GetReplaceCoroutine(condition));
            return float.NaN;
        }

        #endregion


        #endregion

        #region static 

        public static ReCoroutine ReplaceCoroutine { get; set; }                                // 替代用的Coroutine

        public static IEnumerator<float> GetReplaceCoroutine(Func<bool> func)
        {
            bool resulst = !func();
            while (resulst)
            {
                resulst = !func();
                yield return 0;
            }
        }

        #endregion
    }

}
