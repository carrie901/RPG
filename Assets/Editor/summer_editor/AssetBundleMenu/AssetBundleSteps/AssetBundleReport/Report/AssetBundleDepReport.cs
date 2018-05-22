using System;
using System.Text;
using System.Collections.Generic;
using Summer;

namespace SummerEditor
{
    /// <summary>
    /// 依赖报告
    /// </summary>
    public class AssetBundleDepReport
    {
        public const string ASSETBUNDLEDEP_REPORT_NAME = "AssetBundle依赖信息.csv";

        public static string assetbundle_name = "AssetBundle 名称";
        public static string texture = "贴图";
        public static string mesh = "网格数";
        public static string material = "材质球";
        public static string sprite = " 图集数";
        public static string shader = "着色器";
        public static string animation_clip = "动作文件";
        public static string audio_clip = "音效";


        public static string redundance = "冗余";

        public static void CreateReport(string directory_path)
        {
            List<EAssetBundleFileInfo> assetbundle_files = AssetBundleAnalyzeManager.FindAssetBundleFiles();
            assetbundle_files.Sort(SortAsset);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(assetbundle_name);

            int length = assetbundle_files.Count;
            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(assetbundle_files[i].ab_name);
                AppendLine(sb, assetbundle_files[i]);
                AppendLineRedundance(sb, assetbundle_files[i]);
                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directory_path + "/" + ASSETBUNDLEDEP_REPORT_NAME, sb.ToString());
        }

        public static void AppendLine(StringBuilder sb, EAssetBundleFileInfo ab_file_info)
        {
            sb.AppendLine(" , OK");
            AppendType(sb, ab_file_info, E_AssetType.texture, false, texture);
            AppendType(sb, ab_file_info, E_AssetType.mesh, false, mesh);
            AppendType(sb, ab_file_info, E_AssetType.material, false, material);
            AppendType(sb, ab_file_info, E_AssetType.sprite, false, sprite);
            AppendType(sb, ab_file_info, E_AssetType.shader, false, shader);
            AppendType(sb, ab_file_info, E_AssetType.animation_clip, false, animation_clip);
            AppendType(sb, ab_file_info, E_AssetType.audio_clip, false, audio_clip);
        }

        public static void AppendLineRedundance(StringBuilder sb, EAssetBundleFileInfo ab_file_info)
        {
            sb.AppendLine(" , " + redundance + "[" + ab_file_info.FindRedundance() + "]");
            AppendType(sb, ab_file_info, E_AssetType.texture, true, texture);
            AppendType(sb, ab_file_info, E_AssetType.mesh, true, mesh);
            AppendType(sb, ab_file_info, E_AssetType.material, true, material);
            AppendType(sb, ab_file_info, E_AssetType.sprite, true, sprite);
            AppendType(sb, ab_file_info, E_AssetType.shader, true, shader);
            AppendType(sb, ab_file_info, E_AssetType.animation_clip, true, animation_clip);
            AppendType(sb, ab_file_info, E_AssetType.audio_clip, true, audio_clip);
        }


        public static void AppendType(StringBuilder sb, EAssetBundleFileInfo ab_file_info, E_AssetType asset_type, bool is_edundance, string des)
        {
            List<EAssetFileInfo> asset_files = new List<EAssetFileInfo>();
            ab_file_info.FindAssetFiles(asset_files, asset_type);
            if (asset_files.Count == 0) return;
            sb.Append(",," + des);
            for (int i = 0; i < asset_files.Count; i++)
            {
                bool result = asset_files[i].included_bundles.Count > 1;
                if (result == is_edundance)
                {
                    sb.Append("," + asset_files[i].asset_name);
                }
            }
            sb.AppendLine();
        }

        public static int SortAsset(EAssetBundleFileInfo a, EAssetBundleFileInfo b)
        {
            if (a == null || b == null) return 0;
            return String.Compare(a.ab_name, b.ab_name, StringComparison.Ordinal);
        }



    }
}
