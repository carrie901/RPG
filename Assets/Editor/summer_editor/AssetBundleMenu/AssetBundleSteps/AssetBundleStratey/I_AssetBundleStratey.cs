using System;
using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 打包策略
    /// </summary>
    public interface I_AssetBundleStratey
    {

        bool IsBundleStratey(EAssetObjectInfo info);

        void AddAssetBundleFileInfo(EAssetObjectInfo info);

        void SetAssetBundleName();
    }
}
