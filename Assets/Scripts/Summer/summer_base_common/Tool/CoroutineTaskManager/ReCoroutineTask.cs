using System;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 18:20:34
// FileName : ReCoroutineTask.cs
//=============================================================================

namespace Summer
{
    public class ReCoroutineTask
    {
        #region Property

        protected static long _taskId = 1;
        protected IEnumerator<float> _ienumer;                                          // 内部迭代器
        protected Action<bool> _callBack;                                               // 回调函数

        public string Name { get; private set; }
        public object BindObject { get; private set; }
        public bool Running { get; private set; }
        public bool IsFinished { get; private set; }
        public bool Paused { get; private set; }

        #endregion

        #region construction

        public ReCoroutineTask(
            IEnumerator<float> ienumer, Action<bool> callBack = null,
            object bindObject = null, bool autoStart = true)
        {

            Name = ienumer.GetHashCode().ToString();
            _taskId += 1;
            _ienumer = ienumer;
            _callBack = callBack;
            if (bindObject == null)
            {
                BindObject = ReCoroutineTaskManager.Instance.gameObject;
            }
            else
            {
                BindObject = bindObject;
            }

            Running = false;
            Paused = false;
            IsFinished = false;

            if (autoStart)
            {
                Start();
            }

        }

        public ReCoroutineTask(
            string name, IEnumerator<float> ienumer, Action<bool> callBack = null,
            object bindObject = null, bool autoStart = true)
                : this(ienumer, callBack, bindObject, autoStart)
        {
            Name = name;
        }

        #endregion

        #region Start/Pause/UnPause/Stop

        public void Start()
        {
            Running = true;
            IsFinished = false;
            ReCoroutineManager.AddCoroutine(_do_task());
        }

        public void Pause()
        {
            Paused = true;
        }

        public void UnPause()
        {
            Paused = false;
        }

        public void Stop()
        {
            Running = false;
            _internal_call_back(false);
        }

        #endregion

        private IEnumerator<float> _do_task()
        {
            IEnumerator<float> e = _ienumer;
            while (Running)
            {
                if (BindObject.Equals(null))
                {
                    LogManager.Error("协程中断,因为绑定物体被删除所以停止协程");
                    Stop();
                    yield break;
                }

                if (Paused)
                {
                    yield return 0;
                }
                else
                {
                    if (e != null && e.MoveNext())
                    {
                        yield return e.Current;
                    }
                    else
                    {
                        Running = false;
                        IsFinished = true;
                        _internal_call_back(true);
                    }
                }
            }
        }

        public void _internal_call_back(bool value)
        {
            ReCoroutineTaskManager._taskList.Remove(Name);
            if (_callBack != null)
                _callBack(value);
        }
    }
}
