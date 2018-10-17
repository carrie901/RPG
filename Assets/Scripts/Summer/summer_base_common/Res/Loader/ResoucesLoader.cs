using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class ResoucesLoader : I_ResourceLoad
    {
        public static ResoucesLoader instance = new ResoucesLoader();

        #region I_ResourceLoad

        public I_ObjectInfo LoadAsset<T>(string resPath) where T : Object
        {
            Object obj = Resources.Load(resPath);
            ResLog.Assert(obj != null, "ResoucesLoader 加载失败:[{0}]", resPath);
            I_ObjectInfo info = new ResInfo(obj, resPath);
            return info;
        }

        public ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object
        {
            ResAsynLoadOpertion resOpertion = new ResAsynLoadOpertion(resPath);
            return resOpertion;
        }

        public bool UnloadAsset(I_ObjectInfo objectInfo)
        {
            /*if (objectInfo == null) return false;
            Object obj = objectInfo.GetAsset<Object>();
            if (obj == null) return false;
            Resources.UnloadAsset(obj);
            return true;*/
            return true;
        }
        public bool UnLoadAssetRef(I_ObjectInfo objectInfo)
        {
            return false;
        }
        public void OnUpdate() { }

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
