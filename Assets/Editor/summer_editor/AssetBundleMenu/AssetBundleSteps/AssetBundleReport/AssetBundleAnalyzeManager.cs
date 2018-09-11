using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;
using Object = UnityEngine.Object;

namespace SummerEditor
{
    public class AssetBundleAnalyzeManager
    {
        protected static List<EAssetBundleFileInfo> _assetBundleFileInfos                        // 获取所有的AB文件信息                    
            = new List<EAssetBundleFileInfo>();
        protected static Dictionary<long, EAssetFileInfo> _assetFileInfos                         // 所有的Asset的资源信息
            = new Dictionary<long, EAssetFileInfo>();


        public static void Analyze()
        {
            Resources.UnloadUnusedAssets();
            if (!Directory.Exists(EAssetBundleConst.assetbundle_directory))
            {
                Debug.LogError("不存在:" + EAssetBundleConst.assetbundle_directory + "目录");
                return;
            }

            // 收集AB的信息
            _analyze_collect_bundles();
            _analyze_bundle_files();
            _create_report();


        }

        #region public 

        public static Dictionary<long, EAssetFileInfo> FindAssetFiles()
        {
            return _assetFileInfos;
        }

        public static List<EAssetBundleFileInfo> FindAssetBundleFiles()
        {
            return _assetBundleFileInfos;
        }

        public static EAssetFileInfo FindAssetFile(long guid)
        {
            if (_assetFileInfos == null)
            {
                _assetFileInfos = new Dictionary<long, EAssetFileInfo>();
            }

            EAssetFileInfo info;
            if (!_assetFileInfos.TryGetValue(guid, out info))
            {
                info = new EAssetFileInfo(guid);
                _assetFileInfos.Add(guid, info);
            }
            return info;
        }

        public static void Clear()
        {
            _assetFileInfos.Clear();
            _assetBundleFileInfos.Clear();
            EditorUtility.UnloadUnusedAssetsImmediate();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            GC.Collect();
        }

        #endregion

        #region private

        /// <summary>
        /// 收集bundle资源
        /// </summary>
        public static void _analyze_collect_bundles()
        {
            _assetBundleFileInfos.Clear();
            string manifestPath = EAssetBundleConst.ManifestPath;
            AssetBundle manifestAb = AssetBundle.LoadFromFile(manifestPath);

            Debug.AssertFormat(manifestAb != null, "manifest_ab 加载失败,路径地址:[{0}]", manifestPath);
            if (manifestAb == null) return;

            AssetBundleManifest assetBundleManifest = manifestAb.LoadAsset<AssetBundleManifest>("assetbundlemanifest");
            string[] bundles = assetBundleManifest.GetAllAssetBundles();

            int length = bundles.Length;
            for (int i = 0; i < length; i++)
            {
                EAssetBundleFileInfo info = new EAssetBundleFileInfo(bundles[i]);
                info.all_depends.AddRange(assetBundleManifest.GetAllDependencies(info.ab_name));
                _assetBundleFileInfos.Add(info);
            }
            manifestAb.Unload(true);
        }

        /// <summary>
        /// 分析AssetBundle中有多少Asset资源
        /// </summary>
        public static void _analyze_bundle_files()
        {
            // 1.A 文件 被哪些文件引用
            foreach (var info in _assetBundleFileInfos)
            {
                List<string> beDepends = new List<string>();
                foreach (var info2 in _assetBundleFileInfos)
                {
                    if (info2.ab_name == info.ab_name) continue;

                    if (info2.all_depends.Contains(info.ab_name))
                    {
                        beDepends.Add(info2.ab_name);
                    }
                }
                info.be_depends.Clear();
                info.be_depends.AddRange(beDepends.ToArray());
            }

            int index = 0;
            // 分析assetbundle下asset引用信息
            foreach (var info in _assetBundleFileInfos)
            {
                index++;
                EditorUtility.DisplayProgressBar("分析Bundle的Asset资源", info.ab_name, 1.0f * index / _assetBundleFileInfos.Count);
                _analyze_assetbundle_file(info);
            }
            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
        }

        public static void _create_report()
        {
            string dir = DateTime.Now.ToString("yyyy_MM_dd__HHmm");
            string reportDir = Application.dataPath + "/../Report/" + dir;
            if (!Directory.Exists(reportDir))
                Directory.CreateDirectory(reportDir);
            AssetBundleReport.CreateReport(reportDir);
            AssetBundleDepReport.CreateReport(reportDir);
            AssetReport.CreateReport(reportDir);
            TextureReport.CreateReport(reportDir);
            AnimationClipReport.CreateReport(reportDir);
            MeshReport.CreateReport(reportDir);

            EditorUtility.DisplayDialog("生成报告:", "报告路径:" + reportDir, "OK");
        }

        /// <summary>
        /// 分析AssetBundle中有多少Asset资源
        /// </summary>
        public static void _analyze_assetbundle_file(EAssetBundleFileInfo assetbundleFileInfo)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(assetbundleFileInfo.file_path);
            string resualName = string.Empty;
            try
            {
                if (ab == null)
                {
                    Debug.LogErrorFormat("[{0}]加载失败:", assetbundleFileInfo.file_path);
                    return;
                }
                Object[] objs = ab.LoadAllAssets<Object>();
                Object[] collectDepends = EditorUtility.CollectDependencies(objs);
                int length = collectDepends.Length;
                for (int i = 0; i < length; i++)
                {
                    if (!collectDepends[i]) continue;
                    resualName = collectDepends[i].name;
                    _analyze_asset_object(collectDepends[i], assetbundleFileInfo);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[" + assetbundleFileInfo.ab_name + "]_[" + resualName + "]_" + e.Message);
            }
            finally
            {
                if (ab != null) ab.Unload(true);
            }
        }

        private static PropertyInfo _inspectorMode;
        // 分析Asset Object信息
        public static void _analyze_asset_object(Object assetObject, EAssetBundleFileInfo assetbundleFileInfo)
        {
            bool inBuilt = false;
            // 1.检测类型需要符合要求
            E_AssetType assetType = AssetBundleAnallyzeObject.CheckObject(assetObject, assetbundleFileInfo, ref inBuilt);
            if (assetType == E_AssetType.none) return;

            if (_inspectorMode == null)
            {
                _inspectorMode = typeof(SerializedObject).GetProperty("inspectorMode", BindingFlags.NonPublic | BindingFlags.Instance);
            }

            // 得到guid
            SerializedObject serializedObject = new SerializedObject(assetObject);
            if (_inspectorMode != null)
                _inspectorMode.SetValue(serializedObject, InspectorMode.Debug, null);
            SerializedProperty pathIdProp = serializedObject.FindProperty(EAssetBundleConst.LOCAL_ID_DENTFIER_IN_FILE);
            if (pathIdProp == null)
            {
                Debug.LogError("得到Id失败:" + assetbundleFileInfo.ab_name + "_" + assetObject);
                return;
            }
            long guid = pathIdProp.longValue;

            if (assetbundleFileInfo.IsAssetContain(guid))
            {
                Debug.LogAssertionFormat("[{0}]已经存在了[{1}]资源", assetbundleFileInfo.ab_name, assetObject.name);
                serializedObject.Dispose();
                return;
            }


            EAssetFileInfo assetFileInfo = FindAssetFile(guid);
            assetFileInfo._memorysize = EMemorySizeHelper.GetRuntimeMemorySize(assetObject);
            assetFileInfo.InitAsset = true;
            assetFileInfo._assetName = assetObject.name;
            assetFileInfo._assetType = assetType;
            assetFileInfo._inBuilt = inBuilt;
            if (AssetBundleAnallyzeObject.fun_map.ContainsKey(assetType))
                assetFileInfo._propertys = AssetBundleAnallyzeObject.fun_map[assetType].Invoke(assetObject, serializedObject);

            // AssetBundle包含了Asset资源
            assetbundleFileInfo.AddDepAssetFile(assetFileInfo);
            // Asset被指定的AssetBundle引用
            assetFileInfo._includedBundles.Add(assetbundleFileInfo);
            serializedObject.Dispose();
        }

        #endregion
    }
}

