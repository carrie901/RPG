using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 检测资源是否使用了内置Shader
    /// </summary>
    public class CheckAssetShaderE
    {
        public static void AllCheckShader()
        {
            List<string> assets_path = EPathHelper.GetAssetPathList01(EAssetBundleConst.main_driectory, true);

            int length = assets_path.Count;
            for (int i = 0; i < length; i++)
            {
                CheckShader(assets_path[i]);
                EditorUtility.DisplayProgressBar("检测Shader", assets_path[i], (float)(i + 1) / length);
            }
            EditorUtility.ClearProgressBar();
        }

        public static void CheckShader(string path)
        {
            Object go = AssetDatabase.LoadAssetAtPath<Object>(path);
            _check_asset(go);
        }

        public static void _check_asset(Object unity_ob)
        {
            UnityEngine.Object[] ogs = EditorUtility.CollectDependencies(new UnityEngine.Object[] { unity_ob });
            for (int i = 0; i < ogs.Length; i++)
            {
                string ogs_asset_path = AssetDatabase.GetAssetPath(ogs[i]);
                //Debug.Log("Asset Name:" + ogs[i].name + "           Asset Path:" + ogs_asset_path);
                if (ogs[i] is Shader)
                {
                    string ogs_path = AssetDatabase.GetAssetPath(ogs[i]);
                    if (ogs_path.Contains("Resources"))
                        Debug.Log("Path:" + ogs_path);
                }
            }
        }
    }
}
