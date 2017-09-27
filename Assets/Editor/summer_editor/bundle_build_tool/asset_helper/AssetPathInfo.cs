using System.Collections.Generic;

namespace SummerEditor
{
    public class AssetPathInfo
    {
        public static Dictionary<string, AssetPathInfo> _dict_pathinfo = new Dictionary<string, AssetPathInfo>();
        public string Path = "Unknown";
        // Index Of BundleImportData
        public int Index = -1;

        public static AssetPathInfo CreatePathInfo(string asset_path)
        {
            AssetPathInfo path_info = null;
            if (!_dict_pathinfo.TryGetValue(asset_path, out path_info))
            {
                path_info = new AssetPathInfo();
                path_info.Path = asset_path;
                _dict_pathinfo.Add(asset_path, path_info);
            }
            return path_info;
        }
    }
}
