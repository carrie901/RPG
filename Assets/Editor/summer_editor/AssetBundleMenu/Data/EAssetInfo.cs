using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// 1.自身大小
    /// 2.
    /// </summary>
    public abstract class EAssetInfo
    {
        public string AssetPath { get { return _asset_path; } }
        protected string _asset_path;                                                                           // 资源名称
        public int RefCount { get { return _ref_count; } }
        protected int _ref_count;                                                                               // 引用次数
        public float MemSize { get { return _mem_size; } }
        protected float _mem_size;                                                                                  // 内存资源大小
        public float FileSize { get { return _file_size; } }
        protected float _file_size;                                                                             // 文件大小        
        public float TextureCount { get { return _texture_count; } }                                            // 引用纹理个数
        protected float _texture_count;

        protected Dictionary<string, EAssetInfo> _child_dep_map = new Dictionary<string, EAssetInfo>();         // 子类依赖资源
        public Dictionary<string, EAssetInfo> _parent_dep_map = new Dictionary<string, EAssetInfo>();           // 所属于的父类

        protected EAssetInfo(string path)
        {
            _asset_path = path;
            _ref_count = 0;
            _child_dep_map.Clear();
            _parent_dep_map.Clear();
        }

        /// <summary>
        /// 检测第一层依赖
        /// </summary>
        public void CheckFirstDep()
        {
            // 
            _file_size = EMemorySizeHelper.GetFileSize(_asset_path);

            // 1.得到第一层依赖
            string[] deps = AssetDatabase.GetDependencies(_asset_path, false);
            int length = deps.Length;
            for (int i = 0; i < length; i++)
            {
                // 2.根据名字从全部的依赖列表中得到依赖资源的相关信息
                string asset_dep_path = deps[i];
                if (asset_dep_path.EndsWith(EAssetBundleConst.IGNORE_BUILD_ASSET_SUFFIX)) continue;
                EAssetInfo dep_ab = AssetBundleAnalysisE.FindDep(asset_dep_path);
                if (dep_ab == null) continue;
                // 依赖资源重复性质检测
                if (_child_dep_map.ContainsKey(dep_ab._asset_path))
                {
                    Debug.LogError(string.Format("主资源[{0}],已经存在了依赖资源了[{1}]", _asset_path, deps[i]));
                    continue;
                }
                // 3.添加资源的子依赖并且更新引用计数，设置父类
                _child_dep_map.Add(dep_ab._asset_path, dep_ab);
                dep_ab.Ref(this);
            }
        }

        /// <summary>
        /// 引用资源
        /// </summary>
        /// <param name="par_info"></param>
        public virtual void Ref(EAssetInfo par_info)
        {
            if (IsMainAsset())
            {
                Debug.LogErrorFormat("主资源[{0}]的上层主资源[{1}]ERROR", par_info._asset_path, par_info._asset_path);
            }
            else
            {

                if (_parent_dep_map.ContainsKey(par_info._asset_path))
                {
                    Debug.LogErrorFormat("父类资源[{0}]已经被[{1}]依赖了:", par_info._asset_path, AssetPath);
                    return;
                }
                _ref_count++;
                _parent_dep_map.Add(par_info._asset_path, par_info);
            }
        }
        /// <summary>
        /// 检测资源是否为主资源，true
        /// </summary>
        /// <returns></returns>
        public abstract bool IsMainAsset();
    }
}
