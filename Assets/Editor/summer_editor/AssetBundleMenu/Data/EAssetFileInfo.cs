using System;
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

        public string _assetName;                                                           // 资产名称（有可能重名）
        public long _guid;                                                                  // 唯一ID 需要取得 PathID 才能确保唯一性
        public E_AssetType _assetType;                                                      // 资源类型
        public float _memorysize;
        public List<KeyValuePair<string, System.Object>> _propertys                         // 属性
            = new List<KeyValuePair<string, System.Object>>();
        public List<EAssetBundleFileInfo> _includedBundles                                  // 属于哪一个AssetBundle资源，就是爸爸是谁
            = new List<EAssetBundleFileInfo>();

        public bool _inBuilt;

        public bool InitAsset { get; set; }

        #endregion

        #region 构造

        public EAssetFileInfo(long uid)
        {
            _guid = uid;
            InitAsset = false;
        }

        #endregion

        #region public

        public float GetMemorySize()
        {
            if (_assetType == E_AssetType.TEXTURE)
            {
                //float memorysize_texture = 0;
                List<KeyValuePair<string, System.Object>> values = _propertys;
                //float memorysize_texture = (float)values[5].Value;
                /*try
                {
                    memorysize_texture = (float)propertys[5].Value;
                    Debug.Log("memorysize:" + memorysize + "_" + memorysize_texture);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError("--------------asset_name:" + asset_name);
                }*/
                //return 0;
                //return memorysize_texture;
                return _memorysize;
            }
            else
            {
                return _memorysize;
            }
        }

        public override string ToString()
        {
            return _assetName;
        }

        #endregion
    }

}
