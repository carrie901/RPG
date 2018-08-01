using UnityEngine;
using UnityEditor;

namespace SummerEditor
{
    public class ModelAssetPostprocessor : AssetPostprocessor
    {
        /*public static Material s_material;
        public static string asset_material_path;

        public void OnPreprocessModel()
        {
            string asset_path = this.assetPath;
            ModelImporter model_importer = assetImporter as ModelImporter;
            model_importer.importMaterials = false;
        }

        //模型导入之前调用  
        public void OnPostprocessModel(GameObject model)
        {
        }

        Material OnAssignMaterialModel(Material material, Renderer renderer)
        {
            InitMaterial();
            return s_material;
        }

        public static void InitMaterial()
        {
            if (s_material != null) return;
            s_material = AssetDatabase.LoadAssetAtPath<Material>(asset_material_path);
            Debug.Log("初始化材质球");
        }*/
    }
}

