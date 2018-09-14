using System;
using System.Collections.Generic;
using System.Text;
using Summer;
namespace SummerEditor
{
    public class AssetBundleReportInfo
    {
        public string AssetBundleName;
        public int FileSize;
        public int CalMemSize;
        public int RepeatMemSize;
        public int DepAb;
        public int RepeatAb;

        public void SetInfo(List<string> contents)
        {
            AssetBundleName = contents[0];
            FileSize = (int)(float.Parse(contents[1]));
            CalMemSize = (int)float.Parse(contents[2]);
            RepeatMemSize = (int)float.Parse(contents[3]);
            DepAb = int.Parse(contents[4]);
            RepeatAb = int.Parse(contents[5]);
        }
    }

    /// <summary>
    /// AssetBundle资源列表
    /// </summary>
    public class AssetBundleReport
    {
        public const string ASSETBUNDLE_REPORT_NAME = "AssetBundle资源列表.csv";

        public const string ASSETBUNDLE_NAME = "AB 名称";
        public const string AB_MEMORY_SIZE = "AB文件大小";
        public const string CAL_MEMORY_SIZE = "统计的AB内存Kb(估算)";
        public const string REPEAT_MEMORY_SIZE = "冗余内存 Kb(估算)";
        public const string AB_DEP = "依赖AB数";
        public const string BE_REF = "冗余资源数";
        public const string MESH = "   网格数";
        public const string MATERIAL = "   材质球";
        public const string TEXTURE = "    贴图数";
        public const string SPRITE = " 图集数";
        public const string SHADER = "着色器";
        public const string ANIMATION_CLIP = "动作文件";
        public const string AUDIO_CLIP = "音效";

        public static string[] _titles = new string[]
        {
            ASSETBUNDLE_NAME, AB_MEMORY_SIZE ,CAL_MEMORY_SIZE,
            REPEAT_MEMORY_SIZE,REPEAT_MEMORY_SIZE,AB_DEP,
            BE_REF,MESH,MATERIAL,
            TEXTURE,SPRITE,SPRITE,
            SHADER,ANIMATION_CLIP,AUDIO_CLIP
        };
        public static float[] _titlesWidth = new float[]
        {
            240, 80 ,160,
            120,100,80,
            80,80,80,
            80,80,80,
            80,80,80
        };


        public static void CreateReport(string directoryPath)
        {
            List<EAssetBundleFileInfo> assetbundleFiles = AssetBundleAnalyzeManager.FindAssetBundleFiles();
            assetbundleFiles.Sort(SortAsset);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0},{1},{2},{12}," +
                                        "{3},{4},{5}," +
                                        "{6},{7},{8}," +
                                        "{9},{10},{11}",
                ASSETBUNDLE_NAME, AB_MEMORY_SIZE, CAL_MEMORY_SIZE,
                AB_DEP, BE_REF, MESH,
                MATERIAL, TEXTURE, SHADER,
                SPRITE, ANIMATION_CLIP, AUDIO_CLIP, REPEAT_MEMORY_SIZE));

            int length = assetbundleFiles.Count;
            for (int i = 0; i < length; i++)
            {

                EAssetBundleFileInfo info = assetbundleFiles[i];

                int tRepeatMemSize = (int) info.GetRepeatMemSize();
                int tAbSize = (int) info.FileAbMemorySize;
                int tAbMemSize = (int) (info.GetMemorySize());
                sb.AppendLine(string.Format("{0},{1},{2},{12}," +
                                    "{3},{4},{5}," +
                                    "{6},{7},{8}," +
                                    "{9},{10},{11}",
            info.AbName, tAbSize, tAbMemSize,
            info._allDepends.Count, info.FindRedundance(), info.GetAssetCount(E_AssetType.MESH),
            info.GetAssetCount(E_AssetType.MATERIAL), info.GetAssetCount(E_AssetType.TEXTURE), info.GetAssetCount(E_AssetType.SHADER),
            info.GetAssetCount(E_AssetType.SPRITE), info.GetAssetCount(E_AssetType.ANIMATION_CLIP), info.GetAssetCount(E_AssetType.AUDIO_CLIP), tRepeatMemSize));


            }

            FileHelper.WriteTxtByFile(directoryPath + "/" + ASSETBUNDLE_REPORT_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetBundleFileInfo a, EAssetBundleFileInfo b)
        {
            if (a == null || b == null) return 0;
            return String.Compare(a.AbName, b.AbName, StringComparison.Ordinal);
        }
    }
}
