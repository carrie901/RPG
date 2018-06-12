
namespace Summer
{
    #region 资源加载

    public interface I_ResourceLoad
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        AssetInfo LoadAsset<T>(string res_path) where T : UnityEngine.Object;

        /// <summary>
        /// 异步加载
        /// </summary>
        LoadOpertion LoadAssetAsync<T>(string res_path) where T : UnityEngine.Object;

        /// <summary>
        /// 处于加载中
        /// </summary>
        bool HasInLoading(string res_path);

        bool UnloadAssetBundle(string res_path);

        void OnUpdate();
    }

    #endregion
}

