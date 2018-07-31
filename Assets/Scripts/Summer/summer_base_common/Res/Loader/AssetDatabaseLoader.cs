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

        public AssetInfo LoadAsset(string path)
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(EVN + path);

            ResLog.Assert(obj != null, "AssetDatabaseLoader 加载失败:[{0}]", path);
            AssetInfo info = new AssetInfo(obj, path);
            return info;
        }

        public void LoadSyncChildRes(string res_path) { }

        public LoadOpertion LoadAssetAsync(string path)
        {
            AssetDatabaseAsynLoadOpertion asyn_local = new AssetDatabaseAsynLoadOpertion(EVN + path);
            return asyn_local;
        }

        public bool UnloadAll()
        {
            return true;
        }

        public bool UnloadAssetBundle(AssetInfo asset_info)
        {
            Object obj = asset_info.GetAsset<Object>();
            if (obj == null) return false;
            Resources.UnloadAsset(obj);
            return true;
        }

        public bool UnLoadChildRes(AssetInfo asset_info)
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
