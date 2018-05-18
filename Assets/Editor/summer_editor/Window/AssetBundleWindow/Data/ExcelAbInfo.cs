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
        public int ref_count;
        public int be_ref_count;
        public float mem_size;
    }

    public class ExcelAbManager
    {
        public static string csv_path = Application.dataPath + "AbAnalysis.csv";
        public static List<ExcelAbInfo> infos = new List<ExcelAbInfo>();
        public static void Read()
        {
            string text = FileHelper.ReadAllText(csv_path);

            string[] lines = text.ToStrs(StringHelper.split_huanhang);
            infos.Clear();
            int length = lines.Length;
            for (int i = 0; i < length; i++)
            {
                ExcelAbInfo info = new ExcelAbInfo();
                if (lines[i].Length == 0) continue;
                string[] contents = lines[i].ToStrs(StringHelper.split_douhao);
                if (contents.Length == 0)
                {
                    Debug.LogError("lines[i]:" + lines[i]);
                    continue;
                }
                info.asset_path = contents[0];
                info.ref_count = int.Parse(contents[1]);
                info.be_ref_count = int.Parse(contents[2]);
                info.mem_size = float.Parse(contents[3]);

                infos.Add(info);
            }
        }

        [MenuItem("Tools/测试Ab/生成csv")]
        public static void Write()
        {
            List<string> paths = EPathHelper.GetAssetPathList01(EAssetBundleConst.main_driectory, true);
            infos.Clear();
            for (int i = 0; i < paths.Count; i++)
            {
                ExcelAbInfo info = new ExcelAbInfo();
                info.asset_path = paths[i];
                info.be_ref_count = (int)(Random.value * 100);
                info.ref_count = (int)(Random.value * 100);
                info.mem_size = 10;
                infos.Add(info);
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < infos.Count; i++)
            {
                sb.AppendLine(
                    infos[i].asset_path
                    + "," + infos[i].ref_count
                    + "," + infos[i].be_ref_count
                    + "," + infos[i].mem_size);
            }

            FileHelper.WriteTxtByFile(csv_path, sb.ToString());
        }
    }



}

