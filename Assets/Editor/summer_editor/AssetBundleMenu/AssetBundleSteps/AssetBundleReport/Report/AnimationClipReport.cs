
using System;
using System.Collections.Generic;
using System.Text;
using Summer;

namespace SummerEditor
{
    public class AnimationClipReport 
    {
        public const string ANIMATIONCLIP_NAME = "动作文件.csv";
        public static void CreateReport(string directory_path)
        {
            List<EAssetFileInfo> assetFiles = new List<EAssetFileInfo>(AssetBundleAnalyzeManager.FindAssetFiles().Values);
            assetFiles.Sort(SortAsset);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0},{1}",
               "动作名称", "占用内存"));

            int length = assetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                EAssetFileInfo assetFileInfo = assetFiles[i];
                if (assetFileInfo._assetType != E_AssetType.ANIMATION_CLIP) continue;
                List<KeyValuePair<string, System.Object>> values = assetFileInfo._propertys;

                sb.Append(string.Format("{0},{1}",
                assetFileInfo._assetName, values[0].Value));

                int refCount = assetFileInfo._includedBundles.Count;
                for (int j = 0; j < refCount; j++)
                    sb.Append("," + assetFileInfo._includedBundles[j].AbName);
                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directory_path + "/" + ANIMATIONCLIP_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetFileInfo a, EAssetFileInfo b)
        {
            return String.CompareOrdinal(a._assetName, b._assetName);
        }
    }
}
