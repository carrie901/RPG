using UnityEngine;
using System.Collections.Generic;
using System;
//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:25:33
// FileName : ReCoroutineTaskManager.cs
//=============================================================================

namespace Summer
{
    public class ReCoroutineTaskManager : MonoBehaviour
    {
        public static ReCoroutineTaskManager Instance;
        public static Dictionary<string, ReCoroutineTask> _taskList = new Dictionary<string, ReCoroutineTask>();

        #region MONO

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                LogManager.Error("ReCoroutineTaskManager Instance Error");
            _taskList = new Dictionary<string, ReCoroutineTask>();
            //GameObject.DontDestroyOnLoad(gameObject);
            //gameObject.hideFlags |= HideFlags.HideAndDontSave;
        }

        #endregion

        #region Add Task

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public ReCoroutineTask AddTask(string taskName, IEnumerator<float> ienumer,
            Action<bool> callBack = null, object bindObject = null, bool autoStart = true)
        {
            if (_taskList.ContainsKey(taskName))
            {
                //Debug.logger.LogError("添加新任务", "任务重名！" + taskName);
                Restart(taskName);
                return _taskList[taskName];
            }
            else
            {
                ReCoroutineTask task = new ReCoroutineTask(taskName, ienumer, callBack, bindObject, autoStart);
                _taskList.Add(taskName, task);
                return task;
            }
        }

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public ReCoroutineTask AddTask(IEnumerator<float> ienumer,
            Action<bool> callBack = null, object bindObject = null, bool autoStart = true)
        {
            ReCoroutineTask task = new ReCoroutineTask(ienumer, callBack, bindObject, autoStart);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public ReCoroutineTask AddTask(ReCoroutineTask task)
        {
            if (_taskList.ContainsKey(task.Name))
            {
                //Debug.logger.LogError("添加新任务", "任务重名！" + task.name);
                Restart(task.Name);
            }
            else
            {
                _taskList.Add(task.Name, task);
            }
            return task;
        }

        #endregion

        #region  Play/Pause/UnPause/Stop/StopAll

        /// <summary>
        /// 开始一个任务
        /// </summary>
        public void DoTask(string taskName)
        {
            if (!_taskList.ContainsKey(taskName))
            {
                LogManager.Error("开始任务", "不存在该任务" + taskName);
                return;
            }
            _taskList[taskName].Start();
        }

        /// <summary>
        /// 暂停协程
        /// </summary>
        public void Pause(string taskName)
        {
            if (!_taskList.ContainsKey(taskName))
            {
                LogManager.Error("暂停任务", "不存在该任务" + taskName);
                return;
            }
            _taskList[taskName].Pause();

        }

        /// <summary>
        /// 取消暂停某个协程
        /// </summary>
        public void Unpause(string taskName)
        {
            if (!_taskList.ContainsKey(taskName))
            {
                LogManager.Error("重新开始任务", "不存在该任务" + taskName);
                return;
            }
            _taskList[taskName].UnPause();
        }

        /// <summary>
        /// 停止特定协程
        /// </summary>
        public void Stop(string taskName)
        {
            if (!_taskList.ContainsKey(taskName))
            {
                LogManager.Error("停止任务", "不存在该任务" + taskName);
                return;
            }
            _taskList[taskName].Stop();
        }

        public void Restart(string taskName)
        {
            if (!_taskList.ContainsKey(taskName))
            {
                LogManager.Error("重新开始任务", "不存在该任务" + taskName);
                return;
            }
            ReCoroutineTask task = _taskList[taskName];
            Stop(taskName);
            AddTask(task);
        }

        /// <summary>
        /// 停止所有协程
        /// </summary>
        public void StopAll()
        {
            List<ReCoroutineTask> tampList = new List<ReCoroutineTask>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (ReCoroutineTask task in _taskList.Values)
            {
                tampList.Add(task);
            }

            int length = tampList.Count;
            for (int i = 0; i < length; i++)
            {
                tampList[i].Stop();
            }
        }

        #endregion

        #region Wait 

        /// <summary>
        /// 等待一段时间再执行时间
        /// </summary>
        public ReCoroutineTask WaitSecondTodo(Action callBack, float time, object bindObject = null)
        {
            // ReSharper disable once RedundantLambdaSignatureParentheses
            Action<bool> callBack2 = (bo) =>
            {
                if (bo)
                    callBack();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitTodo(time),
                callBack2, bindObject);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等待一段时间再执行时间
        /// </summary>
        public ReCoroutineTask WaitSecondTodo(Action<bool> callBack, float time, object bindObject = null)
        {
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitTodo(time),
                callBack, bindObject);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等到下一帧
        /// </summary>
        public ReCoroutineTask WaitFrameEnd(Action callBack, object bindObject = null)
        {
            // ReSharper disable once RedundantLambdaSignatureParentheses
            Action<bool> callBack2 = (bo) =>
            {
                if (bo)
                    callBack();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitFrameEndTodo(),
                callBack2, bindObject);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等待直到某个条件成立时
        /// </summary>
        public ReCoroutineTask WaitUntilTodo(Action callBack, Func<bool> predicates = null,
            object bindObject = null)
        {
            Action<bool> callBack2 = (bo) =>
            {
                if (bo)
                    callBack();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitUntil(predicates), callBack2, bindObject);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 当条件成立时等待
        /// </summary>
        public ReCoroutineTask WaitWhileTodo(Action callBack, Func<bool> predicates,
            object bindObject = null)
        {
            Action<bool> callBack2 = (bo) =>
            {
                if (bo)
                    callBack();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitWhile(predicates), callBack2, bindObject);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等待所有其他携程任务完成
        /// </summary>
        public ReCoroutineTask WaitForAllCoroutine(Action callBack, ReCoroutineTask[] tasks,
    object bindObject = null)
        {
            // ReSharper disable once RedundantLambdaSignatureParentheses
            Action<bool> callBack2 = (bo) =>
            {
                if (bo)
                    callBack();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitForAllCoroutine(tasks), callBack2, bindObject);
            AddTask(task);
            return task;
        }


        #endregion

        #region Loop

        /// <summary>
        /// 间隔时间进行多次动作
        /// </summary>
        public ReCoroutineTask LoopTodoByTime(Action callBack, float interval,
            int loopTime, object bindObject = null, float startTime = 0)
        {

            ReCoroutineTask task = new ReCoroutineTask(
                DoLoopByTime(interval, loopTime, callBack, startTime), null, bindObject);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 每帧进行循环
        /// </summary>
        public ReCoroutineTask LoopByEveryFrame(Action callBack, int loopTime = -1
            , object bindObject = null, float startTime = 0)
        {
            ReCoroutineTask task = new ReCoroutineTask(
                DoLoopByEveryFrame(loopTime, callBack, startTime), null, bindObject);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 当满足条件循环动作
        /// </summary>
        public ReCoroutineTask LoopTodoByWhile(Action callBack, float interval, Func<bool> predicates,
            object bindObject = null, float startTime = 0)
        {
            ReCoroutineTask task = new ReCoroutineTask(
                DoLoopByWhile(interval, predicates, callBack, startTime), null, bindObject);
            AddTask(task);
            return task;
        }
        #endregion

        #region DoLoop IEnumerator

        private IEnumerator<float> DoLoopByTime(float interval, int loopTime,
            Action callBack, float startTime)
        {
            yield return startTime;
            if (loopTime <= 0)
            {
                loopTime = int.MaxValue;
            }
            int loopNum = 0;
            while (loopNum < loopTime)
            {
                loopNum++;
                callBack();
                yield return interval;
            }
        }

        private IEnumerator<float> DoLoopByEveryFrame(int loopTime,
            Action callBack, float startTime)
        {
            yield return startTime;
            if (loopTime <= 0)
            {
                loopTime = int.MaxValue;
            }
            int loopNum = 0;
            while (loopNum < loopTime)
            {
                loopNum++;
                callBack();
                yield return Time.deltaTime;
            }
        }

        private IEnumerator<float> DoLoopByWhile(float interval,
            Func<bool> predicates, Action callBack, float startTime)
        {
            yield return startTime;

            // ReSharper disable once NotAccessedVariable
            int loopNum = 0;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (predicates())
            {
                loopNum++;
                callBack();
                yield return interval;
            }
        }

        #endregion

        #region DoWait

        private IEnumerator<float> DoWaitWhile(Func<bool> predicates)
        {
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (predicates())
            {
                yield return 0;
            }
        }

        private IEnumerator<float> DoWaitForAllCoroutine(params ReCoroutineTask[] coroutines)
        {
            // ReSharper disable once TooWideLocalVariableScope
            bool allFinished;
            // ReSharper disable once TooWideLocalVariableScope
            int length;
            while (true)
            {
                allFinished = true;
                length = coroutines.Length;
                for (int i = 0; i < length; i++)
                {
                    if (!coroutines[i].IsFinished)
                    {
                        allFinished = false;
                        break;
                    }
                }
                if (allFinished)
                    break;
                yield return 0;
            }
        }

        private IEnumerator<float> DoWaitUntil(Func<bool> predicate)
        {
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (!predicate())
            {
                yield return 0;
            }
        }

        private IEnumerator<float> DoWaitTodo(float time)
        {
            yield return time;
        }

        private IEnumerator<float> DoWaitFrameEndTodo()
        {
            yield return Time.deltaTime;
        }

        #endregion
    }
}