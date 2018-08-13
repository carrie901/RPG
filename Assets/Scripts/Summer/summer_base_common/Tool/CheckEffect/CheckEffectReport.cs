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
        public float interval_eff_check = 5;
        public CheckEffectReportCnf _curr_report;
        public Dictionary<string, CheckEffectReportCnf> report_map = new Dictionary<string, CheckEffectReportCnf>();
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
            foreach (var report in report_map)
            {
                yield return StartCoroutine(AnalyzeSingleFx(report.Value));
            }
            // 3. 输出文本
            WriteCsv();
        }

        public void ReadCsv()
        {
            string text = File.ReadAllText(CheckEffectConst.effect_texture_report_path);
            string[] contents = text.ToStrs(StringHelper.split_huanhang);
            int length = contents.Length;
            for (int i = 1; i < length; i++)
            {
                String[] info = contents[i].Split(',');
                if (info.Length < 4) continue;
                CheckEffectReportCnf report = new CheckEffectReportCnf();

                report.eff_name = info[0];
                report.asset_path = info[3];
                report.tex_info = info[4];
                report.tex_mem_bytes = float.Parse(info[1]);
                report.tex_mem_count = int.Parse(info[2]);
                report_map.Add(report.eff_name, report);
            }
        }

        public void WriteCsv()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(CheckEffectReportCnf.ToTopDes());
            foreach (var report in report_map)
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
            _curr_report = cnf;
            System.GC.Collect();
            AsyncOperation ao = Resources.UnloadUnusedAssets();
            yield return ao;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;

            // 1.加载的时间
            float t1 = Time.realtimeSinceStartup;
            string new_path = _curr_report.asset_path;
            GameObject eff_go = AssetDatabase.LoadAssetAtPath<GameObject>(new_path);
            float t2 = Time.realtimeSinceStartup;
            _curr_report.load_time = (int)((t2 - t1) * 1000);

            if (eff_go == null) yield break;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;

            // 2.粒子
            _curr_report.total_ps_count = eff_go.GetComponentsInChildren<ParticleSystem>(true).Length;
            // 3.渲染器个数
            Renderer[] eff_renderers = eff_go.GetComponentsInChildren<Renderer>();
            // 4.材质球个数
            Dictionary<Material, bool> eff_materials = new Dictionary<Material, bool>();
            int length = eff_renderers.Length;
            for (int i = 0; i < length; i++)
            {
                bool has;
                Material r = eff_renderers[i].sharedMaterial;
                if (r != null && !eff_materials.TryGetValue(r, out has))
                    eff_materials.Add(r, true);
            }
            // 材质球个数
            _curr_report.material_count = eff_materials.Count;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;

            // 5.实例化时间
            t2 = Time.realtimeSinceStartup;
            GameObject go = Instantiate(eff_go);
            float t3 = Time.realtimeSinceStartup;

            _curr_report.inst_time = (int)((t3 - t2) * 1000);

            float t4 = 0;
            // 6.监控DrawCall
            int old_dc = UnityStats.drawCalls;
            int max_dc = old_dc;

            int old_triangles = UnityStats.triangles;
            int max_triangles = old_triangles;
            while (t4 < interval_eff_check)
            {
                float dt = Time.deltaTime;
                if (UnityStats.drawCalls > max_dc)
                    max_dc = UnityStats.drawCalls;
                t4 += dt;
                if (UnityStats.triangles > max_triangles)
                    max_triangles = UnityStats.triangles;
                yield return CoroutineConst.wait_for_end_of_frame;
            }
            _curr_report.dc = max_dc - old_dc;
            _curr_report.triangles = max_triangles - old_triangles;
            yield return CoroutineConst.wait_for_end_of_frame;
            yield return CoroutineConst.wait_for_end_of_frame;
            Object.DestroyImmediate(go);
            yield return CoroutineConst.wait_for_end_of_frame;
        }

        #endregion
    }
}
#endif