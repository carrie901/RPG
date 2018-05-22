
using System;
using System.Collections.Generic;
using System.Text;
using Summer;
namespace SummerEditor
{
    public class TextureReport
    {
        public const string TEXTURE_NAME = "纹理.csv";
        public static string texture_name = "资源名称";
        public static string width = "宽度 Width";
        public static string height = "高度 Height";
        public static string format = "格式 Format";
        public static string mip_map = "MipMap功能";
        public static string read_write = "Read/Write";
        public static string size = "内存占用";
        public static string ab_count = "AB文件数量";
        public static string ab_files = "相应的AB文件";
        public static void CreateReport(string directory_path)
        {
            List<EAssetFileInfo> asset_files = new List<EAssetFileInfo>(AssetBundleAnalyzeManager.FindAssetFiles().Values);
            asset_files.Sort(SortAsset);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0},{1},{2}," +
                                       "{3},{4},{5}" +
                                       "{6},{7},{8}",
               texture_name, width, height,
               format, mip_map, read_write,
               size, ab_count, ab_files));

            int length = asset_files.Count;
            for (int i = 0; i < length; i++)
            {
                EAssetFileInfo asset_file_info = asset_files[i];
                if (asset_file_info.asset_type != E_AssetType.texture) continue;
                List<KeyValuePair<string, System.Object>> values = asset_file_info.propertys;

                int mem_size = (int)values[5].Value;
                sb.Append(string.Format("{0},{1},{2}," +
                                        "{3},{4},{5}," +
                                        "{6},{7}",
                asset_file_info.asset_name, values[0].Value, values[1].Value,
                values[2].Value, values[3].Value, values[4].Value,
                EMemorySizeHelper.GetKb((float)mem_size), asset_file_info.included_bundles.Count));

                int ref_count = asset_file_info.included_bundles.Count;
                for (int j = 0; j < ref_count; j++)
                    sb.Append("," + asset_file_info.included_bundles[j].ab_name);
                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directory_path + "/" + TEXTURE_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetFileInfo a, EAssetFileInfo b)
        {
            return String.CompareOrdinal(a.asset_name, b.asset_name);
        }
    }
}
