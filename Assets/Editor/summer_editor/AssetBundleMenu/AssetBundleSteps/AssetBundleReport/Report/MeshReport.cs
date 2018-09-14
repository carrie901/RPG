
using System;
using System.Collections.Generic;
using System.Text;
using Summer;

namespace SummerEditor
{
    public class MeshReport
    {
        public const string MESH_NAME = "网格.csv";

        public static void CreateReport(string directoryPath)
        {
            List<EAssetFileInfo> assetFiles = new List<EAssetFileInfo>(AssetBundleAnalyzeManager.FindAssetFiles().Values);
            assetFiles.Sort(SortAsset);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0},{1},{2}," +
                                       "{3},{4},{5}",
               "网格名字", "顶点数", "面数",
               "子网格数", "网格压缩", "Read/Write"));

            int length = assetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                EAssetFileInfo assetFileInfo = assetFiles[i];
                if (assetFileInfo._assetType != E_AssetType.MESH) continue;
                List<KeyValuePair<string, Object>> values = assetFileInfo._propertys;

                sb.Append(string.Format("{0},{1},{2}," +
                                        "{3},{4},{5}",
                assetFileInfo._assetName, values[0].Value, values[1].Value,
                values[2].Value, values[3].Value, values[4].Value));

                int refCount = assetFileInfo._includedBundles.Count;
                for (int j = 0; j < refCount; j++)
                    sb.Append("," + assetFileInfo._includedBundles[j].AbName);
                sb.AppendLine();
            }

            FileHelper.WriteTxtByFile(directoryPath + "/" + MESH_NAME, sb.ToString());
        }

        public static int SortAsset(EAssetFileInfo a, EAssetFileInfo b)
        {
            return String.CompareOrdinal(a._assetName, b._assetName);
        }
    }

    public class MeshReportInfo
    {
        public string MeshName;
        public int Vertices;
        public int Triangles;
        public string SubMeshCount;
        public string MeshCompression;
        public bool Rw;
        public int BeRefCount;
        public List<string> BeRefs = new List<string>();

        public void SetInfo(List<string> content)
        {
            MeshName = content[0];
            Vertices = int.Parse(content[1]);
            Triangles = int.Parse(content[2]);
            SubMeshCount = /*int.Parse*/(content[3]);
            MeshCompression = (content[4]);
            Rw = bool.Parse(content[5]);
            BeRefCount = content.Count - 6;

            BeRefs.Clear();
            for (int i = 6; i < content.Count; i++)
            {
                BeRefs.Add(content[i]);
            }
        }
    }


}
