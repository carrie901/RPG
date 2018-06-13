using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    /// <summary>
    /// 当前这个职责是有一点混乱的
    /// 本身应该注重的事AssetBundle方面的加载
    /// 同事他有涉及到了一定程度上的cache方面的 职责上的问题
    /// 
    /// 问题
    /// 1. 目前没有处理如果加载的资源又是主资源，又是依赖资源这种情况
    ///     1.1引用的问题，主引用和依赖引用
    ///     1.2处于别人的依赖包中/处于别人加载的包中
    /// 2. 无法处理同步和异步同事加载一个资源，而且也没有预警措施
    /// 
    /// 区分res_path路径，assetbundle_path,assetbundle_name
    /// 分别是原始的加载路径（res/../a.prefab），对应的AB包路径(StreamingAssets/../a.ab)，ab的名字（a）
    /// </summary>
    public class AssetBundleLoader : I_ResourceLoad
    {
        #region 静态

        public static AssetBundleLoader _instance;
        public static AssetBundleLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssetBundleLoader();
                    Init();
                }
                return _instance;
            }
        }
        public static int max_loading_count = 5;                                                // 一次性最大加载个数
        public static Dictionary<string, AssetBundleDepInfo> dep_map
              = new Dictionary<string, AssetBundleDepInfo>();
        public static Dictionary<string, AssetBundleRes> res_map
           = new Dictionary<string, AssetBundleRes>();

        #endregion

        #region 构造

        public Dictionary<string, string[]> dep_maps = new Dictionary<string, string[]>();
        public AssetBundleLoader()
        {
            AssetBundle ab = AssetBundle.LoadFromFile(AssetBundleConst.GetAssetBundleRootDirectory() + "/rpg");
            AssetBundleManifest manifest = ab.LoadAllAssets()[0] as AssetBundleManifest;

            string[] all_ab = manifest.GetAllAssetBundles();
            for (int i = 0; i < all_ab.Length; i++)
            {
                string[] deps = manifest.GetAllDependencies(all_ab[i]);
                dep_maps.Add(all_ab[i], deps);
            }
        }

        #endregion

        #region param

        public static Dictionary<string, AssetBundlePackageInfo> package_map
          = new Dictionary<string, AssetBundlePackageInfo>();


        public List<LoadOpertion> _on_loading_request = new List<LoadOpertion>();               // 正在加载的请求           
        public Queue<LoadOpertion> _wait_to_load_request = new Queue<LoadOpertion>();           // 等待加载的请求   
        protected List<string> on_loading_ab_package                                            //加载中的资源包
            = new List<string>();

        protected Dictionary<string, int> on_wait_ab_package = new Dictionary<string, int>();   //加载中的资源包

        #endregion

        #region I_ResourceLoad

        // TODO Bug没有好的防御机制，在加载失败的情况下，不会导致整个程序死掉
        public AssetInfo LoadAsset<T>(string res_path) where T : UnityEngine.Object
        {
            // 1.资源对应的包信息
            AssetBundleRes res_info = GetAssetBundleRes(res_path);
            if (res_info == null) return null;

            // 2.得到AssetBundle包
            AssetBundlePackageInfo main_package_info = GetPackageInfo(res_info.package_path);
            // 3.加载依赖信息
            AssetBundleDepInfo deps_info = GetDepInfo(res_info.package_path);
            foreach (var dep_info in deps_info.child_ref)
            {
                string dependencies = dep_info.Key;
                AssetBundlePackageInfo package_info = GetPackageInfo(dependencies);
                if (!_need_load(package_info)) continue;
                _internal_syncload_package(package_info);
            }

            _internal_syncload_package(main_package_info);

            // 3.包中的资源
            AssetInfo asset_info = main_package_info.GetAsset<T>(res_info.res_path);
            return asset_info;
        }

        public LoadOpertion LoadAssetAsync<T>(string res_path) where T : UnityEngine.Object
        {
            // 1.资源对应的包
            AssetBundleRes res_info = GetAssetBundleRes(res_path);
            if (res_info == null) return null;
            AssetBundlePackageInfo main_package_info = GetPackageInfo(res_info.package_path);

            AssetBundleDepInfo dep_info = GetDepInfo(res_info.package_path);
            foreach (var info in dep_info.child_ref)
            {
                AssetBundlePackageInfo dep_package_info = GetPackageInfo(info.Key);
                if (!_need_load(dep_package_info)) continue;
                _internal_asyncload_package(dep_package_info);
            }

            AssetBundleAsyncLoadOpertion main_opertion = _internal_asyncload_package(main_package_info);
            return main_opertion;
        }

        public bool HasInLoading(string name)
        {
            return true;
        }

        public bool UnloadAll()
        {
            return true;
        }

        public bool UnloadAssetBundle(AssetInfo asset_info)
        {
            // 1.资源对应的包信息
            AssetBundleRes res_info = GetAssetBundleRes(asset_info.ResPath);
            if (res_info == null) return false;

            // 2.得到AssetBundle包
            AssetBundlePackageInfo main_package_info = GetPackageInfo(res_info.package_path);

            // 3.加载依赖信息
            AssetBundleDepInfo deps_info = GetDepInfo(res_info.package_path);
            foreach (var dep_info in deps_info.child_ref)
            {
                ResLog.Log("引用--,dep_info:[{0}]", dep_info.Key);
                string dependencies = dep_info.Key;
                //AssetBundlePackageInfo package_info = GetPackageInfo(dependencies);
                
            }
            main_package_info.UnLoad();
            return true;
        }

        public void OnUpdate()
        {
            // 1.更新请求
            int length = _on_loading_request.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (_on_loading_request[i].OnUpdate())
                {
                    _on_loading_request.RemoveAt(i);
                }
            }

            // 检测新的内容
            for (int i = length; i < max_loading_count; i++)
            {
                if (_wait_to_load_request.Count <= 0) continue;
                LoadOpertion load_opertion = _wait_to_load_request.Dequeue();
                load_opertion.OnInit();
                _on_loading_request.Add(load_opertion);
            }
        }

        #endregion

        #region public 

        // AssetBundle是否处于加载状态
        public bool ContainsLoadAssetBundles(string ab_package_path)
        {
            return on_loading_ab_package.Contains(ab_package_path);
        }

        #endregion

        #region private

        public void _internal_syncload_package(AssetBundlePackageInfo package_info)
        {
            AssetBundle assetbundle = AssetBundle.LoadFromFile(package_info.FullPath);
            bool result = (assetbundle != null);
            ResLog.Assert(result, "同步加载AssetBundlePack失败，路径不存在:[{0}]", package_info.PackagePath);

            if (assetbundle == null) return;
            Object[] objs = assetbundle.LoadAllAssets();
            package_info.InitAssetBundle(assetbundle, objs);
        }

        // 增加一个请求
        public AssetBundleAsyncLoadOpertion _internal_asyncload_package(AssetBundlePackageInfo package_info)
        {
            AssetBundleAsyncLoadOpertion package_opertion = new AssetBundleAsyncLoadOpertion(package_info);
            _wait_to_load_request.Enqueue(package_opertion);

            return package_opertion;
        }

        public bool _need_load(AssetBundlePackageInfo package_info)
        {
            if (package_info == null) return false;
            // 已经完成
            if (package_info.IsComplete) return false;

            // 在等待中
            if (on_wait_ab_package.ContainsKey(package_info.PackagePath)) return false;
            // 已经在加载中
            if (on_loading_ab_package.Contains(package_info.PackagePath)) return false;
            return true;
        }

        #endregion

        #region static

        public static AssetBundleRes GetAssetBundleRes(string res_path)
        {
            AssetBundleRes res_info;
            res_map.TryGetValue(res_path, out res_info);
            ResLog.Assert((res_info != null), "资源[{0}]找不到对应的包", res_path);
            return res_info;
        }

        public static AssetBundleDepInfo GetDepInfo(string assetbundle_package_path)
        {
            if (dep_map.ContainsKey(assetbundle_package_path))
                return dep_map[assetbundle_package_path];

            ResLog.Error("dep_map不可能出现的情况，尼玛居然出现了[{0}]______", assetbundle_package_path);
            return null;
        }

        public static AssetBundlePackageInfo GetPackageInfo(string assetbundle_package_path)
        {
            if (package_map.ContainsKey(assetbundle_package_path))
                return package_map[assetbundle_package_path];
            ResLog.Error("找不到对应的主包:[{0}]不存在", assetbundle_package_path);
            return null;
        }

        public static void Init()
        {
            dep_map.Clear();
            package_map.Clear();
            res_map.Clear();

            List<string[]> dep_result = AssetBundleConfig.GetAbInfo(AssetBundleConst.assetbundle_dep_path);
            int length = dep_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleDepInfo dep = new AssetBundleDepInfo(dep_result[i]);
                dep_map.Add(dep.AssetBundleName, dep);
            }

            List<string[]> package_result = AssetBundleConfig.GetAbInfo(AssetBundleConst.assetbundle_package_path);
            length = package_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundlePackageInfo package_info = new AssetBundlePackageInfo(package_result[i]);
                package_map.Add(package_info.PackagePath, package_info);
            }

            List<string[]> res_result = AssetBundleConfig.GetAbInfo(AssetBundleConst.assetbundle_res_path);
            length = res_result.Count;
            for (int i = 0; i < length; i++)
            {
                AssetBundleRes res = new AssetBundleRes(res_result[i]);
                res_map.Add(res.res_path, res);
            }
        }

        #endregion

    }
}
