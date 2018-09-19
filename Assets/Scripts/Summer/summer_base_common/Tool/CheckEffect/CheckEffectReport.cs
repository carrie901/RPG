#if UNITY_EDITOR

using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Summer.Tool;
using UnityEditor;
using Summer;
using Object = UnityEngine.Object;

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-31 20:5:54
// FileName : CheckEffectReport.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 检测这个特效的Dc
    /// </summary>
	public class CheckEffectReport : MonoBehaviour
    {
        public float _intervalEffCheck = 5;
        public CheckEffectReportCnf _currReport;
        public Dictionary<string, CheckEffectReportCnf> _reportMap = new Dictionary<string, CheckEffectReportCnf>();
        private void Start()
        {
            StartCoroutine(CheckAllEffect());
        }

        #region

        public IEnumerator CheckAllEffect()
        {
            yield return CoroutineConst.GetWaitForSeconds(1f);

            // 1. 读取 特效的列表
            ReadCsv();
            yield return CoroutineConst.GetWaitForSeconds(0.5f);
            // 2. 依次解析特效的dc和三角面
            foreach (var report in _reportMap)
            {
                yield return StartCoroutine(AnalyzeSingleFx(report.Value));
            }
            // 3. 输出文本
            WriteCsv();
        }

        public void ReadCsv()
        {
            string text = File.ReadAllText(CheckEffectConst.effect_texture_report_path);
            string[] contents = text.ToStrs(StringHelper._splitHuanhang);
            int length = contents.Length;
            for (int i = 1; i < length; i++)
            {
                String[] info = contents[i].Split(',');
                if (info.Length < 4) continue;
                CheckEffectReportCnf report = new CheckEffectReportCnf();

                report._effName = info[0];
                report._assetPath = info[3];
                report._texInfo = info[4];
                report._texMemBytes = int.Parse(info[1]);
                report._texMemCount = int.Parse(info[2]);
                _reportMap.Add(report._effName, report);
            }
        }

        public void WriteCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(CheckEffectReportCnf.ToTopDes());
            foreach (var report in _reportMap)
            {
                sb.AppendLine(report.Value.ToDes());
            }
            File.WriteAllText(CheckEffectConst.effect_report_path, sb.ToString());
            EditorApplication.isPlaying = false;
            EditorUtility.DisplayDialog("分析特效性能结束", "查看目录" + Application.dataPath + CheckEffectConst.effect_report_path, "Ok");
        }

        public IEnumerator AnalyzeSingleFx(CheckEffectReportCnf cnf)
        {
            yield return null;
            _currReport = cnf;
            System.GC.Collect();
            AsyncOperation ao = Resources.UnloadUnusedAssets();
            yield return ao;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;

            // 1.加载的时间
            float t1 = Time.realtimeSinceStartup;
            string newPath = _currReport._assetPath;
            GameObject effGo = AssetDatabase.LoadAssetAtPath<GameObject>(newPath);
            float t2 = Time.realtimeSinceStartup;
            _currReport._loadTime = (int)((t2 - t1) * 1000);

            if (effGo == null) yield break;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;

            // 2.粒子
            _currReport._totalPsCount = effGo.GetComponentsInChildren<ParticleSystem>(true).Length;
            // 3.渲染器个数
            Renderer[] effRenderers = effGo.GetComponentsInChildren<Renderer>();
            // 4.材质球个数
            Dictionary<Material, bool> effMaterials = new Dictionary<Material, bool>();
            int length = effRenderers.Length;
            for (int i = 0; i < length; i++)
            {
                bool has;
                Material r = effRenderers[i].sharedMaterial;
                if (r != null && !effMaterials.TryGetValue(r, out has))
                    effMaterials.Add(r, true);
            }
            // 材质球个数
            _currReport._materialCount = effMaterials.Count;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;

            // 5.实例化时间
            t2 = Time.realtimeSinceStartup;
            GameObject go = Instantiate(effGo);
            float t3 = Time.realtimeSinceStartup;

            _currReport._instTime = (int)((t3 - t2) * 1000);

            float t4 = 0;
            // 6.监控DrawCall
            int oldDc = UnityStats.drawCalls;
            int maxDc = oldDc;

            int oldTriangles = UnityStats.triangles;
            int maxTriangles = oldTriangles;
            while (t4 < _intervalEffCheck)
            {
                float dt = Time.deltaTime;
                if (UnityStats.drawCalls > maxDc)
                    maxDc = UnityStats.drawCalls;
                t4 += dt;
                if (UnityStats.triangles > maxTriangles)
                    maxTriangles = UnityStats.triangles;
                yield return CoroutineConst.wait_for_end_of_frame;
            }
            _currReport._dc = maxDc - oldDc;
            _currReport._triangles = maxTriangles - oldTriangles;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;
            Object.DestroyImmediate(go);
            yield return CoroutineConst.wait_for_end_of_frame;
        }

        #endregion
    }
}
#endif