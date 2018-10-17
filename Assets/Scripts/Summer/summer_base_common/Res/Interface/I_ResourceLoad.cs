
using Object = UnityEngine.Object;
namespace Summer
{
    #region 资源加载

    public interface I_ResourceLoad
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        I_ObjectInfo LoadAsset<T>(string resPath) where T : Object;
        /// <summary>
        /// 异步加载
        /// </summary>
        ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object;
        /// <summary>
        /// 卸载内部资源
        /// </summary>
        bool UnloadAsset(I_ObjectInfo objectInfo);
        /// <summary>
        /// 卸载引用
        /// </summary>
        bool UnLoadAssetRef(I_ObjectInfo objectInfo);
        /// <summary>
        /// Update
        /// </summary>
        void OnUpdate();
        /// <summary>
        /// 资源路径转换
        /// </summary>
        /// <param name="resPath"></param>
        /// <returns></returns>
        string GetResPath(string resPath);
        /// <summary>
        /// 测试方法
        /// </summary>
        void CheckInfo();

    }

    #endregion
}

