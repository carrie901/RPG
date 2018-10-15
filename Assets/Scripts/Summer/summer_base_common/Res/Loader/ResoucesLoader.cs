using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class ResoucesLoader : I_ResourceLoad
    {
        public static ResoucesLoader instance = new ResoucesLoader();

        #region I_ResourceLoad

        public AssetInfo LoadAsset<T>(string resPath) where T : Object
        {
            Object obj = Resources.Load(resPath);
            ResLog.Assert(obj != null, "ResoucesLoader 加载失败:[{0}]", resPath);
            AssetInfo info = new AssetInfo(obj, resPath);
            return info;
        }

        public ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object
        {
            ResAsynLoadOpertion resOpertion = new ResAsynLoadOpertion(resPath);
            return resOpertion;
        }

        public bool UnloadAssetBundle(AssetInfo assetInfo)
        {
            if (assetInfo == null) return false;
            Object obj = assetInfo.GetAsset<Object>();
            if (obj == null) return false;
            Resources.UnloadAsset(obj);
            return true;
        }

        public void OnUpdate()
        {

        }
        public void CheckInfo()
        {

        }
        #endregion

        #region private 

        #endregion
    }
}
