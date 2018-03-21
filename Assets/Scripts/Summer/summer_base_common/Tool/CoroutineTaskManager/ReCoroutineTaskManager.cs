using UnityEngine;
using System.Collections.Generic;
using System;
//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:25:33
// FileName : ReCoroutineTaskManager.cs
//=============================================================================

namespace Summer.Tool
{
    public class ReCoroutineTaskManager : MonoBehaviour
    {
        public static ReCoroutineTaskManager Instance;
        public static Dictionary<string, ReCoroutineTask> task_list = new Dictionary<string, ReCoroutineTask>();

        #region MONO

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                LogManager.Error("ReCoroutineTaskManager Instance Error");
            task_list = new Dictionary<string, ReCoroutineTask>();
            //GameObject.DontDestroyOnLoad(gameObject);
            //gameObject.hideFlags |= HideFlags.HideAndDontSave;
        }

        #endregion

        #region Add Task

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public ReCoroutineTask AddTask(string task_name, IEnumerator<float> ienumer,
            Action<bool> call_back = null, object bind_object = null, bool auto_start = true)
        {
            if (task_list.ContainsKey(task_name))
            {
                //Debug.logger.LogError("添加新任务", "任务重名！" + taskName);
                Restart(task_name);
                return task_list[task_name];
            }
            else
            {
                ReCoroutineTask task = new ReCoroutineTask(task_name, ienumer, call_back, bind_object, auto_start);
                task_list.Add(task_name, task);
                return task;
            }
        }

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public ReCoroutineTask AddTask(IEnumerator<float> ienumer,
            Action<bool> call_back = null, object bind_object = null, bool auto_start = true)
        {
            ReCoroutineTask task = new ReCoroutineTask(ienumer, call_back, bind_object, auto_start);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public ReCoroutineTask AddTask(ReCoroutineTask task)
        {
            if (task_list.ContainsKey(task.Name))
            {
                //Debug.logger.LogError("添加新任务", "任务重名！" + task.name);
                Restart(task.Name);
            }
            else
            {
                task_list.Add(task.Name, task);
            }
            return task;
        }

        #endregion

        #region  Play/Pause/UnPause/Stop/StopAll

        /// <summary>
        /// 开始一个任务
        /// </summary>
        public void DoTask(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("开始任务", "不存在该任务" + task_name);
                return;
            }
            task_list[task_name].Start();
        }

        /// <summary>
        /// 暂停协程
        /// </summary>
        public void Pause(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("暂停任务", "不存在该任务" + task_name);
                return;
            }
            task_list[task_name].Pause();

        }

        /// <summary>
        /// 取消暂停某个协程
        /// </summary>
        public void Unpause(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("重新开始任务", "不存在该任务" + task_name);
                return;
            }
            task_list[task_name].UnPause();
        }

        /// <summary>
        /// 停止特定协程
        /// </summary>
        public void Stop(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("停止任务", "不存在该任务" + task_name);
                return;
            }
            task_list[task_name].Stop();
        }

        public void Restart(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("重新开始任务", "不存在该任务" + task_name);
                return;
            }
            ReCoroutineTask task = task_list[task_name];
            Stop(task_name);
            AddTask(task);
        }

        /// <summary>
        /// 停止所有协程
        /// </summary>
        public void StopAll()
        {
            List<ReCoroutineTask> tamp_list = new List<ReCoroutineTask>();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (ReCoroutineTask task in task_list.Values)
            {
                tamp_list.Add(task);
            }

            int length = tamp_list.Count;
            for (int i = 0; i < length; i++)
            {
                tamp_list[i].Stop();
            }
        }

        #endregion

        #region Wait 

        /// <summary>
        /// 等待一段时间再执行时间
        /// </summary>
        public ReCoroutineTask WaitSecondTodo(Action call_back, float time, object bind_object = null)
        {
            // ReSharper disable once RedundantLambdaSignatureParentheses
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitTodo(time),
                call_back2, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等待一段时间再执行时间
        /// </summary>
        public ReCoroutineTask WaitSecondTodo(Action<bool> call_back, float time, object bind_object = null)
        {
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitTodo(time),
                call_back, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等到下一帧
        /// </summary>
        public ReCoroutineTask WaitFrameEnd(Action call_back, object bind_object = null)
        {
            // ReSharper disable once RedundantLambdaSignatureParentheses
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitFrameEndTodo(),
                call_back2, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等待直到某个条件成立时
        /// </summary>
        public ReCoroutineTask WaitUntilTodo(Action call_back, Func<bool> predicates = null,
            object bind_object = null)
        {
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitUntil(predicates), call_back2, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 当条件成立时等待
        /// </summary>
        public ReCoroutineTask WaitWhileTodo(Action call_back, Func<bool> predicates,
            object bind_object = null)
        {
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitWhile(predicates), call_back2, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等待所有其他携程任务完成
        /// </summary>
        public ReCoroutineTask WaitForAllCoroutine(Action call_back, ReCoroutineTask[] tasks,
    object bind_object = null)
        {
            // ReSharper disable once RedundantLambdaSignatureParentheses
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            ReCoroutineTask task = new ReCoroutineTask(
                DoWaitForAllCoroutine(tasks), call_back2, bind_object);
            AddTask(task);
            return task;
        }


        #endregion

        #region Loop

        /// <summary>
        /// 间隔时间进行多次动作
        /// </summary>
        public ReCoroutineTask LoopTodoByTime(Action call_back, float interval,
            int loop_time, object bind_object = null, float start_time = 0)
        {

            ReCoroutineTask task = new ReCoroutineTask(
                DoLoopByTime(interval, loop_time, call_back, start_time), null, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 每帧进行循环
        /// </summary>
        public ReCoroutineTask LoopByEveryFrame(Action call_back, int loop_time = -1
            , object bind_object = null, float start_time = 0)
        {
            ReCoroutineTask task = new ReCoroutineTask(
                DoLoopByEveryFrame(loop_time, call_back, start_time), null, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 当满足条件循环动作
        /// </summary>
        public ReCoroutineTask LoopTodoByWhile(Action call_back, float interval, Func<bool> predicates,
            object bind_object = null, float start_time = 0)
        {
            ReCoroutineTask task = new ReCoroutineTask(
                DoLoopByWhile(interval, predicates, call_back, start_time), null, bind_object);
            AddTask(task);
            return task;
        }
        #endregion

        #region DoLoop IEnumerator

        private IEnumerator<float> DoLoopByTime(float interval, int loop_time,
            Action call_back, float start_time)
        {
            yield return start_time;
            if (loop_time <= 0)
            {
                loop_time = int.MaxValue;
            }
            int loop_num = 0;
            while (loop_num < loop_time)
            {
                loop_num++;
                call_back();
                yield return interval;
            }
        }

        private IEnumerator<float> DoLoopByEveryFrame(int loop_time,
            Action call_back, float start_time)
        {
            yield return start_time;
            if (loop_time <= 0)
            {
                loop_time = int.MaxValue;
            }
            int loop_num = 0;
            while (loop_num < loop_time)
            {
                loop_num++;
                call_back();
                yield return Time.deltaTime;
            }
        }

        private IEnumerator<float> DoLoopByWhile(float interval,
            Func<bool> predicates, Action call_back, float start_time)
        {
            yield return start_time;

            // ReSharper disable once NotAccessedVariable
            int loop_num = 0;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (predicates())
            {
                loop_num++;
                call_back();
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
            bool all_finished;
            // ReSharper disable once TooWideLocalVariableScope
            int length;
            while (true)
            {
                all_finished = true;
                length = coroutines.Length;
                for (int i = 0; i < length; i++)
                {
                    if (!coroutines[i].IsFinished)
                    {
                        all_finished = false;
                        break;
                    }
                }
                if (all_finished)
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