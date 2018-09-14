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

        public static string _assetbundleName = "AssetBundle 名称";
        public static string _texture = "贴图";
        public const string MESH = "网格数";
        public const string MATERIAL = "材质球";
        public const string SPRITE = " 图集数";
        public const string SHADER = "着色器";
        public const string ANIMATION_CLIP = "动作文件";
        public const string AUDIO_CLIP = "音效";


        public static string _redundance = "冗余";

        public static void CreateReport(string directoryPath)
        {
            List<EAssetBundleFileInfo> assetbundle_files = AssetBundleAnalyzeManager.FindAssetBundleFiles();
            assetbundle_files.Sort(SortAsset);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_assetbundleName);

            int length = assetbundle_files.Count;
            for (int i = 0; i < length; i++)
            {
                sb.AppendLine(assetbundle_files[i].AbName);
                AppendLine(sb, assetbundle_files[i]);
                AppendLineRedundance(sb, assetbundle_files[i]);
                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directoryPath + "/" + ASSETBUNDLEDEP_REPORT_NAME, sb.ToString());
        }

        public static void AppendLine(StringBuilder sb, EAssetBundleFileInfo abFileInfo)
        {
            sb.AppendLine(" , OK");
            AppendType0(sb, abFileInfo, E_AssetType.TEXTURE, false, _texture);
            AppendType0(sb, abFileInfo, E_AssetType.MESH, false, MESH);
            AppendType0(sb, abFileInfo, E_AssetType.MATERIAL, false, MATERIAL);
            AppendType0(sb, abFileInfo, E_AssetType.SPRITE, false, SPRITE);
            AppendType0(sb, abFileInfo, E_AssetType.SHADER, false, SHADER);
            AppendType0(sb, abFileInfo, E_AssetType.ANIMATION_CLIP, false, ANIMATION_CLIP);
            AppendType0(sb, abFileInfo, E_AssetType.AUDIO_CLIP, false, AUDIO_CLIP);
        }

        public static void AppendLineRedundance(StringBuilder sb, EAssetBundleFileInfo abFileInfo)
        {
            sb.AppendLine(" , " + _redundance + "[" + abFileInfo.FindRedundance() + "]");
            AppendType(sb, abFileInfo, E_AssetType.TEXTURE, true, _texture);
            AppendType(sb, abFileInfo, E_AssetType.MESH, true, MESH);
            AppendType(sb, abFileInfo, E_AssetType.MATERIAL, true, MATERIAL);
            AppendType(sb, abFileInfo, E_AssetType.SPRITE, true, SPRITE);
            AppendType(sb, abFileInfo, E_AssetType.SHADER, true, SHADER);
            AppendType(sb, abFileInfo, E_AssetType.ANIMATION_CLIP, true, ANIMATION_CLIP);
            AppendType(sb, abFileInfo, E_AssetType.AUDIO_CLIP, true, AUDIO_CLIP);
        }

        public static void AppendType0(StringBuilder sb, EAssetBundleFileInfo abFileInfo, E_AssetType assetType, bool isEdundance, string des)
        {
            List<EAssetFileInfo> assetFiles = new List<EAssetFileInfo>();
            abFileInfo.FindAssetFiles(assetFiles, assetType);
            if (assetFiles.Count == 0) return;

            sb.Append(",," + des);
            int length = assetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                bool result = assetFiles[i]._includedBundles.Count > 1;
                if (result == isEdundance)
                {
                    sb.Append("," + assetFiles[i]._assetName);
                }
            }
            sb.AppendLine();
        }

        public static void AppendType(StringBuilder sb, EAssetBundleFileInfo abFileInfo, E_AssetType assetType, bool isEdundance, string des)
        {
            List<EAssetFileInfo> assetFiles = new List<EAssetFileInfo>();
            abFileInfo.FindAssetFiles(assetFiles, assetType);
            if (assetFiles.Count == 0) return;

            float allSize = 0;
            int length = assetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                bool result = assetFiles[i]._includedBundles.Count > 1;
                if (result == isEdundance)
                {
                    allSize += assetFiles[i].GetMemorySize();
                }
            }

            sb.Append(",," + des + "(" + allSize + ")");
            for (int i = 0; i < length; i++)
            {
                bool result = assetFiles[i]._includedBundles.Count > 1;
                if (result == isEdundance)
                {
                    string tmpSize = (assetFiles[i].GetMemorySize()).ToString("f2");
                    sb.Append("," + assetFiles[i]._assetName + "(" + tmpSize + "Kb)");
                }
            }
            sb.AppendLine();
        }

        public static int SortAsset(EAssetBundleFileInfo a, EAssetBundleFileInfo b)
        {
            if (a == null || b == null) return 0;
            return String.Compare(a.AbName, b.AbName, StringComparison.Ordinal);
        }

    }
}
