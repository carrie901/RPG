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

        public AssetInfo LoadAsset(string path)
        {
            Object obj = Resources.Load(path);
            ResLog.Assert(obj != null, "ResoucesLoader 加载失败:[{0}]", path);
            AssetInfo info = new AssetInfo(obj, path);
            return info;
        }

        public void LoadSyncChildRes(string res_path) { }

        public LoadOpertion LoadAssetAsync(string res_path)
        {
            ResAsynLoadOpertion res_opertion = new ResAsynLoadOpertion(res_path);
            return res_opertion;
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
