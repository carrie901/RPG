using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:13:11
// FileName : Property.cs
//=============================================================================

namespace Summer.Tool
{
    public abstract class Property
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 应用属性 
        /// </summary>
        public abstract void Apply(WorkFlow work_flow);

    }
}
