using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Reflection;
using Summer;
using Object = UnityEngine.Object;

namespace SummerEditor
{
    public class AssetBundleAnalyzeManager
    {
        protected static List<EAssetBundleFileInfo> _assetBundleFileInfos                       // 获取所有的AB文件信息                    
            = new List<EAssetBundleFileInfo>();
        protected static Dictionary<EAssetFileInfoKey, EAssetFileInfo> _assetFileInfos                       // 所有的Asset的资源信息
            = new Dictionary<EAssetFileInfoKey, EAssetFileInfo>();

        public static void Analyze()
        {
            if (!EPathHelper.IsExitDirectory(EAssetBundleConst.AssetbundleDirectory)) return;
            // 收集AB的信息
            AnalyzeCollectBundles();
            AnalyzeBundleFiles();
            CreateReport();
            Clear();
        }

        #region public 

        public static Dictionary<EAssetFileInfoKey, EAssetFileInfo> FindAssetFiles()
        {
            return _assetFileInfos;
        }

        public static List<EAssetBundleFileInfo> FindAssetBundleFiles()
        {
            return _assetBundleFileInfos;
        }

        public static EAssetFileInfoKey sKey = new EAssetFileInfoKey();
        public static EAssetFileInfo FindAssetFile(long guid, long size, E_AssetType type)
        {
            if (_assetFileInfos == null)
            {
                _assetFileInfos = new Dictionary<EAssetFileInfoKey, EAssetFileInfo>();
            }

            EAssetFileInfo info;
            sKey.Guid = guid;
            sKey.AssetType = type;
            sKey.Size = size;
            if (!_assetFileInfos.TryGetValue(sKey, out info))
            {
                info = new EAssetFileInfo(guid);
                EAssetFileInfoKey newKey = sKey.CopyInfo();
                _assetFileInfos.Add(newKey, info);
            }
            return info;
        }

        public static void Clear()
        {
            _assetFileInfos.Clear();
            _assetBundleFileInfos.Clear();
            EditorUtility.UnloadUnusedAssetsImmediate();
            Resources.UnloadUnusedAssets();
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();

            GC.Collect();
        }

        #endregion

        #region private

        /// <summary>
        /// 收集bundle资源
        /// </summary>
        private static void AnalyzeCollectBundles()
        {
            _assetBundleFileInfos.Clear();
            _assetFileInfos.Clear();
            /* string manifestPath = EAssetBundleConst.ManifestPath;
             AssetBundle manifestAb = AssetBundle.LoadFromFile(manifestPath);

             Debug.AssertFormat(manifestAb != null, "manifest_ab 加载失败,路径地址:[{0}]", manifestPath);
             if (manifestAb == null) return;

             AssetBundleManifest assetBundleManifest = manifestAb.LoadAsset<AssetBundleManifest>("assetbundlemanifest");
             string[] bundles = assetBundleManifest.GetAllAssetBundles();*/

            List<string> temp = new List<string>();
            List<FileInfo> files = FileHelper.GetAllFiles(Application.streamingAssetsPath + "/BundleResources");
            for (int i = 0; i < files.Count; i++)
            {
                FileInfo fileInfo = files[i];
                if (fileInfo.Extension == ".meta") continue;
                if (fileInfo.Extension == ".manifest") continue;
                string t = fileInfo.FullName;
                t = t.Replace("F:\\RPG\\Assets\\StreamingAssets\\BundleResources", "");
                t = t.Replace("\\", "/");
                temp.Add(t);
            }
            string[] bundles = temp.ToArray();
            for (int i = 0; i < bundles.Length; i++)
            {
                EAssetBundleFileInfo info = new EAssetBundleFileInfo(bundles[i]);
                _assetBundleFileInfos.Add(info);
            }
            /*            int length = bundles.Length;
                        for (int i = 0; i < length; i++)
                        {
                            EAssetBundleFileInfo info = new EAssetBundleFileInfo(bundles[i]);
                            if (bundles[i].Contains("11002"))
                            {
                                uint crc;
                                Hash128 hash1;
                                Hash128 hash=assetBundleManifest.GetAssetBundleHash(bundles[i]);

                                BuildPipeline.GetCRCForAssetBundle(bundles[i], out crc);
                                BuildPipeline.GetHashForAssetBundle(bundles[i], out hash1);
                                UnityEngine.Debug.Log(hash);
                                UnityEngine.Debug.Log(hash1);
                                UnityEngine.Debug.Log(crc);
                                UnityEngine.Debug.Log("=======================================================");
                            }

                            info._allDepends.AddRange(assetBundleManifest.GetAllDependencies(info.AbName));
                            _assetBundleFileInfos.Add(info);
                        }
                        manifestAb.Unload(true);*/
        }

        /// <summary>
        /// 分析AssetBundle中有多少Asset资源
        /// </summary>
        private static void AnalyzeBundleFiles()
        {
            // 1.A 文件 被哪些文件引用
            foreach (var info in _assetBundleFileInfos)
            {
                List<string> beDepends = new List<string>();
                foreach (var info2 in _assetBundleFileInfos)
                {
                    if (info2.AbName == info.AbName) continue;

                    if (info2._allDepends.Contains(info.AbName))
                    {
                        beDepends.Add(info2.AbName);
                    }
                }
                info._beDepends.AddRange(beDepends.ToArray());
            }

            int index = 0;
            // 分析assetbundle下asset引用信息
            foreach (var info in _assetBundleFileInfos)
            {
                index++;
                EditorUtility.DisplayProgressBar("分析Bundle的Asset资源", info.AbName, 1.0f * index / _assetBundleFileInfos.Count);
                AnalyzeAssetbundleFile(info);
            }
            EditorUtility.ClearProgressBar();
            Resources.UnloadUnusedAssets();
        }

        private static void CreateReport()
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
            AssetRepeatReport.CreateReport(reportDir);
            EditorUtility.DisplayDialog("生成报告:", "报告路径:" + reportDir, "OK");
        }

        /// <summary>
        /// 分析AssetBundle中有多少Asset资源
        /// </summary>
        private static void AnalyzeAssetbundleFile(EAssetBundleFileInfo assetbundleFileInfo)
        {
            AssetBundle ab = AssetBundle.LoadFromFile(assetbundleFileInfo.FilePath);
            if (assetbundleFileInfo.FilePath.Contains("ship/ship_100"))
            {
                Debug.Log("1");
            }
            string resualName = string.Empty;
            try
            {
                if (ab == null)
                {
                    Debug.LogErrorFormat("[{0}]加载失败:", assetbundleFileInfo.FilePath);
                    return;
                }
                Object[] objs = ab.LoadAllAssets<Object>();
                Object[] collectDepends = EditorUtility.CollectDependencies(objs);
                int length = collectDepends.Length;
                for (int i = 0; i < length; i++)
                {
                    if (!collectDepends[i]) continue;
                    resualName = collectDepends[i].name;
                    AnalyzeAssetObject(collectDepends[i], assetbundleFileInfo);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("[" + assetbundleFileInfo.AbName + "]_[" + resualName + "]_" + e.Message);
            }
            finally
            {
                if (ab != null) ab.Unload(true);
            }
        }

        private static PropertyInfo _inspectorMode;
        // 分析Asset Object信息
        private static void AnalyzeAssetObject(Object assetObject, EAssetBundleFileInfo assetbundleFileInfo)
        {
            bool inBuilt = false;
            // 1.检测类型需要符合要求
            E_AssetType assetType = AssetBundleAnallyzeObject.CheckObject(assetObject, assetbundleFileInfo, ref inBuilt);

            long size = EMemorySizeHelper.GetRuntimeMemorySize(assetObject);
            if (assetType == E_AssetType.NONE) return;

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
                Debug.LogError("得到Id失败:" + assetbundleFileInfo.AbName + "_" + assetObject);
                return;
            }
            long guid = pathIdProp.longValue;

            List<KeyValuePair<string, System.Object>> propertys = new List<KeyValuePair<string, object>>();
            if (AssetBundleAnallyzeObject._funMap.ContainsKey(assetType))
                propertys.AddRange(AssetBundleAnallyzeObject._funMap[assetType].Invoke(assetObject, serializedObject));

            if (assetbundleFileInfo.IsAssetContain(guid))
            {
                Debug.LogAssertionFormat("[{0}]已经存在了[{1}]资源", assetbundleFileInfo.AbName, assetObject.name);
                serializedObject.Dispose();
                return;
            }
            if (assetType == E_AssetType.TEXTURE)
                size = (long)propertys[5].Value;
            EAssetFileInfo assetFileInfo = FindAssetFile(guid, size, assetType);
            assetFileInfo._memorysize = size;
            assetFileInfo.InitAsset = true;
            assetFileInfo._assetName = assetObject.name;
            assetFileInfo._assetType = assetType;
            assetFileInfo._inBuilt = inBuilt;
            assetFileInfo._propertys = propertys;


            if (assetObject.name.Contains("11001_N") && assetType == E_AssetType.TEXTURE)
            {
                string path = AssetDatabase.GetAssetPath(assetObject);
                Debug.Log("guid:" + guid + "_" + assetbundleFileInfo.AbName + "_" + EMemorySizeHelper.GetRuntimeMemorySize(assetObject) + "_" + size + "_" + path);

            }

            // AssetBundle包含了Asset资源
            assetbundleFileInfo.AddDepAssetFile(assetFileInfo);
            // Asset被指定的AssetBundle引用
            assetFileInfo.AddParent(assetbundleFileInfo);
            serializedObject.Dispose();

            AssetRepeatReport.AddAssetFile(assetFileInfo);
        }

        #endregion
    }
}

