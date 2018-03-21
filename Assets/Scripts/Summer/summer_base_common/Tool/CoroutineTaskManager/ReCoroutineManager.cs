using UnityEngine;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:25:20
// FileName : ReCoroutineManager.cs
//=============================================================================

namespace Summer.Tool
{
    /// <summary>
    /// 管理ReCoroutine,对外提供添加ReCoroutine
    /// </summary>
    public class ReCoroutineManager : MonoBehaviour
    {

        #region Property

        public static ReCoroutineManager Instance;

        private readonly List<ReCoroutine> update_ienumerator_list = new List<ReCoroutine>();
        private readonly List<ReCoroutine> late_update_ienumerator_list = new List<ReCoroutine>();
        private readonly List<ReCoroutine> fixed_update_ienumerator_list = new List<ReCoroutine>();

        private static float update_delta_time;
        private static float late_update_delta_time;
        private static float fixed_update_delta_time;

        private readonly List<ReCoroutine> remove_ienumerator = new List<ReCoroutine>();

        #endregion

        #region MONO
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                LogManager.Error("ReCoroutineManager Instance Error");
            update_delta_time = Time.deltaTime;
            late_update_delta_time = Time.deltaTime;
            fixed_update_delta_time = Time.fixedDeltaTime;
        }

        void Update()
        {
            remove_ienumerator.Clear();

            int length = update_ienumerator_list.Count;
            for (int i = 0; i < length; i++)
            {
                var cor = update_ienumerator_list[i];

                cor.Update();

                if (cor.IsDone)
                {
                    remove_ienumerator.Add(cor);
                }
            }

            length = remove_ienumerator.Count;
            for (int i = 0; i < length; i++)
            {
                update_ienumerator_list.Remove(remove_ienumerator[i]);
            }
        }

        void LateUpdate()
        {
            remove_ienumerator.Clear();
            int length = late_update_ienumerator_list.Count;
            for (int i = 0; i < length; i++)
            {
                var cor = late_update_ienumerator_list[i];
                cor.LateUpdate();

                if (cor.IsDone)
                {
                    remove_ienumerator.Add(cor);
                    continue;
                }
            }
            length = remove_ienumerator.Count;
            for (int i = 0; i < length; i++)
            {
                late_update_ienumerator_list.Remove(remove_ienumerator[i]);
            }
        }

        void FixedUpdate()
        {
            remove_ienumerator.Clear();

            int length = fixed_update_ienumerator_list.Count;
            for (int i = 0; i < length; i++)
            {
                var cor = fixed_update_ienumerator_list[i];

                cor.FixedUpdate();

                if (cor.IsDone)
                {
                    remove_ienumerator.Add(cor);
                    continue;
                }

            }

            length = remove_ienumerator.Count;
            for (int i = 0; i < length; i++)
            {
                fixed_update_ienumerator_list.Remove(remove_ienumerator[i]);
            }
        }

        #endregion

        #region public 

        /// <summary>
        /// 添加新协程
        /// </summary>
        public static ReCoroutine AddCoroutine(IEnumerator<float> e, E_CoroutineType type = E_CoroutineType.update)
        {
            return Instance._internal_add_coroutine(e, type);
        }

        /// <summary>
        /// 间隔时间
        /// </summary>
        public static float GetDeltaTime(ReCoroutine coroutine)
        {
            switch (coroutine.ECoroutineType)
            {
                case E_CoroutineType.update:
                    return update_delta_time;
                case E_CoroutineType.late_update:
                    return late_update_delta_time;
                case E_CoroutineType.fixed_update:
                    return fixed_update_delta_time;
                default:
                    return 0;
            }
        }

        #endregion

        #region private 

        public ReCoroutine _internal_add_coroutine(IEnumerator<float> e, E_CoroutineType type = E_CoroutineType.update)
        {
            ReCoroutine cor = new ReCoroutine(e, type);

            if (type == E_CoroutineType.update)
                update_ienumerator_list.Add(cor);
            else if (type == E_CoroutineType.late_update)
                late_update_ienumerator_list.Add(cor);
            else if (type == E_CoroutineType.fixed_update)
                fixed_update_ienumerator_list.Add(cor);

            return cor;
        }

        #endregion
    }
}
