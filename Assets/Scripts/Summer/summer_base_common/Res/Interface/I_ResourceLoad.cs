
using Object = UnityEngine.Object;
namespace Summer
{
    #region 资源加载

    public interface I_ResourceLoad
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        AssetInfo LoadAsset<T>(string resPath) where T : Object;
        /// <summary>
        /// 异步加载
        /// </summary>
        ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object;

        bool UnloadAssetBundle(AssetInfo assetInfo);

        void OnUpdate();

        void CheckInfo();

    }

    #endregion
}

