using System.Collections.Generic;
using System.IO;
namespace SummerEditor
{
    /// <summary>
    /// AssetBundle文件信息,用来做AssetBundle的报告 冗余数以及相关自己的大小内存等等
    /// </summary>
    public class EAssetBundleFileInfo
    {
        public string ab_name;                                                          // 名称（不会重名）
        public string file_path;                                                        // file下的完整文件路径
        public long file_ab_memory_size;                                                // Ab的内存大小
        public long cal_ab_memory_size;                                                 // 计算ab的内存大小
        public List<string> all_depends = new List<string>();                           // 所有依赖的AssetBundle列表
        public List<string> be_depends = new List<string>();                            // 所有被依赖的AssetBundle列表                                             // 是主包资源
        public List<EAssetFileInfo> dep_asset_files = new List<EAssetFileInfo>();       // 包含的资源名称


        public EAssetBundleFileInfo(string tmp_ab_name)
        {
            ab_name = tmp_ab_name;
            file_path = EAssetBundleConst.assetbundle_directory + "/" + ab_name;//Path.Combine(, ab_name);
            file_ab_memory_size = new FileInfo(file_path).Length;
            all_depends.Clear();
            be_depends.Clear();
        }

        /// <summary>
        /// 获取相同类型的资产数量
        /// </summary>
        public int GetAssetCount(E_AssetType asset_type)
        {
            int count = 0;
            foreach (var info in dep_asset_files)
            {
                if (info.asset_type == asset_type)
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 是否包含指定资产
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public bool IsAssetContain(long guid)
        {
            foreach (var asset in dep_asset_files)
            {
                if (asset.guid == guid)
                {
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return ab_name;
        }

        public int FindRedundance()
        {
            int count = 0;
            for (int i = 0; i < dep_asset_files.Count; i++)
            {
                if (dep_asset_files[i].included_bundles.Count > 1)
                    count++;
            }
            return count;
        }

        public void FindAssetFiles(List<EAssetFileInfo> assets, E_AssetType asset_type)
        {
            assets.Clear();

            foreach (var info in dep_asset_files)
            {
                if (info.asset_type == asset_type)
                {
                    assets.Add(info);
                }
            }
        }
    }
}


