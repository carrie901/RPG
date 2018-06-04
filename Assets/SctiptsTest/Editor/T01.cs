using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class T01 : MonoBehaviour
{

    [MenuItem("MyMenu/AtlasMaker")]
    static private void MakeAtlas()
    {
        string sprite_dir = Application.dataPath + "/Resources/Sprite";

        if (!Directory.Exists(sprite_dir))
        {
            Directory.CreateDirectory(sprite_dir);
        }

        DirectoryInfo root_dir_info = new DirectoryInfo(Application.dataPath + "/Atlas");
        foreach (DirectoryInfo dirInfo in root_dir_info.GetDirectories())
        {
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
            {
                string all_path = pngFile.FullName;
                string asset_path = all_path.Substring(all_path.IndexOf("Assets"));
                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(asset_path);
                //Sprite sprite = Resources.LoadAssetAtPath<Sprite>(assetPath);
                GameObject go = new GameObject(sprite.name);
                go.AddComponent<SpriteRenderer>().sprite = sprite;
                all_path = sprite_dir + "/" + sprite.name + ".prefab";
                string prefab_path = all_path.Substring(all_path.IndexOf("Assets"));
                PrefabUtility.CreatePrefab(prefab_path, go);
                GameObject.DestroyImmediate(go);
            }
        }
    }

    [MenuItem("MyMenu/Build Assetbundle")]
    static private void BuildAssetBundle()
    {
       

        string dir = Application.dataPath + "/StreamingAssets";

        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        BuildPipeline.BuildAssetBundles(dir + "/AssetBundle", 0, GetBuildTarget());
        /*DirectoryInfo rootDirInfo = new DirectoryInfo(Application.dataPath + "/Atlas");
        foreach (DirectoryInfo dirInfo in rootDirInfo.GetDirectories())
        {
            List<AssetBundleBuild> abbs = new List<AssetBundleBuild>();

            List<Sprite> assets = new List<Sprite>();
            string path = dir + "/" + dirInfo.Name + ".assetbundle";
            foreach (FileInfo pngFile in dirInfo.GetFiles("*.png", SearchOption.AllDirectories))
            {
                string allPath = pngFile.FullName;
                string assetPath = allPath.Substring(allPath.IndexOf("Assets"));

                Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                assets.Add(sprite);
                //assets.Add(Resources.LoadAssetAtPath<Sprite>(assetPath));
            }
            AssetBundleBuild[] t = abbs.ToArray();
            
            //BuildPipeline.BuildAssetBundles(path, t, GetBuildTarget());
            if (BuildPipeline.BuildAssetBundle(null, assets.ToArray(), path, BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions.CollectDependencies, GetBuildTarget()))
            {
            }
        }*/
    }
    static private BuildTarget GetBuildTarget()
    {
        BuildTarget target;
#if UNITY_STANDALONE
        target = BuildTarget.StandaloneWindows;
#elif UNITY_IPHONE
			target = BuildTarget.iPhone;
#elif UNITY_ANDROID
        target = BuildTarget.Android;
#endif
        return target;
    }
}
