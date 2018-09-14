using System.Collections.Generic;


namespace SummerEditor
{
    /// <summary>
    /// AssetBundle文件信息,用来做AssetBundle的报告 冗余数以及相关自己的大小内存等等
    /// </summary>
    public class EAssetBundleFileInfo
    {
        #region 属性

        public string AbName { get; set; }                                              // 名称（不会重名）
        public string FilePath { get; set; }                                            // file下的完整文件路径
        public long FileAbMemorySize { get; set; }                                      // Ab的内存大小
        public List<string> _allDepends = new List<string>();                           // 所有依赖的AssetBundle列表
        public List<string> _beDepends = new List<string>();                            // 所有被依赖的AssetBundle列表                                             // 是主包资源
        public List<EAssetFileInfo> _depAssetFiles = new List<EAssetFileInfo>();        // 包含的资源名称

        #endregion

        #region 构造

        public EAssetBundleFileInfo(string tmpAbName)
        {
            AbName = tmpAbName;
            FilePath = EAssetBundleConst.assetbundle_directory + "/" + AbName;//Path.Combine(, ab_name);
            FileAbMemorySize = EPathHelper.GetFileSize(FilePath);
            _allDepends.Clear();
            _beDepends.Clear();
            _depAssetFiles.Clear();
        }

        #endregion

        #region public

        public float GetMemorySize()
        {
            float allMemory = 0;
            int length = _depAssetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                allMemory += _depAssetFiles[i].GetMemorySize();
            }
            return allMemory;
        }

        public float GetRepeatMemSize()
        {
            float allMemory = 0;
            int length = _depAssetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                if (_depAssetFiles[i]._includedBundles.Count > 1)
                {
                    allMemory += _depAssetFiles[i].GetMemorySize();
                }
            }
            return allMemory;
        }

        /// <summary>
        /// 获取相同类型的资产数量
        /// </summary>
        public int GetAssetCount(E_AssetType assetType)
        {
            int count = 0;
            int length = _depAssetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                var info = _depAssetFiles[i];
                if (info._assetType == assetType)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 是否包含指定资产
        /// </summary>
        public bool IsAssetContain(long guid)
        {
            int length = _depAssetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                var info = _depAssetFiles[i];
                if (info._guid == guid)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 冗余数
        /// </summary>
        public int FindRedundance()
        {
            int count = 0;
            int length = _depAssetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                if (_depAssetFiles[i]._includedBundles.Count > 1)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// 得到某一类型的子资源列表
        /// </summary>
        public void FindAssetFiles(List<EAssetFileInfo> assets, E_AssetType assetType)
        {
            assets.Clear();

            int length = _depAssetFiles.Count;
            for (int i = 0; i < length; i++)
            {
                var info = _depAssetFiles[i];
                if (info._assetType == assetType)
                {
                    assets.Add(info);
                }
            }
        }

        /// <summary>
        /// 增加子依赖
        /// </summary>
        public void AddDepAssetFile(EAssetFileInfo assetFile)
        {
            _depAssetFiles.Add(assetFile);
        }

        public override string ToString()
        {
            return AbName;
        }

        #endregion
    }
}


