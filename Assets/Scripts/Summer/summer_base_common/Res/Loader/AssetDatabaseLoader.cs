#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseLoader : I_ResourceLoad
    {
        public static AssetDatabaseLoader instance = new AssetDatabaseLoader();
        public const string EVN = "Assets/";


        #region I_ResourceLoad

        public AssetInfo LoadAsset(string resPath)
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(EVN + resPath);

            ResLog.Assert(obj != null, "AssetDatabaseLoader 加载失败:[{0}]", resPath);
            AssetInfo info = new AssetInfo(obj, resPath);
            return info;
        }

        public void LoadSyncChildRes(string resPath) { }

        public LoadOpertion LoadAssetAsync(string resPath)
        {
            AssetDatabaseAsynLoadOpertion asynLocal = new AssetDatabaseAsynLoadOpertion(EVN + resPath);
            return asynLocal;
        }

        public bool UnloadAll()
        {
            return true;
        }

        public bool UnloadAssetBundle(AssetInfo assetInfo)
        {
            Object obj = assetInfo.GetAsset<Object>();
            if (obj == null) return false;
            Resources.UnloadAsset(obj);
            return true;
        }

        public bool UnLoadChildRes(AssetInfo assetInfo)
        {
            return true;
        }

        public void OnUpdate()
        {

        }

        #endregion

        #region private

        #endregion
    }
}
#endif
