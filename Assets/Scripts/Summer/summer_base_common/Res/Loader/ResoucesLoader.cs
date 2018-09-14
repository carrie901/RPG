using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class ResoucesLoader : I_ResourceLoad
    {
        public static ResoucesLoader instance = new ResoucesLoader();

        public Dictionary<string, int> _loading =
            new Dictionary<string, int>();

        #region I_ResourceLoad

        public AssetInfo LoadAsset(string resPath)
        {
            Object obj = Resources.Load(resPath);
            ResLog.Assert(obj != null, "ResoucesLoader 加载失败:[{0}]", resPath);
            AssetInfo info = new AssetInfo(obj, resPath);
            return info;
        }

        public void LoadSyncChildRes(string resPath) { }

        public LoadOpertion LoadAssetAsync(string resPath)
        {
            ResAsynLoadOpertion resOpertion = new ResAsynLoadOpertion(resPath);
            return resOpertion;
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
