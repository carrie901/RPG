using UnityEngine;
using System.Collections;
using System.IO;

namespace SummerEditor
{
    public class EditorDirectoryTool
    {
        //创建文件夹
        public static void CreateDirectory(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            string dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }

    }
}

