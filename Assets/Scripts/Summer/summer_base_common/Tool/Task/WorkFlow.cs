using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:16:10
// FileName : WorkFlow.cs
//=============================================================================

namespace Summer.Tool
{
    /// <summary>
    /// WorkFlow：作为整个工作流的容器，包含了所有的任务以及属性
    /// Task：组成工作流的组件，其中组件我们可以复用，我们可以定义自己的Task来供以后使用
    /// Property：定义了WorkFlow的一些属性，供Task使用
    /// </summary>
    public class WorkFlow
    {
        #region Property

        public readonly DictionaryList<string, Property> _propertys                 // 依赖的属性
            = new DictionaryList<string, Property>();
        public Dictionary<string, object> _context                                  // 上下文
            = new Dictionary<string, object>();
        public DictionaryList<string, Task> _taskDict                               // 任务列表
            = new DictionaryList<string, Task>();

        public string Name { get; set; }                                            // 名称
        public string Description { get; set; }                                     // 描述

        #endregion

        #region construction

        public WorkFlow(string name, string description)
        {
            Name = name;
            Description = description;
        }

        #endregion

        #region public

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="task"></param>
        public WorkFlow AddTask(Task task)
        {
            if (task.WorkFlow != null && task.WorkFlow != this)
            {
                LogManager.Error("添加任务错误,任务[{0}]已经在别的工程当中了", task.Name);
                return this;
            }
            if (_taskDict.ContainsKey(task.Name))
            {
                LogManager.Error("添加任务错误,任务[{0}]不可重复添加", task.Name);
                return this;
            }
            task.WorkFlow = this;
            _taskDict.Add(task.Name, task);
            return this;
        }

        /// <summary>
        /// 应用属性
        /// </summary>
        /// <param name="prop"></param>
        public WorkFlow Apply(Property prop)
        {
            if (_propertys.ContainsKey(prop.Name))
            {
                LogManager.Error("添加属性错误,属性[{0}]不可重复添加", prop.Name);
                return this;
            }
            _propertys.Add(prop.Name, prop);
            return this;
        }

        /// <summary>
        /// 开始执行流程
        /// </summary>
        public void Start()
        {
            //应用属性
            for (int i = 0; i < _propertys.Count; i++)
            {
                _propertys.GetValueAt(i).Apply(this);
            }
            ReCoroutineManager.AddCoroutine(Run());
        }

        #endregion

        #region private

        /// <summary>
        /// 执行
        /// </summary>
        private IEnumerator<float> Run()
        {
            if (_taskDict.Count == 0) yield break;
            //ReCoroutine[] tasks = new ReCoroutine[task_dep_dict.Count];
            for (int i = 0; i < _taskDict.Count; i++)
            {
                //按顺序执行 
                yield return ReCoroutine.Wait(_taskDict.GetValueAt(i).GetCoroutine());
            }
        }

        #endregion
    }
}
