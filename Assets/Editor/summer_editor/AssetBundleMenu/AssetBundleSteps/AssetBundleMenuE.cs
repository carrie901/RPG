
using System.Text;
using UnityEditor;
namespace SummerEditor
{
    public class AssetBundleMenuE
    {
        [MenuItem("Tools/AssetBundle/Build/生成资源配置列表")]
        public static void CreateAbConfigFile()
        {
            CreateAssetBundleConfigE.CreateAbConfigFile();
        }

        [MenuItem("Tools/AssetBundle/Build/开始分析")]
        public static void AllAnalysisAsset()
        {
            AssetBundleAnalysisE.AllAnalysisAsset();
            //AssetAnalysisE.AllAnalysisAsset();
        }

        [MenuItem("Tools/AssetBundle/Build/打包策略")]
        public static void BuildStrategy()
        {
        }

        [MenuItem("Tools/AssetBundle/Build/测试 设置AssetBundle Name")]
        public static void SetAllAssetBundleName()
        {
            string[] all_ab_names = AssetDatabase.GetAllAssetBundleNames();
            UnityEngine.Debug.Log("设置名字之前all_ab_names:" + all_ab_names.Length);
            AssetBundleSetNameE.SetMainAbName();
        }

        [MenuItem("Tools/AssetBundle/Build/清除所有AssetBundle 名字")]
        public static void ClearAssetBundleName()
        {
            AssetBundleSetNameE.ClearAllAssetBundleName();
        }

        [MenuItem("Assets/AssetBundle/Build/设置AssetBundle 名字")]
        public static void SetAssetBundleName()
        {
            AssetBundleSetNameE.SetSelectionAssetBundleName();
        }

        [MenuItem("Tools/AssetBundle/Build/打包资源")]
        public static void BuildAssetBuild()
        {
            AssetBundleBuildE.AllAssetBundleBuild();
        }

        [MenuItem("Tools/AssetBundle/检测/调用内置Shader情况")]
        public static void AllCheckShader()
        {
            CheckAssetShaderE.AllCheckShader();
        }

        [MenuItem("Assets/AssetBundle/检测/调用内置Shader情况")]
        public static void CheckShader()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string path = AssetDatabase.GetAssetPath(id);
                CheckAssetShaderE.CheckShader(path);
            }

        }

        [MenuItem("Assets/AssetBundle/查看Dep")]
        public static void TestDep()
        {
            foreach (var id in Selection.instanceIDs)
            {
                string path = AssetDatabase.GetAssetPath(id);
                UnityEngine.Object go = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);
                string[] dep = AssetDatabase.GetDependencies(path, false);
                string[] dep1 = AssetDatabase.GetDependencies(path, true);
                string[] dep2 = AssetDatabase.GetDependencies(path);
                Dep(path);
            }
        }

        public static void Dep(string path)
        {
            string[] deps = AssetDatabase.GetDependencies(path, false);
            int index = 1;
            for (int i = 0; i < deps.Length; i++)
            {
                UnityEngine.Debug.Log(deps[i]);
                index = GetDep(index, deps[i]);
            }
        }

        public static int GetDep(int tab, string path)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < tab; i++)
                sb.Append("\t");
            string[] dep = AssetDatabase.GetDependencies(path, false);
            for (int i = 0; i < dep.Length; i++)
            {
                UnityEngine.Debug.Log(sb + dep[i]);
                GetDep(tab + 1, dep[i]);
            }



            return tab;
        }

    }
}
