using UnityEngine;
using System.Collections;

//=============================================================================
/// Author : mashao
/// CreateTime : 2018-2-1 15:32:55
/// FileName : I_ContentFilter.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 过滤
    /// </summary>
    public interface I_ContentFilter
    {
        bool FilterContent(string path);
    }
}
