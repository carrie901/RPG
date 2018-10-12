using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Text;
using SummerEditor;

namespace SummerEditor
{
    public class ModelFormatInfo
    {
        #region 属性

        public string Path = "Unknown";
        public int MemSize;

        public bool ReadWriteEnable;                                // 若开启，顶点可被实时修改，建议没有修改需求的模型关闭此选项
        public bool OptimizeMesh;                                   // 优化骨骼结点,可以优化Animatior.Update的耗时
        public bool ImportMaterials;                                // 默认材质球
        public bool ImportAnimation;
        public ModelImporterMeshCompression MeshCompression
            = ModelImporterMeshCompression.Off;
        public bool _bHasUv;
        public bool _bHasUv2;
        public bool _bHasUv3;
        public bool _bHasUv4;
        public bool _bHasColor;
        public bool _bHasNormal;
        public bool _bHasTangent;
        public int _vertexCount;
        public int _triangleCount;

        #endregion

        #region public

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

        #endregion

        #region static function

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

        public static ModelFormatInfo CreateModelInfo(string assetPath)
        {
            if (!EPathHelper.IsModel(assetPath))
            {
                return null;
            }

            ModelFormatInfo formatInfo;
            if (!m_dict_model_info.TryGetValue(assetPath, out formatInfo))
            {
                formatInfo = new ModelFormatInfo();
                m_dict_model_info.Add(assetPath, formatInfo);
            }
            ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);

            if (modelImporter == null || mesh == null)
                return null;

            formatInfo.Path = assetPath;
            formatInfo.ReadWriteEnable = modelImporter.isReadable;
            formatInfo.OptimizeMesh = modelImporter.optimizeMesh;
            formatInfo.ImportMaterials = modelImporter.importMaterials;
            formatInfo.ImportAnimation = modelImporter.importAnimation;
            formatInfo.MeshCompression = modelImporter.meshCompression;

            formatInfo._bHasUv = mesh.uv != null && mesh.uv.Length != 0;
            formatInfo._bHasUv2 = mesh.uv2 != null && mesh.uv2.Length != 0;
            formatInfo._bHasUv3 = mesh.uv3 != null && mesh.uv3.Length != 0;
            formatInfo._bHasUv4 = mesh.uv4 != null && mesh.uv4.Length != 0;
            formatInfo._bHasColor = mesh.colors != null && mesh.colors.Length != 0;
            formatInfo._bHasNormal = mesh.normals != null && mesh.normals.Length != 0;
            formatInfo._bHasTangent = mesh.tangents != null && mesh.tangents.Length != 0;
            formatInfo._vertexCount = mesh.vertexCount;
            formatInfo._triangleCount = mesh.triangles.Length / 3;

            formatInfo.MemSize = 0;

            if (m_loadCount % 256 == 0)
            {
                Resources.UnloadUnusedAssets();
            }

            return formatInfo;
        }

        public static List<ModelFormatInfo> GetModelInfoByDirectory(string dir)
        {
            List<ModelFormatInfo> modelInfoList = new List<ModelFormatInfo>();
            List<string> list = EPathHelper.GetAssetsPath(dir, true);
            int length = list.Count;
            for (int i = 0; i < length; ++i)
            {
                string assetPath = list[i];
                ModelFormatInfo modelFormatInfo = CreateModelInfo(assetPath);
                if (modelFormatInfo != null)
                {
                    modelInfoList.Add(modelFormatInfo);
                }
            }

            return modelInfoList;
        }

        private static int m_loadCount = 0;
        private static readonly Dictionary<string, ModelFormatInfo> m_dict_model_info = new Dictionary<string, ModelFormatInfo>();

        #endregion
    }
}
