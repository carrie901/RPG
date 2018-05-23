using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 单独的资源Asset
    /// </summary>
    public class EAssetObjectInfo
    {
        #region 属性

        public string AssetPath { get { return _asset_path; } }
        protected string _asset_path;                                                                       // 资源名称
        public int RefCount { get { return _ref_count; } }
        protected int _ref_count;                                                                           // 引用次数
        public bool IsMainAsset { get { return _is_main_asset; } }
        protected bool _is_main_asset;                                                                      // 主依赖资源

        //public float MemSize { get { return _mem_size; } }
        //protected float _mem_size;                                                                                // 内存资源大小
        //public float FileSize { get { return _file_size; } }
        //protected float _file_size;                                                                               // 文件大小        
        //public float TextureCount { get { return _texture_count; } }                                            
        //protected float _texture_count;                                                                           // 引用纹理个数

        protected Dictionary<string, EAssetObjectInfo> _top_dep_map                                         // 顶层依赖资源
            = new Dictionary<string, EAssetObjectInfo>();
        /*protected Dictionary<string, EAssetObjectInfo> _all_dep_map                                       // 所有依赖资源
            = new Dictionary<string, EAssetObjectInfo>();*/
        protected Dictionary<string, EAssetObjectInfo> _be_deps                                             // 所属于的父类
            = new Dictionary<string, EAssetObjectInfo>();

        #endregion

        #region 构造

        public EAssetObjectInfo(string asset_path, bool is_main_asset = false)
        {
            _asset_path = asset_path;
            _is_main_asset = is_main_asset;
        }

        #endregion

        #region public

        public void AnalyzeAsset()
        {

            List<string> tmp_top_dep = new List<string>();
            AssetBundleHelper.FindRealDep(_asset_path, tmp_top_dep);
            
            // 添加Asset真真的依赖
            for (int i = tmp_top_dep.Count - 1; i >= 0; i--)
            {
                if (tmp_top_dep[i].EndsWith(EAssetBundleConst.IGNORE_BUILD_ASSET_SUFFIX)) continue;
                EAssetObjectInfo dep_ab = EAssetBundleAnalysis.FindAssetObject(tmp_top_dep[i]);
                if (dep_ab == null)
                {
                    Debug.LogError("找不到资源:" + tmp_top_dep[i] + "居然没进缓存");
                    continue;
                }
                Debug.AssertFormat(!dep_ab.IsMainAsset, "主依赖资源[{0}]不能成为依赖资源", dep_ab.AssetPath);
                _top_dep_map.Add(dep_ab.AssetPath, dep_ab);
                dep_ab.SetParent(this);
            }
        }

        public void SetParent(EAssetObjectInfo asset)
        {
            if (_be_deps.ContainsKey(asset.AssetPath)) return;
            _be_deps.Add(asset.AssetPath, asset);
            _ref_count++;
        }

        public int BeDepCount()
        {
            return _be_deps.Count;
        }

        #endregion
    }
}
