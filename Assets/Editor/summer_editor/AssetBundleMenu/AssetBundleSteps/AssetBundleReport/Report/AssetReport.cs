using System;
using System.Collections.Generic;
using System.Text;
using Summer;
namespace SummerEditor
{
    /// <summary>
    /// 资源列表
    /// </summary>
    public class AssetReport
    {
        public const string ASSET_REPORT_NAME = "Asset资源列表.csv";

        public static string asset_name = "资源名称";
        public static string asset_size = "内存大小";
        public static string in_built = "内建资源";
        public static string asset_type = "资源类型";
        public static string be_ref_count = "被引用的个数";
        public static string ab_list = "对应的AB包名称";
        public static void CreateReport(string directory_path)
        {

            List<EAssetFileInfo> asset_files = new List<EAssetFileInfo>(AssetBundleAnalyzeManager.FindAssetFiles().Values);
            asset_files.Sort(SortAsset);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0},{1},{5},{2},{3},{4}", asset_name, asset_size, asset_type, be_ref_count, ab_list, in_built));

            int length = asset_files.Count;
            for (int i = 0; i < length; i++)
            {
                EAssetFileInfo info = asset_files[i];
                sb.Append(info.asset_name + "," + info.GetMemorySize() + "," + info.in_built + "," + info.asset_type + "," + info.included_bundles.Count);
                int ref_count = info.included_bundles.Count;
                for (int j = 0; j < ref_count; j++)
                {
                    EAssetBundleFileInfo assetbundle_file = info.included_bundles[j];
                    sb.Append("," + assetbundle_file.ab_name);
                }

                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directory_path + "/" + ASSET_REPORT_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetFileInfo a, EAssetFileInfo b)
        {
            if (a == null || b == null) return 0;
            if (a.asset_type < b.asset_type)
            {
                return -1;
            }
            else if (a.asset_type > b.asset_type)
            {
                return 1;
            }
            else
            {
                return String.Compare(a.asset_name, b.asset_name, StringComparison.Ordinal);
            }
        }
    }
}
