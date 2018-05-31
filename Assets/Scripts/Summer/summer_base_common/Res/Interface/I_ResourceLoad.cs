
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

        /// <summary>
        /// 处于加载中
        /// </summary>
        bool HasInLoading(string res_path);

        bool UnloadAssetBundle(string res_path);

        void OnUpdate();
    }

    #endregion
}

