using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text;
using SummerEditor;

namespace ResourceFormat
{
    public class ModelInfo
    {
        public string Path = "Unknown";
        public int MemSize;
        public string MemSizeT;

        public bool ReadWriteEnable;
        public bool OptimizeMesh;
        public bool ImportMaterials;
        public bool ImportAnimation;
        public ModelImporterMeshCompression MeshCompression = ModelImporterMeshCompression.Off;
        public bool _bHasUv;
        public bool _bHasUv2;
        public bool _bHasUv3;
        public bool _bHasUv4;
        public bool _bHasColor;
        public bool _bHasNormal;
        public bool _bHasTangent;
        public int _vertexCount;
        public int _triangleCount;

        public int GetMeshDataId()
        {
            int meshDataIndex = -1;
            meshDataIndex = _WorkData(0, _bHasUv);
            meshDataIndex = _WorkData(0, _bHasUv2);
            meshDataIndex = _WorkData(meshDataIndex, _bHasUv3);
            meshDataIndex = _WorkData(meshDataIndex, _bHasUv4);
            meshDataIndex = _WorkData(meshDataIndex, _bHasColor);
            meshDataIndex = _WorkData(meshDataIndex, _bHasNormal);
            meshDataIndex = _WorkData(meshDataIndex, _bHasTangent);

            return meshDataIndex;
        }

        public int GetVertexRangeId()
        {
            return _vertexCount / AssetFormatConst.VertexCountMod;
        }

        public string GetVertexRangeStr()
        {
            int index = GetVertexRangeId();
            return string.Format("{0}-{1}",
                index * AssetFormatConst.VertexCountMod,
                (index + 1) * AssetFormatConst.VertexCountMod - 1);
        }

        public int GetTriangleRangeId()
        {
            return _triangleCount / AssetFormatConst.TriangleCountMod;
        }

        public string GetTriangleRangeStr()
        {
            int index = GetTriangleRangeId();
            return string.Format("{0}-{1}",
                index * AssetFormatConst.TriangleCountMod,
                (index + 1) * AssetFormatConst.TriangleCountMod - 1);
        }

        public static string GetMeshDataStr(int key)
        {
            bool[] bData = new bool[7];
            for (int i = 0; i < 7; ++i)
            {
                bData[i] = ((key >> i) & 1) > 0;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("vertices");
            for (int i = 6; i >= 0; --i)
            {
                if (bData[i])
                {
                    sb.Append("," + AssetFormatConst.MeshDataStr[i]);
                }
            }

            return sb.ToString();
        }

        private static int _WorkData(int data, bool flag)
        {
            if (flag)
                return (data << 1) | 1;
            else
                return data << 1;
        }

        public static ModelInfo CreateModelInfo(string assetPath)
        {
            if (!EPathHelper.IsModel(assetPath))
            {
                return null;
            }

            ModelInfo tInfo;
            if (!m_dict_model_info.TryGetValue(assetPath, out tInfo))
            {
                tInfo = new ModelInfo();
                m_dict_model_info.Add(assetPath, tInfo);
            }
            ModelImporter tImport = AssetImporter.GetAtPath(assetPath) as ModelImporter;
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);

            if (tImport == null || mesh == null)
                return null;

            tInfo.Path = assetPath;
            tInfo.ReadWriteEnable = tImport.isReadable;
            tInfo.OptimizeMesh = tImport.optimizeMesh;
            tInfo.ImportMaterials = tImport.importMaterials;
            tInfo.ImportAnimation = tImport.importAnimation;
            tInfo.MeshCompression = tImport.meshCompression;

            tInfo._bHasUv = mesh.uv != null && mesh.uv.Length != 0;
            tInfo._bHasUv2 = mesh.uv2 != null && mesh.uv2.Length != 0;
            tInfo._bHasUv3 = mesh.uv3 != null && mesh.uv3.Length != 0;
            tInfo._bHasUv4 = mesh.uv4 != null && mesh.uv4.Length != 0;
            tInfo._bHasColor = mesh.colors != null && mesh.colors.Length != 0;
            tInfo._bHasNormal = mesh.normals != null && mesh.normals.Length != 0;
            tInfo._bHasTangent = mesh.tangents != null && mesh.tangents.Length != 0;
            tInfo._vertexCount = mesh.vertexCount;
            tInfo._triangleCount = mesh.triangles.Length / 3;

            tInfo.MemSize = 0;
            tInfo.MemSizeT = "等待计算";

            if (m_loadCount % 256 == 0)
            {
                Resources.UnloadUnusedAssets();
            }

            return tInfo;
        }

        public static List<ModelInfo> GetModelInfoByDirectory(string dir)
        {
            List<ModelInfo> modelInfoList = new List<ModelInfo>();
            List<string> list = EPathHelper.GetAssetsPath(dir, true);
            int length = list.Count;
            for (int i = 0; i < length; ++i)
            {
                string assetPath = list[i];
                ModelInfo modelInfo = CreateModelInfo(assetPath);
                if (modelInfo != null)
                {
                    modelInfoList.Add(modelInfo);
                }
            }

            return modelInfoList;
        }

        private static int m_loadCount = 0;
        private static readonly Dictionary<string, ModelInfo> m_dict_model_info = new Dictionary<string, ModelInfo>();
    }
}
