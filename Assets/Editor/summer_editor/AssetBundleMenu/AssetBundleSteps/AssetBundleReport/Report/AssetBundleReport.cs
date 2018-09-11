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
        public const string mesh = "   网格数";
        public const string material = "   材质球";
        public const string texture = "    贴图数";
        public const string sprite = " 图集数";
        public const string shader = "着色器";
        public const string animation_clip = "动作文件";
        public const string audio_clip = "音效";

        public static string[] titles = new string[]
        {
            ASSETBUNDLE_NAME, AB_MEMORY_SIZE ,CAL_MEMORY_SIZE,
            REPEAT_MEMORY_SIZE,REPEAT_MEMORY_SIZE,AB_DEP,
            BE_REF,mesh,material,
            texture,sprite,sprite,
            shader,animation_clip,audio_clip
        };
        public static float[] titles_width = new float[]
        {
            240, 80 ,160,
            120,100,80,
            80,80,80,
            80,80,80,
            80,80,80
        };


        public static void CreateReport(string directory_path)
        {
            List<EAssetBundleFileInfo> assetbundle_files = AssetBundleAnalyzeManager.FindAssetBundleFiles();
            assetbundle_files.Sort(SortAsset);


            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0},{1},{2},{12}," +
                                        "{3},{4},{5}," +
                                        "{6},{7},{8}," +
                                        "{9},{10},{11}",
                ASSETBUNDLE_NAME, AB_MEMORY_SIZE, CAL_MEMORY_SIZE,
                AB_DEP, BE_REF, mesh,
                material, texture, shader,
                sprite, animation_clip, audio_clip, REPEAT_MEMORY_SIZE));

            int length = assetbundle_files.Count;
            for (int i = 0; i < length; i++)
            {

                EAssetBundleFileInfo info = assetbundle_files[i];

                int tRepeatMemSize = (int) info.GetRepeatMemSize();
                int tAbSize = (int) info.file_ab_memory_size;
                int tAbMemSize = (int) (info.GetMemorySize());
                sb.AppendLine(string.Format("{0},{1},{2},{12}," +
                                    "{3},{4},{5}," +
                                    "{6},{7},{8}," +
                                    "{9},{10},{11}",
            info.ab_name, tAbSize, tAbMemSize,
            info.all_depends.Count, info.FindRedundance(), info.GetAssetCount(E_AssetType.mesh),
            info.GetAssetCount(E_AssetType.material), info.GetAssetCount(E_AssetType.texture), info.GetAssetCount(E_AssetType.shader),
            info.GetAssetCount(E_AssetType.sprite), info.GetAssetCount(E_AssetType.animation_clip), info.GetAssetCount(E_AssetType.audio_clip), tRepeatMemSize));


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
