using System.Collections.Generic;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 单独的资源Asset
    /// </summary>
    public class EAssetObjectInfo
    {
        #region 属性

        public string AssetPath { get { return _assetPath; } }
        protected string _assetPath;                                                                       // 资源名称
        public int RefCount { get { return _refCount; } }
        protected int _refCount;                                                                           // 引用次数
        public bool IsMainAsset { get { return _isMainAsset; } }
        protected bool _isMainAsset;                                                                      // 主依赖资源

        //public float MemSize { get { return _mem_size; } }
        //protected float _mem_size;                                                                                // 内存资源大小
        //public float FileSize { get { return _file_size; } }
        //protected float _file_size;                                                                               // 文件大小        
        //public float TextureCount { get { return _texture_count; } }                                            
        //protected float _texture_count;                                                                           // 引用纹理个数

        //protected Dictionary<string, EAssetObjectInfo> _topDepMap                                           // 顶层依赖资源
        //    = new Dictionary<string, EAssetObjectInfo>();
        /*protected Dictionary<string, EAssetObjectInfo> _all_dep_map                                       // 所有依赖资源
            = new Dictionary<string, EAssetObjectInfo>();*/
        //protected Dictionary<string, EAssetObjectInfo> _beDeps                                              // 所属于的父类
        //    = new Dictionary<string, EAssetObjectInfo>();

        #endregion

        #region 构造

        public EAssetObjectInfo(string assetPath, bool isMainAsset = false)
        {
            _assetPath = assetPath;
            _isMainAsset = isMainAsset;
        }

        #endregion

        #region public

        public void AnalyzeAsset()
        {

            List<string> tmpTopDep = new List<string>();
            EAssetBundleHelper.FindRealDep(_assetPath, tmpTopDep);

            // 添加Asset真真的依赖
            for (int i = tmpTopDep.Count - 1; i >= 0; i--)
            {
                if (tmpTopDep[i].EndsWith(EAssetBundleConst.IGNORE_BUILD_ASSET_SUFFIX)) continue;
                EAssetObjectInfo depAb = EAssetBundleAnalysis.FindAssetObject(tmpTopDep[i]);
                if (depAb == null)
                {
                    Debug.LogError("找不到资源:" + tmpTopDep[i] + "居然没进缓存");
                    continue;
                }
                Debug.AssertFormat(!depAb.IsMainAsset, "主依赖资源[{0}]不能成为依赖资源", depAb.AssetPath);
                //_topDepMap.Add(depAb.AssetPath, depAb);
                depAb.SetParent(this);
            }
        }

        public int BeDepCount()
        {
            return 0;
            //return _beDeps.Count;
        }

        #endregion

        #region private 

        private void SetParent(EAssetObjectInfo asset)
        {
            /*if (_beDeps.ContainsKey(asset.AssetPath)) return;
            _beDeps.Add(asset.AssetPath, asset);
            _refCount++;*/
        }

        #endregion
    }
}
