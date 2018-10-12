using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// AssetBundle包信息
    /// 这个AssetBundle
    ///     1.实际加载路径
    ///     2.相对路径
    ///     3.AssetBundle 资源
    ///     4.这个AssetBundle下的所有的AssetInfo
    /// </summary>
    public class AssetBundlePackageCnf
    {
        public static string _fullPath = Application.streamingAssetsPath + "/rpg/";

        #region 属性
        /// <summary>
        /// AssetBundel包的绝对路径 
        /// </summary>
        public string FullPath { get; private set; }
        /// <summary>
        /// AssetBundel包的相对路径
        /// </summary>
        public string PackagePath { get; private set; }                                                                                              // 包的路径
        /// <summary>
        /// 资源的实际路径 Key=资源相对路径，value=资源的名称
        /// 例子:
        ///     key = Raw/Animation/Zhaoyun/climb_down,
        ///     value = climb_down
        /// </summary>
        public Dictionary<string, string> _resPathMap = new Dictionary<string, string>();
        /// <summary>
        /// 资源的名称 key的资源的名称，Value=资源的路径
        /// 例子:
        ///     key = climb_down
        ///     value = Raw/Animation/Zhaoyun/climb_down
        /// </summary>
        public Dictionary<string, string> _resNames = new Dictionary<string, string>();

        #region 引用信息

        #endregion

        #endregion

        #region 构造

        public AssetBundlePackageCnf(string[] infos)
        {
            PackagePath = infos[0];
            string.Intern(PackagePath);

            // TODO 可以优化 这块需要考虑到远程加载的问题 是加载本地的还是更新文件
            FullPath = _fullPath + PackagePath;
            string.Intern(FullPath);

            for (int i = 1; i < infos.Length; i = i + 2)
            {
                bool result = _resPathMap.ContainsKey(infos[i]);
                LogManager.Assert(!result, "初始化AssetBundle的依赖信息失败，[{0}]", infos[i]);
                if (result) continue;

                _resPathMap.Add(infos[i], infos[i + 1]);
                _resNames.Add(infos[i + 1], infos[i]);
            }
        }

        #endregion
    }
}

