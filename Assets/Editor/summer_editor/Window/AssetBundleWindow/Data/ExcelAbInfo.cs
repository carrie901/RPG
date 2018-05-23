using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    public class ExcelAbInfo
    {
        public string asset_path;
        public int ref_count;                   // 引用数量
        public int be_ref_count;                // 被引用数据
        public float mem_size;                  // 内存占用
        public float file_size;                 // 文件大小
        public int ref_texture;                 // 引用贴图数量
    }

    public class ExcelAbManager
    {
        public static string csv_path = Application.dataPath + "AbAnalysis.csv";
        public static List<ExcelAbInfo> infos = new List<ExcelAbInfo>();
    }



}

