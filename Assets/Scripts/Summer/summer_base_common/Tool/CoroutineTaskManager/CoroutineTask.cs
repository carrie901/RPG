using System;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 20:3:34
// FileName : CoroutineTask.cs
//=============================================================================

namespace Summer.Tool
{
    /// <summary>
    /// 协同任务
    /// </summary>
    public class CoroutineTask
    {
        #region Property

        public delegate void FinishedHandler(bool manual);
        public event FinishedHandler Finished;

        protected static long task_id = 1;
        protected IEnumerator _ienumer;
        protected Action<bool> _call_back;

        public string Name { get; private set; }                // 名称
        public object BindObject { get; private set; }          // 绑定物体
        public bool Running { get; private set; }               // 运行
        public bool Paused { get; private set; }                // 是否暂停

        #endregion

        #region construction

        public CoroutineTask(IEnumerator ienumer, Action<bool> call_back = null,
            object bind_object = null, bool auto_start = true)
        {
            task_id += 1;
            Name = _ienumer.GetHashCode().ToString();
            _ienumer = ienumer;
            _call_back = call_back;

            if (bind_object == null)
            {
                BindObject = CoroutineTaskManager.Instance.gameObject;
            }
            else
            {
                BindObject = bind_object;
            }

            Running = false;
            Paused = false;

            if (auto_start)
                Start();
        }

        public CoroutineTask(
            string name, IEnumerator ienumer, Action<bool> call_back = null,
            object bind_object = null, bool auto_start = true)
            : this(ienumer, call_back, bind_object, auto_start)
        {
            Name = name;
        }

        #endregion

        #region Start/Pause/UnPause/Stop

        public void Start()
        {
            Running = true;
            CoroutineTaskManager.Instance.StartCoroutine(_do_task());
        }

        public void Pause()
        {
            Paused = true;
        }

        public void UnPause()
        {
            Paused = false;
        }

        // 提交结束/停止任务
        public void Stop()
        {
            Running = false;
            _internal_call_back(false);
        }

        public IEnumerator _do_task()
        {
            IEnumerator e = _ienumer;
            while (Running)
            {
                // 安全性检测 性能GameObject待定
                if (BindObject.Equals(null))
                {
                    LogManager.Error("协程中断,因为绑定物体被删除所以停止协程");
                    Stop();
                    yield break;
                }

                // 暂停
                if (Paused)
                {
                    yield return null;
                }
                else
                {
                    // 运行
                    if (e != null && e.MoveNext())
                    {
                        yield return e.Current;
                    }
                    else
                    {
                        Running = false;
                    }
                }
            }

            _internal_call_back(true);
        }

        public void _internal_call_back(bool value)
        {
            CoroutineTaskManager.task_list.Remove(Name);
            if (_call_back != null)
                _call_back(value);
        }

        #endregion

        protected virtual void OnFinished(bool manual)
        {
            var handler = Finished;
            if (handler != null) handler(manual);
        }
    }
}
