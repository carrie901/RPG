
using System;
using System.Collections.Generic;
using System.Text;
using Summer;

namespace SummerEditor
{
    public class MeshReport
    {
        public const string MESH_NAME = "网格.csv";

        public static void CreateReport(string directory_path)
        {
            List<EAssetFileInfo> asset_files = new List<EAssetFileInfo>(AssetBundleAnalyzeManager.FindAssetFiles().Values);
            asset_files.Sort(SortAsset);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0},{1},{2}," +
                                       "{3},{4},{5}",
               "网格名字", "顶点数", "面数",
               "子网格数", "网格压缩", "Read/Write"));

            int length = asset_files.Count;
            for (int i = 0; i < length; i++)
            {
                EAssetFileInfo asset_file_info = asset_files[i];
                if (asset_file_info.asset_type != E_AssetType.mesh) continue;
                List<KeyValuePair<string, System.Object>> values = asset_file_info.propertys;

                sb.Append(string.Format("{0},{1},{2}," +
                                        "{3},{4},{5}",
                asset_file_info.asset_name, values[0].Value, values[1].Value,
                values[2].Value, values[3].Value, values[4].Value));

                int ref_count = asset_file_info.included_bundles.Count;
                for (int j = 0; j < ref_count; j++)
                    sb.Append("," + asset_file_info.included_bundles[j].ab_name);
                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directory_path + "/" + MESH_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetFileInfo a, EAssetFileInfo b)
        {
            return String.CompareOrdinal(a.asset_name, b.asset_name);
        }
    }
}
