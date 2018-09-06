using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-8 11:33:27
// FileName : EGuidHelper.cs
//=============================================================================

namespace SummerEditor
{
    public class EGuidHelper
    {
        /// <summary>
        /// 读取文件中的guid值
        /// </summary>
        public static List<string> GetGuidsByFile(string file_path)
        {
            List<string> guids = new List<string>();
            Regex reg = new Regex(@"([a-f0-9]{32})");
            string[] lines = File.ReadAllLines(file_path);
            int length = lines.Length;
            for (int i = 0; i < length; i++)
            {
                Match m = reg.Match(lines[i]);
                if (m.Success)
                {
                    guids.Add(m.Groups[0].Value);
                }
            }
            return guids;
        }

        public static string FindSelectionGuid()
        {
            if (Selection.activeObject == null) return string.Empty; ;
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            return GetGuid(path);
        }

        public static string GetGuid(string path)
        {
            if (string.IsNullOrEmpty(path)) return string.Empty;
            string guid = AssetDatabase.AssetPathToGUID(path);
            return guid;
        }
    }
}
