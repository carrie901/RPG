using System.Collections.Generic;
using UnityEngine;

namespace SummerEditor
{

    /// <summary>
    /// 资产名称（有可能重名）
    /// </summary>
    public class EAssetFileInfo
    {
        #region 属性

        public string asset_name;                                                           // 资产名称（有可能重名）
        public long guid;                                                                   // 唯一ID 需要取得 PathID 才能确保唯一性
        public E_AssetType asset_type;                                                      // 资源类型
        public List<KeyValuePair<string, System.Object>> propertys                          // 属性
            = new List<KeyValuePair<string, System.Object>>();
        public List<EAssetBundleFileInfo> included_bundles                                  // 属于哪一个AssetBundle资源，就是爸爸是谁
            = new List<EAssetBundleFileInfo>();

        public bool InitAsset { get; set; }

        #endregion

        #region 构造

        public EAssetFileInfo(long uid)
        {
            guid = uid;
            InitAsset = false;
        }

        #endregion

        #region public

        public override string ToString()
        {
            return asset_name;
        }

        #endregion
    }

}
