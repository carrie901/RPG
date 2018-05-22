using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor.Animations;
using Object = UnityEngine.Object;

namespace SummerEditor
{
    public class AssetBundleAnalyzeManager
    {
        protected static List<EAssetBundleFileInfo> _asset_bundle_file_infos                        // 获取所有的AB文件信息                    
            = new List<EAssetBundleFileInfo>();
        protected static Dictionary<long, EAssetFileInfo> _asset_file_infos                         // 所有的Asset的资源信息
            = new Dictionary<long, EAssetFileInfo>();


        public static void Analyze()
        {
            if (!Directory.Exists(EAssetBundleConst.assetbundle_directory))
            {
                Debug.LogError("不存在:" + EAssetBundleConst.assetbundle_directory + "目录");
                return;
            }

            // 收集AB的信息
            AnalyzeManifestAbDep();
            AnalyzeBundleFiles();
            CreateReport();
        }

        /// <summary>
        /// Manifest中的AB依赖相关信息
        /// </summary>
        public static void AnalyzeManifestAbDep()
        {
            _asset_bundle_file_infos.Clear();
            string manifest_path = EAssetBundleConst.ManifestPath;
            AssetBundle manifest_ab = AssetBundle.LoadFromFile(manifest_path);

            Debug.AssertFormat(manifest_ab != null, "manifest_ab 加载失败,路径地址:[{0}]", manifest_path);
            if (manifest_ab == null) return;

            AssetBundleManifest asset_bundle_manifest = manifest_ab.LoadAsset<AssetBundleManifest>("assetbundlemanifest");
            string[] bundles = asset_bundle_manifest.GetAllAssetBundles();

            int length = bundles.Length;
            for (int i = 0; i < length; i++)
            {
                EAssetBundleFileInfo info = new EAssetBundleFileInfo(bundles[i]);
                info.all_depends.AddRange(asset_bundle_manifest.GetAllDependencies(info.ab_name));
                _asset_bundle_file_infos.Add(info);
            }
            manifest_ab.Unload(true);
        }

        public static void AnalyzeBundleFiles()
        {
            // 1.A 文件 被哪些文件引用
            foreach (var info in _asset_bundle_file_infos)
            {
                List<string> be_depends = new List<string>();
                foreach (var info2 in _asset_bundle_file_infos)
                {
                    if (info2.ab_name == info.ab_name) continue;

                    if (info2.all_depends.Contains(info.ab_name))
                    {
                        be_depends.Add(info2.ab_name);
                    }
                }
                info.be_depends.Clear();
                info.be_depends.AddRange(be_depends.ToArray());
            }

            // 分析assetbundle下asset引用信息
            foreach (var info in _asset_bundle_file_infos)
            {
                _analyze_assetbundle_file(info);
            }
            Resources.UnloadUnusedAssets();
        }

        public static void CreateReport()
        {
            string dir = DateTime.Now.ToString("yyyy_MM_dd__HHmm");
            string report_dir = Application.dataPath + "/../Report/" + dir;
            if (!Directory.Exists(report_dir))
                Directory.CreateDirectory(report_dir);
            AssetBundleReport.CreateReport(report_dir);
            AssetBundleDepReport.CreateReport(report_dir);
            AssetReport.CreateReport(report_dir);
            TextureReport.CreateReport(report_dir);
            AnimationClipReport.CreateReport(report_dir);
            MeshReport.CreateReport(report_dir);
        }

        public static Dictionary<long, EAssetFileInfo> FindAssetFiles()
        {
            return _asset_file_infos;
        }

        public static List<EAssetBundleFileInfo> FindAssetBundleFiles()
        {
            return _asset_bundle_file_infos;
        }

        public static EAssetBundleFileInfo FindAssetBundleFile(string ab_name)
        {
            int length = _asset_bundle_file_infos.Count;
            for (int i = 0; i < length; i++)
            {
                if (_asset_bundle_file_infos[i].ab_name == ab_name)
                    return _asset_bundle_file_infos[i];
            }
            return null;
        }

        public static EAssetFileInfo FindAssetFile(long guid)
        {
            if (_asset_file_infos == null)
            {
                _asset_file_infos = new Dictionary<long, EAssetFileInfo>();
            }

            EAssetFileInfo info;
            if (!_asset_file_infos.TryGetValue(guid, out info))
            {
                info = new EAssetFileInfo(guid);
                _asset_file_infos.Add(guid, info);
            }
            return info;
        }

        public static void Clear()
        {
            _asset_file_infos.Clear();
            _asset_bundle_file_infos.Clear();
            EditorUtility.UnloadUnusedAssetsImmediate();
            System.GC.Collect();
        }

        #region private

        // 分析AssetBundle信息
        public static void _analyze_assetbundle_file(EAssetBundleFileInfo assetbundle_file_info)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(assetbundle_file_info.file_path);
            string resual_name = string.Empty;
            try
            {
                if (ab == null)
                {
                    Debug.LogErrorFormat("[{0}]加载失败:", assetbundle_file_info.file_path);
                    return;
                }
                Object[] objs = ab.LoadAllAssets<Object>();
                Object[] collect_depends = EditorUtility.CollectDependencies(objs);
                int length = collect_depends.Length;
                for (int i = 0; i < length; i++)
                {
                    if (!collect_depends[i]) continue;
                    resual_name = collect_depends[i].name;
                    _analyze_asset_object(collect_depends[i], assetbundle_file_info);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[" + assetbundle_file_info.ab_name + "]_[" + resual_name + "]_" + e.Message);
            }
            finally
            {
                if (ab != null) ab.Unload(true);
            }
        }


        private static PropertyInfo inspector_mode;
        // 分析Asset Object信息
        public static void _analyze_asset_object(Object asset_object, EAssetBundleFileInfo assetbundle_file_info)
        {
            // 1.检测类型需要符合要求
            E_AssetType asset_type = AssetBundleAnallyzeObject.CheckObject(asset_object, assetbundle_file_info);
            if (asset_type == E_AssetType.none) return;

            if (inspector_mode == null)
            {
                inspector_mode = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            }

            // 得到guid
            SerializedObject serialized_object = new SerializedObject(asset_object);
            inspector_mode.SetValue(serialized_object, InspectorMode.Debug, null);
            SerializedProperty path_id_prop = serialized_object.FindProperty(EAssetBundleConst.LOCAL_ID_DENTFIER_IN_FILE);
            if (path_id_prop == null)
            {
                Debug.LogError("得到Id失败:" + assetbundle_file_info.ab_name + "_" + asset_object.ToString());
                return;
            }
            long guid = path_id_prop.longValue;

            if (assetbundle_file_info.IsAssetContain(guid))
            {
                Debug.LogAssertionFormat("[{0}]已经存在了[{1}]资源", assetbundle_file_info.ab_name, asset_object.name);
                serialized_object.Dispose();
                return;
            }


            EAssetFileInfo asset_file_info = FindAssetFile(guid);

            /*if (asset_file_info.InitAsset && asset_file_info.asset_name == asset_object.name)
            {
                Debug.LogError("Error");
            }*/

            asset_file_info.InitAsset = true;
            asset_file_info.asset_name = asset_object.name;
            asset_file_info.asset_type = asset_type;

            if (AssetBundleAnallyzeObject.fun_map.ContainsKey(asset_type))
                asset_file_info.propertys = AssetBundleAnallyzeObject.fun_map[asset_type].Invoke(asset_object, serialized_object);

            // AssetBundle包含了Asset资源
            assetbundle_file_info.dep_asset_files.Add(asset_file_info);
            // Asset被指定的AssetBundle引用
            asset_file_info.included_bundles.Add(assetbundle_file_info);
            serialized_object.Dispose();
        }

        #endregion
    }
}

