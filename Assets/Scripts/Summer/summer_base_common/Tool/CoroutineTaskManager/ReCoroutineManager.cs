using UnityEngine;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:25:20
// FileName : ReCoroutineManager.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 管理ReCoroutine,对外提供添加ReCoroutine
    /// </summary>
    public class ReCoroutineManager : MonoBehaviour
    {

        #region Property

        public static ReCoroutineManager Instance;

        private readonly List<ReCoroutine> _updateIenumeratorList = new List<ReCoroutine>();
        private readonly List<ReCoroutine> _lateUpdateIenumeratorList = new List<ReCoroutine>();
        private readonly List<ReCoroutine> _fixedUpdateIenumeratorList = new List<ReCoroutine>();

        private static float UpdateDeltaTime;
        private static float LateUpdateDeltaTime;
        private static float FixedUpdateDeltaTime;

        private readonly List<ReCoroutine> _removeIenumerator = new List<ReCoroutine>();

        #endregion

        #region MONO
        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                LogManager.Error("ReCoroutineManager Instance Error");
            UpdateDeltaTime = Time.deltaTime;
            LateUpdateDeltaTime = Time.deltaTime;
            FixedUpdateDeltaTime = Time.fixedDeltaTime;
        }

        void Update()
        {
            _removeIenumerator.Clear();

            int length = _updateIenumeratorList.Count;
            for (int i = 0; i < length; i++)
            {
                var cor = _updateIenumeratorList[i];

                cor.Update();

                if (cor.IsDone)
                {
                    _removeIenumerator.Add(cor);
                }
            }

            length = _removeIenumerator.Count;
            for (int i = 0; i < length; i++)
            {
                _updateIenumeratorList.Remove(_removeIenumerator[i]);
            }
        }

        void LateUpdate()
        {
            _removeIenumerator.Clear();
            int length = _lateUpdateIenumeratorList.Count;
            for (int i = 0; i < length; i++)
            {
                var cor = _lateUpdateIenumeratorList[i];
                cor.LateUpdate();

                if (cor.IsDone)
                {
                    _removeIenumerator.Add(cor);
                    continue;
                }
            }
            length = _removeIenumerator.Count;
            for (int i = 0; i < length; i++)
            {
                _lateUpdateIenumeratorList.Remove(_removeIenumerator[i]);
            }
        }

        void FixedUpdate()
        {
            _removeIenumerator.Clear();

            int length = _fixedUpdateIenumeratorList.Count;
            for (int i = 0; i < length; i++)
            {
                var cor = _fixedUpdateIenumeratorList[i];

                cor.FixedUpdate();

                if (cor.IsDone)
                {
                    _removeIenumerator.Add(cor);
                    continue;
                }

            }

            length = _removeIenumerator.Count;
            for (int i = 0; i < length; i++)
            {
                _fixedUpdateIenumeratorList.Remove(_removeIenumerator[i]);
            }
        }

        #endregion

        #region public 

        /// <summary>
        /// 添加新协程
        /// </summary>
        public static ReCoroutine AddCoroutine(IEnumerator<float> e, E_CoroutineType type = E_CoroutineType.Update)
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
                case E_CoroutineType.Update:
                    return UpdateDeltaTime;
                case E_CoroutineType.LateUpdate:
                    return LateUpdateDeltaTime;
                case E_CoroutineType.FixedUpdate:
                    return FixedUpdateDeltaTime;
                default:
                    return 0;
            }
        }

        #endregion

        #region private 

        public ReCoroutine _internal_add_coroutine(IEnumerator<float> e, E_CoroutineType type = E_CoroutineType.Update)
        {
            ReCoroutine cor = new ReCoroutine(e, type);

            if (type == E_CoroutineType.Update)
                _updateIenumeratorList.Add(cor);
            else if (type == E_CoroutineType.LateUpdate)
                _lateUpdateIenumeratorList.Add(cor);
            else if (type == E_CoroutineType.FixedUpdate)
                _fixedUpdateIenumeratorList.Add(cor);

            return cor;
        }

        #endregion
    }
}
