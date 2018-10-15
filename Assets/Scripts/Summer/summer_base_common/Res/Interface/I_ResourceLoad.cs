
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
        /// <summary>
        /// 卸载内部资源
        /// </summary>
        bool UnloadAssetBundle(AssetInfo assetInfo);
        /// <summary>
        /// Update
        /// </summary>
        void OnUpdate();
        /// <summary>
        /// 测试方法
        /// </summary>
        void CheckInfo();

    }

    #endregion
}

