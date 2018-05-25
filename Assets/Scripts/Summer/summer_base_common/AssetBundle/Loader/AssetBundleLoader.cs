using System;
using System.Collections;
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

        public static AssetBundleLoader instance = new AssetBundleLoader();

        public static bool _init_complete;                                                 //主文件加载完成
        public static string _evn_path;                                                    //环境路径
        public const int TIME_OUT = 120;

        #endregion

        #region param

        //public Dictionary<string, AssetBundlePackageInfo> package_map
        //    = new Dictionary<string, AssetBundlePackageInfo>();

        public List<OloadOpertion> _load_opertions                                  //加载的请求
            = new List<OloadOpertion>(32);

        protected Dictionary<string, MainBundleInfo> load_assetbundles              //被加载的主资源包
            = new Dictionary<string, MainBundleInfo>();

        protected Dictionary<string, DepBundleInfo> dep_bundles                     //依赖的资源包
            = new Dictionary<string, DepBundleInfo>();

        protected List<string> on_loading_ab_package                              //加载中的资源包
            = new List<string>();

        public Dictionary<string, string> _asset_list = new Dictionary<string, string>();


        //public AssetBundleConfig ab_config;
        #endregion

        public AssetBundleLoader()
        {
            AssetBundleConfig.Init();
            _init_complete = true;
            instance = this;
            _evn_path = Application.streamingAssetsPath + "/rpg/";
            /*string main_fest_path = Application.streamingAssetsPath + "/rpg/rpg";
            AssetBundle ab = AssetBundle.LoadFromFile(main_fest_path);
            _mainfest = ab.LoadAllAssets()[0] as AssetBundleManifest;
            if (_mainfest != null)
            {
                string[] asset_names = _mainfest.GetAllAssetBundles();

                int asset_length = asset_names.Length;
                for (int i = 0; i < asset_length; i++)
                {
                    string[] deps = _mainfest.GetAllDependencies(asset_names[i]);
                    _assetbundle_map.Add(asset_names[i], deps);
                }
            }*/

        }

        #region I_ResourceLoad

        public Object LoadAsset(string res_path)
        {
            AssetBundleRes res_info;
            AssetBundleConfig.res_map.TryGetValue(res_path, out res_info);
            if (res_info == null)
            {
                ResLog.Error("资源[{0}]找不到对应的包", res_path);
                return null;
            }

            //res_path = "res_bundle/" + res_path;
            //string assetbundle_name, asset_name;
            // 1.解析文件路径信息
            //_parse_path(res_path, out assetbundle_name, out asset_name);
            AssetBundlePackageInfo info = _load_assetbundle_package(res_info);

            if (info == null) return null;
            Object obj = info.LoadAsset(res_info.res_name);
            return obj;
            // 2.加载AssetBundle
            /*AssetBundle asset_target = _load_assetbundle(res_info);

            if (asset_target == null)
            {
                ResLog.Error("找不到对应的资源，地址:{0}", res_path);
                return null;
            }

            _cal_ref(res_info);
            // 3.加载Asset
            Object obj = asset_target.LoadAsset(res_info.res_name);
            asset_target.Unload(false);
            return obj;*/
            /*return null;*/
        }

        public OloadOpertion LoadAssetAsync(string path)
        {
            string assetbundle_name, asset_name;
            // 1.解析文件路径信息
            _parse_path(path, out assetbundle_name, out asset_name);
            // 2.主资源的属性
            MainBundleInfo info = _find_main_asset(assetbundle_name);
            // 3.加载依赖资源
            StartCoroutineManager.Start(load_dependencies_assetbundle_async(info));
            // 4.主资源请求
            string real_path = _evn_path + assetbundle_name;
            OabMainLoadOpertion main_ab = new OabMainLoadOpertion(real_path, asset_name, info);
            _load_opertions.Add(main_ab);
            return main_ab;
        }

        public bool HasInLoading(string name)
        {
            return true;
        }

        public bool UnloadAll()
        {
            return true;
        }

        public bool UnloadAssetBundle(string assetbundle_path)
        {
            return true;
        }

        public void Update()
        {
            int length = _load_opertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (_load_opertions[i].Update())
                {
                    _load_opertions.RemoveAt(i);
                }
            }
        }

        #endregion

        #region public 

        // AssetBundle是否处于加载状态
        public bool ContainsLoadAssetBundles(string assetbundle_name)
        {
            return on_loading_ab_package.Contains(assetbundle_name);
        }

        #endregion

        #region private

        // 解析路径和AssetName 这里需要区分AssetBundleName和AssetName
        public void _parse_path(string path, out string assetbundle_name, out string asset_name)
        {
            //AssetBundleName 是指ab包的名字
            //AssetName指的事ab包的Asset资源的名字
            //path = path.Substring(0, path.LastIndexOf(".", StringComparison.Ordinal));
            assetbundle_name = path;
            int index = path.LastIndexOf("/", StringComparison.Ordinal);

            path = path.Substring(0, path.LastIndexOf(".", StringComparison.Ordinal));
            asset_name = path.Substring(index + 1);
        }

        public AssetBundlePackageInfo _load_assetbundle_package(AssetBundleRes res_info)
        {
            // 1.检测是否处于加载状态
            if (on_loading_ab_package.Contains(res_info.package_path))
            {
                ResLog.Error("当前资源处于异步加载中.[{0}]", res_info.package_path);
                return null;
            }

            // 2.加载依赖的AssetBundle
            _load_dependencies_assetbundle(res_info);
            AssetBundlePackageInfo package_info;
            if (!AssetBundleConfig.package_map.TryGetValue(res_info.package_path, out package_info))
            {
                ResLog.Error("资源路径[{0}]对应的主包:[{1}]不存在", res_info.res_path, res_info.package_path);
                return null;
            }

            if (package_info.IsComplete)
            {
                package_info.GetAsset(res_info.res_name);
            }
            else
            {
                // 优化掉这一步
                string asset_path = _evn_path + package_info.PackagePath;
                AssetBundle assetbundle = AssetBundle.LoadFromFile(asset_path);
                package_info.InitAssetBundle(assetbundle);
            }
            return package_info;
        }

        // 同步加载依赖的AssetBundle
        public void _load_dependencies_assetbundle(AssetBundleRes res_info)
        {
            // 1.初始化完毕
            if (!_init_complete) return;

            //TODO 待优化缓存这一部分内容
            AssetBundleDepInfo dep_info = AssetBundleConfig.GetDepInfo(res_info.package_path);
            int length = dep_info.dep_count;
            foreach (var info in dep_info.child_ref)
            {
                string dependencies = info.Key;
                if (!dep_bundles.ContainsKey(dependencies))
                {
                    AssetBundle asset_bundle = AssetBundle.LoadFromFile(_evn_path + dependencies);
                    dep_bundles.Add(dependencies, new DepBundleInfo(asset_bundle));
                }
            }
        }

        // 异步加载依赖资源
        public IEnumerator load_dependencies_assetbundle_async(MainBundleInfo info)
        {
            Dictionary<string, int> dep_map = info.DepMap();
            foreach (var dep_info in dep_map)
            {
                string assetbundle_name = dep_info.Key;
                int load_count = dep_info.Value;
                if (load_count == 0)
                    ResLog.Error("_new_load_dependencies_assetbundle_async Error,[{0}]", assetbundle_name);
                if (on_loading_ab_package.Contains(assetbundle_name))
                {
                    OabLoadWaitOpertion wait_opertion = new OabLoadWaitOpertion(assetbundle_name, TIME_OUT);
                    _load_opertions.Add(wait_opertion);
                    // 6.等待主包加载完成
                    yield return wait_opertion;
                    info.LoadComplete(assetbundle_name);
                }
                else
                {
                    //将ab_name加入加载中列表
                    on_loading_ab_package.Add(assetbundle_name);
                    string real_path = _evn_path + assetbundle_name;
                    OabDepLoadOpertion ab_async_opertion = new OabDepLoadOpertion(real_path, assetbundle_name);
                    _load_opertions.Add(ab_async_opertion);
                    yield return ab_async_opertion;
                    info.LoadComplete(assetbundle_name);
                    //将ab从加载中列表移除
                    on_loading_ab_package.Remove(assetbundle_name);
                }
            }
            yield return null;
        }

        // 计算引用
        public void _cal_ref(AssetBundleRes res_info)
        {
            /*AssetBundleDepInfo dep_info = _find_all_dependencies(assetbundle_name);
            int length = deps.Length;
            for (int i = 0; i < length; i++)
            {
                string dep_name = deps[i];
                DepBundleInfo dep_info;
                dep_bundles.TryGetValue(dep_name, out dep_info);
                if (dep_info != null)
                {
                    dep_info.RefCount++;
                }
                else
                {
                    ResLog.Error("[{0}]找不到依赖资源[{1}]", assetbundle_name, dep_name);
                }
            }*/
        }

        // 依赖信息
        /*public AssetBundleDepInfo _find_all_dependencies(string assetbundle_path)
        {
            AssetBundleDepInfo dep_info;
            if (AssetBundleConfig.dep_map.TryGetValue(assetbundle_path, out dep_info))
            {
                return dep_info;
            }

            ResLog.Error("不可能出现的情况，尼玛居然出现了[{0}]______", assetbundle_path);
            return null;
        }*/

        // 得到主资源的属性
        public MainBundleInfo _find_main_asset(string assetbundle_name)
        {
            MainBundleInfo info;
            if (load_assetbundles.TryGetValue(assetbundle_name, out info))
            {
                info = load_assetbundles[assetbundle_name];
                ResLog.Error("得到主资源的属性出现，不应该出现,AssetBundle:[{0}]", assetbundle_name);
            }
            else
            {
                info = new MainBundleInfo(assetbundle_name);
                load_assetbundles.Add(assetbundle_name, info);
            }
            return info;
        }


        #endregion
    }
}
