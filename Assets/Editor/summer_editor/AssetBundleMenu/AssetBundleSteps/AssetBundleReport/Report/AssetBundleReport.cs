using System;
using System.Collections.Generic;
using System.Text;
using Summer;
namespace SummerEditor
{
    /// <summary>
    /// AssetBundle资源列表
    /// </summary>
    public class AssetBundleReport
    {
        public const string ASSETBUNDLE_REPORT_NAME = "AssetBundle资源列表.csv";

        public static string assetbundle_name = "AssetBundle 名称";
        public static string ab_memory_size = "AssetBundle 内存";
        public static string cal_memory_size = "统计的 ab内存";
        public static string ab_dep = "依赖AB数";
        public static string be_ref = "冗余资源数";
        public static string mesh = "   网格数";
        public static string material = "   材质球";
        public static string texture = "    贴图数";
        public static string sprite = " 图集数";
        public static string shader = "着色器";
        public static string animation_clip = "动作文件";
        public static string audio_clip = "音效";
     
        public static void CreateReport(string directory_path)
        {
            List<EAssetBundleFileInfo> assetbundle_files = AssetBundleAnalyzeManager.FindAssetBundleFiles();
            assetbundle_files.Sort(SortAsset);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0},{1},{2}," +
                                        "{3},{4},{5}," +
                                        "{6},{7},{8}," +
                                        "{9},{10},{11}",
                assetbundle_name, ab_memory_size, cal_memory_size,
                ab_dep, be_ref, mesh,
                material, texture, shader,
                sprite, animation_clip, audio_clip));

            int length = assetbundle_files.Count;
            for (int i = 0; i < length; i++)
            {
                EAssetBundleFileInfo info = assetbundle_files[i];
                sb.AppendLine(string.Format("{0},{1},{2}," +
                                        "{3},{4},{5}," +
                                        "{6},{7},{8}," +
                                        "{9},{10},{11}",
                info.ab_name, EMemorySizeHelper.GetKb(info.file_ab_memory_size), EMemorySizeHelper.GetKb(info.cal_ab_memory_size),
                info.all_depends.Count, info.FindRedundance(), info.GetAssetCount(E_AssetType.mesh),
                info.GetAssetCount(E_AssetType.material), info.GetAssetCount(E_AssetType.texture), info.GetAssetCount(E_AssetType.shader),
                info.GetAssetCount(E_AssetType.sprite), info.GetAssetCount(E_AssetType.animation_clip), info.GetAssetCount(E_AssetType.audio_clip)));
            }

            FileHelper.WriteTxtByFile(directory_path + "/" + ASSETBUNDLE_REPORT_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetBundleFileInfo a, EAssetBundleFileInfo b)
        {
            if (a == null || b == null) return 0;
            return String.Compare(a.ab_name, b.ab_name, StringComparison.Ordinal);
        }
    }
}
