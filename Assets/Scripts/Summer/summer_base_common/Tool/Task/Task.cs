using System;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:16:0
// FileName : Task.cs
//=============================================================================

namespace Summer.Tool
{
    public enum E_TaskActionType
    {
        sync,               // 同步
        asyn                // 异步
    }

    public class Task
    {
        #region Property

        public string Name { get; set; }                                    // 名称
        public string Description { get; set; }                             // 描述
        public E_TaskActionType ActionType { get; private set; }            // 该任务为同步或者异步
        public WorkFlow WorkFlow { get; set; }                              // 所处的工程
        public bool IsDone { get; private set; }                            // 是否已经完成
        public bool IsRunning { get; private set; }                         // 是否正在运行

        protected Dictionary<string, object> _input_map;                    // 输入
        protected Dictionary<string, object> _output_map;                   // 输出
        protected List<Action> _sync_actions;                               // 同步方法 
        protected List<IEnumerator<float>> _asyn_actions;                   // 异步方法

        public DictionaryList<string, Task> task_dep_dict                   // 依赖的任务
            = new DictionaryList<string, Task>();

        #endregion

        #region construction

        /// <summary>
        /// 指定任务的名称/描述信息/任务类型
        /// </summary>
        public Task(string name, string description, E_TaskActionType action_type)
        {
            Name = name;
            Description = description;
            ActionType = action_type;

            _input_map = new Dictionary<string, object>(8);
            _output_map = new Dictionary<string, object>(8);
            IsDone = false;

            if (action_type == E_TaskActionType.sync)
                _sync_actions = new List<Action>();
            else
                _asyn_actions = new List<IEnumerator<float>>();

            WorkFlow = null;
            //IsRunning = false;
        }

        #endregion

        #region 依赖任务/ 添加同步和异步

        /// <summary>
        /// 依赖任务,本任务(A)依赖指定任务(B)
        /// 只有B任务完成了，才会接着执行A任务
        /// </summary>
        public Task DependsOn(Task task)
        {
            if (task_dep_dict.ContainsKey(task.Name)) return this;
            task_dep_dict.Add(task.Name, task);
            return this;
        }

        /// <summary>
        /// 同步行为
        /// </summary>
        public Task AddSync(Action closure)
        {
            // 如果当前任务为异步任务，添加一个同步行为
            if (ActionType == E_TaskActionType.asyn)
            {
                _asyn_actions.Add(Sync2Aync(closure));
            }
            else
            {
                _sync_actions.Add(closure);
            }
            return this;
        }

        /// <summary>
        /// 添加异步行为
        /// </summary>
        public Task AddAync(IEnumerator<float> e)
        {
            // 当前任务为同步任务
            if (ActionType == E_TaskActionType.sync)
            {
                LogManager.Error("添加异步行为,同步任务中不允许加入异步行为");
                return this;
            }
            _asyn_actions.Add(e);
            return this;
        }

        #endregion

        #region 输入输出参数的添加和获取

        /// <summary>
        /// 获取输入参数
        /// </summary>
        public T FromInput<T>(string key)
        {
            return (T)(_input_map[key]);
        }

        /// <summary>
        /// 获取输出参数
        /// </summary>
        public T FromOutput<T>(string key)
        {
            return (T)(_output_map[key]);
        }

        /// <summary>
        /// 添加输出参数
        /// </summary>
        public Task AddOutput(string name, object out_obj)
        {
            _output_map.Add(name, out_obj);
            return this;
        }

        #endregion

        #region public

        /// <summary>
        /// 获取执行携程
        /// </summary>
        public ReCoroutine GetCoroutine()
        {
            if (IsRunning)
            {
                return ReCoroutineManager.AddCoroutine(WaitForFinish());
            }
            return ReCoroutineManager.AddCoroutine(Run());
        }

        #endregion

        #region private 

        // 同步行为变成异步行为
        private IEnumerator<float> Sync2Aync(Action closure)
        {
            closure();
            yield return 0;
        }

        /// <summary>
        /// 等待该任务执行完成
        /// </summary>
        private IEnumerator<float> WaitForFinish()
        {
            while (IsRunning)
            {
                yield return 0;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        private IEnumerator<float> Run()
        {
            if (IsDone)
                yield break;

            // 1.根据依赖任务转换成协同，并且同时执行所有协同
            var tasks = new ReCoroutine[task_dep_dict.Count];
            int length = task_dep_dict.Count;
            for (int i = 0; i < length; i++)
            {
                tasks[i] = task_dep_dict.GetValueAt(i).GetCoroutine();
            }
            // 同时执行所有的任务
            yield return ReCoroutine.WaitForAllCoroutines(tasks);

            // 2.任务==同步类型，那么执行同步操作
            if (ActionType == E_TaskActionType.sync)
            {
                length = _sync_actions.Count;
                //同步按顺序执行
                for (int i = 0; i < length; i++)
                {
                    _sync_actions[i]();
                }
            }
            else // 3.任务==异步类型,执行异步任务
            {
                length = _asyn_actions.Count;
                //异步按顺序执行
                for (int i = 0; i < length; i++)
                {
                    yield return ReCoroutine.Wait(_asyn_actions[i]);
                }
            }

            IsDone = true;
        }

        #endregion
    }
}
