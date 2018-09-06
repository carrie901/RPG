using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-8 11:30:37
// FileName : PrefabHelper.cs
//=============================================================================

namespace SummerEditor
{
    public class EPrefabHelper
    {
        /// <summary>
        /// 查找Prefab的实际保存的路径
        /// </summary>
        /// <returns></returns>
	    public static string FindPrefabPath(GameObject go)
        {
            Object obj = PrefabUtility.GetPrefabParent(Selection.activeGameObject);
            string path = AssetDatabase.GetAssetPath(obj);
            return path;
        }
    }
}
