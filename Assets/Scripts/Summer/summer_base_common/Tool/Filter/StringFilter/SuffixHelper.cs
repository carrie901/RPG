using System.Collections.Generic;

//=============================================================================
/// Author : mashao
/// CreateTime : 2018-2-1 15:34:29
/// FileName : SuffixHelper.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 后缀名的过滤Helper
    /// </summary>
    public class SuffixHelper
    {
        public static List<string> Filter(string[] files, I_ContentFilter filter)
        {
            List<string> contents = new List<string>();
            int length = files.Length;
            for (int i = 0; i < length; i++)
            {
                if (filter.FilterContent(files[i]))
                    contents.Add(files[i]);
            }
            return contents;
        }

        public static void Filter(List<string> files, I_ContentFilter filter)
        {
            int length = files.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                if (!filter.FilterContent(files[i]))
                    files.RemoveAt(i);
            }
        }

    }
}
