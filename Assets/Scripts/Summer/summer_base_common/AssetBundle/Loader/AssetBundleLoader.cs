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
    /// </summary>
    public class AssetBundleLoader : I_ResourceLoad
    {
        public static AssetBundleLoader instance = new AssetBundleLoader();

        public AssetBundleManifest _mainfest;                                       //主依赖文件
        public bool _init_complete;                                                 //主文件加载完成
        public string _evn_path;                                                    //环境路径
        public const int TIME_OUT = 120;
        #region param
        public Dictionary<string, string[]> _assetbundle_map
            = new Dictionary<string, string[]>();

        public List<OloadOpertion> _load_opertions                                  //加载的请求
            = new List<OloadOpertion>(32);

        protected Dictionary<string, MainBundleInfo> load_assetbundles              //被加载的主资源包
            = new Dictionary<string, MainBundleInfo>();

        protected Dictionary<string, DepBundleInfo> dep_bundles                     //依赖的资源包
            = new Dictionary<string, DepBundleInfo>();

        protected List<string> on_loading_assetbundles                              //加载中的资源包
            = new List<string>();
        #endregion

        public AssetBundleLoader()
        {
            string main_fest_path = Application.streamingAssetsPath + "/Assetbundle/Assetbundle";
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
                _init_complete = true;

                _evn_path = Application.streamingAssetsPath + "/Assetbundle/";
                instance = this;
            }
        }

        #region I_ResourceLoad

        public Object LoadAsset(string path)
        {
            string assetbundle_name, asset_name;
            // 1.解析文件路径信息
            _parse_path(path, out assetbundle_name, out asset_name);

            // 2.加载AssetBundle
            AssetBundle asset_target = _load_assetbundle(assetbundle_name);

            if (asset_target == null)
            {
                LogManager.Error("找不到对应的资源，地址:{0}", path);
                return null;
            }

            _cal_ref(assetbundle_name);
            // 3.加载Asset
            Object obj = asset_target.LoadAsset(asset_name);
            return obj;
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
            return on_loading_assetbundles.Contains(assetbundle_name);
        }

        #endregion

        #region private

        // 解析路径和AssetName 这里需要区分AssetBundleName和AssetName
        public void _parse_path(string path, out string rel_path, out string obj_name)
        {
            //AssetBundleName 是指ab包的名字
            //AssetName指的事ab包的Asset资源的名字
            path = path.Substring(0, path.LastIndexOf(".", StringComparison.Ordinal));
            rel_path = path;
            int index = path.LastIndexOf("/", StringComparison.Ordinal);
            obj_name = path.Substring(index + 1);
        }

        // 同步加载主资源
        public AssetBundle _load_assetbundle(string assetbundle_name)
        {
            // 1.检测是否处于加载状态
            if (on_loading_assetbundles.Contains(assetbundle_name))
            {
                LogManager.Error("当前资源处于异步加载中.[{0}]", assetbundle_name);
                return null;
            }

            // 2.加载依赖的AssetBundle
            _load_dependencies_assetbundle(assetbundle_name);

            AssetBundle asset_target = null;
            // 3.根据地址加载AssetBundle,并且缓存
            if (!load_assetbundles.ContainsKey(assetbundle_name))
            {
                // 3.1生成MainAssetInfo
                _find_main_asset(assetbundle_name);
                // 3.2加载的全路径
                string asset_path = _evn_path + assetbundle_name;
                // 3.3加载资源
                asset_target = AssetBundle.LoadFromFile(asset_path);
            }
            else
            {
                LogManager.Error("_load_assetbundle/主包资源重复加载.路径:[{0}]", assetbundle_name);
            }

            return asset_target;
        }

        // 同步加载依赖的AssetBundle
        public void _load_dependencies_assetbundle(string assetbundle_name)
        {
            // 1.初始化完毕
            if (!_init_complete) return;

            //TODO 待优化缓存这一部分内容
            string[] deps = _find_all_dependencies(assetbundle_name);
            int length = deps.Length;
            for (int i = 0; i < length; i++)
            {
                string dependencies = deps[i];
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
                    LogManager.Error("_new_load_dependencies_assetbundle_async Error,[{0}]", assetbundle_name);
                if (on_loading_assetbundles.Contains(assetbundle_name))
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
                    on_loading_assetbundles.Add(assetbundle_name);
                    string real_path = _evn_path + assetbundle_name;
                    OabDepLoadOpertion ab_async_opertion = new OabDepLoadOpertion(real_path, assetbundle_name);
                    _load_opertions.Add(ab_async_opertion);
                    yield return ab_async_opertion;
                    info.LoadComplete(assetbundle_name);
                    //将ab从加载中列表移除
                    on_loading_assetbundles.Remove(assetbundle_name);
                }
            }
            yield return null;
        }

        // 计算引用
        public void _cal_ref(string assetbundle_name)
        {
            string[] deps = _find_all_dependencies(assetbundle_name);
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
                    LogManager.Error("[{0}]找不到依赖资源[{1}]", assetbundle_name, dep_name);
                }
            }
        }

        // 依赖信息
        public string[] _find_all_dependencies(string assetbundle_name)
        {
            string[] deps;
            if (_assetbundle_map.TryGetValue(assetbundle_name, out deps))
            {
                return deps;
            }

            LogManager.Error("不可能出现的情况，尼玛居然出现了");
            deps = _mainfest.GetAllDependencies(assetbundle_name);
            return deps;
        }

        // 得到主资源的属性
        public MainBundleInfo _find_main_asset(string assetbundle_name)
        {
            MainBundleInfo info;
            if (load_assetbundles.TryGetValue(assetbundle_name, out info))
            {
                info = load_assetbundles[assetbundle_name];
                LogManager.Error("得到主资源的属性出现，不应该出现,AssetBundle:[{0}]", assetbundle_name);
            }
            else
            {
                info = new MainBundleInfo(assetbundle_name);
                load_assetbundles.Add(assetbundle_name, info);
            }
            return info;
        }


        #endregion

        /*public IEnumerator _load_assetbundle_async(string assetbundle_name, string asset_name, Action<Object> complete)
        {
            // 1.如果主包中已经包含了那么直接回调
            if (load_assetbundles.ContainsKey(assetbundle_name))
            {
                LogManager.Error("主包中已经包含对应的资源，但应该在上层已经屏蔽掉，本层只做加载,[{0}]", assetbundle_name);
                yield break;
            }
            // 2.如果处于其他请求在处理的依赖包
            if (on_loading_assetbundles.Contains(assetbundle_name))
            {
                LogManager.Error("加载的资源处于正在加载中，但主资源的加载属于非本层加载,非法路径:[{0}]", assetbundle_name);
                yield break;
            }

            // 3.将ab_name加入加载中列表
            on_loading_assetbundles.Add(assetbundle_name);
            // 4.加载依赖资源
            yield return _load_dependencies_assetbundle_async(assetbundle_name);
            // 5.创建异步加载请求
            OabAsyncLoadOpertion ab_async_opertion
                = new OabAsyncLoadOpertion(_evn_path + assetbundle_name, asset_name);
            _load_opertions.Add(ab_async_opertion);
            // 6.等待主包加载完成
            yield return ab_async_opertion;
            // 7.把ab_namec从加载中列表移除
            on_loading_assetbundles.Remove(assetbundle_name);
            // 8.缓存数据
            _pop_to_cache(assetbundle_name);

            complete(ab_async_opertion.GetAsset());
        }*/


        //TODO 还没想好又是主资源又是依赖资源的情况
        /* public IEnumerator _load_dependencies_assetbundle_async(string assetbundle_name)
         {
             // 1.得到依赖信息
             string[] dependencies_asset_bundles = _find_all_dependencies(assetbundle_name);

             // 2.依次加载依赖信息
             int length = dependencies_asset_bundles.Length;
             for (int i = 0; i < length; i++)
             {
                 string dep_assetbundle_name = dependencies_asset_bundles[i];

                 // 3.如果处于其他请求在处理的依赖包
                 if (on_loading_assetbundles.Contains(assetbundle_name))
                 {
                     float time_out = 60f;
                     while (time_out > 0)
                     {
                         time_out -= Time.timeScale * Time.deltaTime;
                         yield return null;

                         if (on_loading_assetbundles.Contains(assetbundle_name))
                             continue;
                         break;
                     }
                     // 3.2如果是其他依赖包发起的加载，那么直接开始下一个依赖包的加载
                     if (dep_bundles.ContainsKey(dep_assetbundle_name))
                     {
                         continue;
                     }
                     //TODO
                     // 3.3如果是主包发起的加载，那么这次请求只需要将主包拷贝入依赖列表
                 }

                 // 4.将ab_name加入加载中列表
                 on_loading_assetbundles.Add(assetbundle_name);
                 // 5.创建异步加载请求
                 AssetBundleCreateRequest asset_bundle_dependencies = AssetBundle.LoadFromFileAsync(_evn_path + dep_assetbundle_name);
                 // 6.等待请求完成
                 yield return asset_bundle_dependencies;
                 // 在依赖包中增加请求的assetbundle
                 if (asset_bundle_dependencies.assetBundle != null)
                 {
                     dep_bundles.Add(dep_assetbundle_name, new DepBundleInfo(asset_bundle_dependencies.assetBundle));
                 }

                 //将 asset bundle 从加载中列表移除
                 on_loading_assetbundles.Remove(assetbundle_name);
             }
         }*/
    }

}
