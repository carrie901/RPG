using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Summer.Tool
{
    public class CoroutineTaskManager : MonoBehaviour
    {
        public static CoroutineTaskManager Instance;
        public static Dictionary<string, CoroutineTask> task_list = new Dictionary<string, CoroutineTask>();

        #region MONO

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                LogManager.Error("CoroutineTaskManager Instance Error");
            task_list = new Dictionary<string, CoroutineTask>();
            //GameObject.DontDestroyOnLoad(gameObject);
            //gameObject.hideFlags |= HideFlags.HideAndDontSave;
        }

        #endregion

        #region public 

        #region DoTask/Pause/UnPause/Stop/ReStart/StopAll

        /// <summary>
        /// 开始一个任务
        /// </summary>
        public void DoTask(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("开始任务,不存在该任务[{0}]", task_name);
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
                LogManager.Error("暂停任务,不存在该任务[{0}]", task_name);
                return;
            }
            task_list[task_name].Pause();
        }

        /// <summary>
        /// 取消暂停某个协程
        /// </summary>
        public void UnPause(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("重新开始任务,不存在该任务[{0}]", task_name);
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
                LogManager.Error("停止任务，不存在该任务[{0}]", task_name);
                return;
            }
            task_list[task_name].Stop();
        }

        List<CoroutineTask> temp_List = new List<CoroutineTask>(8);
        /// <summary>
        /// 停止所有协程
        /// </summary>
        public void StopAll()
        {
            temp_List.Clear();
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (CoroutineTask task in task_list.Values)
            {
                temp_List.Add(task);
            }
            int length = temp_List.Count;
            for (int i = 0; i < length; i++)
            {
                temp_List[i].Stop();
            }
        }

        #endregion

        #region Add Task

        #region 基础的添加协同任务

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public void AddTask(string task_name, IEnumerator ienumer, Action<bool> call_back = null, object bind_object = null, bool auto_start = true)
        {
            if (task_list.ContainsKey(task_name))
            {
                LogManager.Error("添加新任务,任务[{0}]重名！", task_name);
                _re_start(task_name);
            }
            else
            {
                CoroutineTask task = new CoroutineTask(task_name, ienumer, call_back, bind_object, auto_start);
                task_list.Add(task_name, task);
            }
        }

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public CoroutineTask AddTask(IEnumerator ienumer, Action<bool> call_back = null, object bind_object = null, bool auto_start = true)
        {
            CoroutineTask task = new CoroutineTask(ienumer, call_back, bind_object, auto_start);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 添加一个新任务
        /// </summary>
        public CoroutineTask AddTask(CoroutineTask task)
        {
            if (task_list.ContainsKey(task.Name))
            {
                LogManager.Error("添加新任务,任务[{0}]重名！", task.Name);
                _re_start(task.Name);
            }
            else
            {
                task_list.Add(task.Name, task);
            }
            return task;
        }

        #endregion

        #region 等待一段时间/等到下一帧/等待直到某个条件成立时/当条件成立时等待 执行Task

        /// <summary>
        /// 等待一段时间再执行回调
        /// </summary>
        public CoroutineTask WaitSecondTodo(Action call_back, float wait_time, object bind_object = null)
        {                    
            // ReSharper disable once RedundantLambdaSignatureParentheses
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            CoroutineTask task = new CoroutineTask(_do_wait_to_do(wait_time), call_back2, bind_object);
            AddTask(task);
            return task;
        }

        public CoroutineTask WaitSecondTodo(Action<bool> call_back, float wait_time, object bind_object = null)
        {
            IEnumerator ienumer = _do_wait_to_do(wait_time);
            CoroutineTask task = new CoroutineTask(ienumer, call_back, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等到下一帧
        /// </summary>
        public CoroutineTask WaitFrameEnd(Action call_back, object bind_object = null)
        {
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            CoroutineTask task = new CoroutineTask(
                _do_wait_frame_end_to_do(),
                call_back2, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 等待直到某个条件成立时
        /// </summary>
        public CoroutineTask WaitUntilTodo(Action call_back, Func<bool> conditions, object bind_object = null)
        {
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            CoroutineTask task = new CoroutineTask(_do_wait_until(conditions), call_back2, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 当条件成立时等待
        /// </summary>
        public CoroutineTask WaitWhileTodo(Action call_back, Func<bool> conditions, object bind_object = null)
        {
            Action<bool> call_back2 = (bo) =>
            {
                if (bo)
                    call_back();
            };
            CoroutineTask task = new CoroutineTask(_do_wait_while(conditions), call_back2, bind_object);
            AddTask(task);
            return task;
        }

        #endregion

        #region 间隔一定时间进行多次动作/每一帧进行循环/条件满足的时循环

        /// <summary>
        /// 间隔时间进行多次动作
        /// </summary>
        public CoroutineTask LoopTodoByTime(Action call_back, float interval
            , int loop_count, object bind_object = null, float delay_time = 0)
        {
            IEnumerator ienumer = _do_loop_by_time(interval, call_back, loop_count, delay_time);
            CoroutineTask task = new CoroutineTask(ienumer, null, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 每帧进行循环
        /// </summary>
        public CoroutineTask LoopByEveryFrame(Action call_back, int loop_time = -1
            , object bind_object = null, float delay_time = 0)
        {
            IEnumerator ienumer = _do_loop_by_every_frame(loop_time, call_back, delay_time);
            CoroutineTask task = new CoroutineTask(ienumer, null, bind_object);
            AddTask(task);
            return task;
        }

        /// <summary>
        /// 当满足条件循环动作
        /// </summary>
        public CoroutineTask LoopTodoByCondition(Action call_back, float interval,
            Func<bool> predicates, object bind_object = null, float start_time = 0)
        {
            IEnumerator ienumer = _do_loop_by_condition(interval, predicates, call_back, start_time);
            CoroutineTask task = new CoroutineTask(ienumer, null, bind_object);
            AddTask(task);
            return task;
        }

        #endregion

        #endregion

        #endregion

        #region private

        #region DoWait

        // 等待一定的时间
        public IEnumerator _do_wait_to_do(float time)
        {
            yield return CoroutineConst.GetWaitForSeconds(time);
        }

        // 等待下一帧
        public IEnumerator _do_wait_frame_end_to_do()
        {
            yield return CoroutineConst.wait_for_end_of_frame;
        }

        //  等待直到某个条件成立时
        public IEnumerator _do_wait_until(Func<bool> conditions)
        {
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (!conditions())
            {
                yield return null;
            }
        }

        // 当条件成立时等待
        public IEnumerator _do_wait_while(Func<bool> conditions)
        {
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (conditions())
            {
                yield return null;
            }
        }

        #endregion

        #region DoLoop

        /// <summary>
        /// 间隔时间进行多次动作
        /// </summary>
        public IEnumerator _do_loop_by_time(float interval, Action call_back, int loop_count = 1, float delay_time = 0f)
        {
            // 间隔时间，循环次数，回调，延迟时间时间
            if (delay_time > 0f)
                yield return CoroutineConst.GetWaitForSeconds(delay_time);

            if (loop_count <= 0)
                loop_count = int.MaxValue;

            int tmp_loop_count = 0;
            while (tmp_loop_count < loop_count)
            {
                tmp_loop_count++;
                call_back();
                yield return CoroutineConst.GetWaitForSeconds(interval);
            }
        }

        /// <summary>
        /// 每帧进行循环
        /// </summary>
        public IEnumerator _do_loop_by_every_frame(int loop_count, Action call_back, float delay_time)
        {
            // 循环次数，回调，开始时间
            if (delay_time > 0f)
                yield return CoroutineConst.GetWaitForSeconds(delay_time);

            if (loop_count <= 0)
                loop_count = int.MaxValue;

            int tmp_loop_count = 0;
            while (tmp_loop_count < loop_count)
            {
                tmp_loop_count++;
                call_back();
                yield return CoroutineConst.wait_for_end_of_frame;
            }
        }

        public IEnumerator _do_loop_by_condition(float interval, Func<bool> condition, Action call_back, float delay_time)
        {
            if (delay_time > 0f)
                yield return CoroutineConst.GetWaitForSeconds(delay_time);
            // ReSharper disable once NotAccessedVariable
            int tmp_loop_count = 0;
            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (condition())
            {
                tmp_loop_count++;
                call_back();
                yield return CoroutineConst.GetWaitForSeconds(interval);
            }
        }

        #endregion

        public void _re_start(string task_name)
        {
            if (!task_list.ContainsKey(task_name))
            {
                LogManager.Error("重新开始任务,不存在该任务[{0}]", task_name);
                return;
            }
            CoroutineTask task = task_list[task_name];
            Stop(task_name);
            AddTask(task);
        }

        #endregion
    }

}
