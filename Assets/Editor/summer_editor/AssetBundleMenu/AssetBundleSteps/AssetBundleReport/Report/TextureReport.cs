
using System;
using System.Collections.Generic;
using System.Text;
using Summer;
namespace SummerEditor
{
    public class TextureReport
    {
        public const string REPORT_NAME = "纹理.csv";
        public const string TEXTURE_NAME = "资源名称";
        public const string WIDTH = "宽度 Width";
        public const string HEIGHT = "高度 Height";
        public const string FORMAT = "格式 Format";
        public const string MIP_MAP = "MipMap功能";
        public const string READ_WRITE = "Read/Write";
        public const string SIZE = "内存占用";
        public const string AB_COUNT = "冗余数量";
        public const string AB_FILES = "相应的AB文件";
        public static void CreateReport(string directoryPath)
        {
            List<EAssetFileInfo> assetFiles = new List<EAssetFileInfo>(AssetBundleAnalyzeManager.FindAssetFiles().Values);
            assetFiles.Sort(SortAsset);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0},{1},{2}," +
                                       "{3},{4},{5}," +
                                       "{6},{7},{8}",
               TEXTURE_NAME, WIDTH, HEIGHT,
               FORMAT, MIP_MAP, READ_WRITE,
               SIZE, AB_COUNT, AB_FILES));

            int length = assetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                EAssetFileInfo assetFileInfo = assetFiles[i];
                if (assetFileInfo._assetType != E_AssetType.TEXTURE) continue;
                List<KeyValuePair<string, Object>> values = assetFileInfo._propertys;

                int memSize = (int)values[5].Value;
                sb.Append(string.Format("{0},{1},{2}," +
                                        "{3},{4},{5}," +
                                        "{6},{7}",
                assetFileInfo._assetName, values[0].Value, values[1].Value,
                values[2].Value, values[3].Value, values[4].Value,
                memSize, assetFileInfo._includedBundles.Count));

                int refCount = assetFileInfo._includedBundles.Count;
                for (int j = 0; j < refCount; j++)
                    sb.Append("," + assetFileInfo._includedBundles[j].AbName);
                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directoryPath + "/" + REPORT_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetFileInfo a, EAssetFileInfo b)
        {
            return String.CompareOrdinal(a._assetName, b._assetName);
        }
    }

    public class TextureReportInfo
    {
        public string TextureName;
        public int Width;
        public int Height;
        public string Format;
        public bool MipMap;
        public bool ReadWrite;
        public int MemSize;
        public int BeRefCount;
        public List<string> BeRefs = new List<string>();
        public void SetInfo(List<string> content)
        {
            TextureName = content[0];
            Width = int.Parse(content[1]);
            Height = int.Parse(content[2]);
            Format = content[3];

            MipMap = bool.Parse(content[4]);
            ReadWrite = bool.Parse(content[5]);

            MemSize = int.Parse(content[6]);
            BeRefCount = int.Parse(content[7]);
            BeRefs.Clear();
            for (int i = 7; i < content.Count; i++)
            {
                BeRefs.Add(content[i]);
            }
        }
    }
}
