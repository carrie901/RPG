#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseLoader : I_ResourceLoad
    {
        public static AssetDatabaseLoader instance = new AssetDatabaseLoader();
        public const string EVN = "Assets/";//"Assets/";


        #region I_ResourceLoad

        public I_ObjectInfo LoadAsset<T>(string resPath) where T : Object
        {
            T obj = AssetDatabase.LoadAssetAtPath<T>(EVN + resPath);

            ResLog.Assert(obj != null, "AssetDatabaseLoader 加载失败:[{0}]", resPath);
            I_ObjectInfo info = new ResInfo(obj, resPath);
            return info;
        }

        public ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object
        {
            AssetDatabaseAsynLoadOpertion asynLocal = new AssetDatabaseAsynLoadOpertion(EVN + resPath);
            return asynLocal;
        }

        public bool UnloadAll()
        {
            return true;
        }

        public bool UnloadAsset(I_ObjectInfo objectInfo)
        {
            //Object obj = objectInfo.GetAsset<Object>();
            //if (obj == null) return false;
            //Resources.UnloadAsset(obj);
            return true;
        }

        public bool UnLoadAssetRef(I_ObjectInfo objectInfo)
        {
            return false;
        }

        public void OnUpdate()
        {

        }

        public string GetResPath(string resPath)
        {
            return resPath;
        }

        public void CheckInfo()
        {

        }
        #endregion

        #region private

        #endregion
    }
}
#endif
