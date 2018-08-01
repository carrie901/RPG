
namespace Summer
{
    #region 资源加载

    public interface I_ResourceLoad
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        AssetInfo LoadAsset(string res_path);
        /// <summary>
        /// 异步加载
        /// </summary>
        LoadOpertion LoadAssetAsync(string res_path);

        bool UnloadAssetBundle(AssetInfo asset_info);

        void OnUpdate();
    }

    #endregion
}

