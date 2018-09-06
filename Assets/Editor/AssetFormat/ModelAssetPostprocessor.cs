using UnityEngine;
using UnityEditor;

namespace SummerEditor
{
    /*public class ModelAssetPostprocessor : AssetPostprocessor
    {
        public static Material _sMaterial;
        public static string _assetMaterialPath= "Assets/Raw/Materials/DefaultMaterial";

        public void OnPreprocessModel()
        {
            //string asset_path = this.assetPath;
            ModelImporter modelImporter = assetImporter as ModelImporter;
            if (modelImporter == null) return;
            modelImporter.importMaterials = false;
        }

        //模型导入之前调用  
        public void OnPostprocessModel(GameObject model)
        {
        }

        Material OnAssignMaterialModel(Material material, Renderer renderer)
        {
            InitMaterial();
            return _sMaterial;
        }

        public static void InitMaterial()
        {
            if (_sMaterial != null) return;
            _sMaterial = AssetDatabase.LoadAssetAtPath<Material>(_assetMaterialPath);
            Debug.Log("初始化材质球");
        }
    }*/
}

