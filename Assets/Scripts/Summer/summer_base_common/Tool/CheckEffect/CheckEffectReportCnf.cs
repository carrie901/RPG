using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-1 11:29:38
// FileName : CheckEffectReportCnf.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 检测特效性能
    /// 特效的相关属性
    /// </summary>
    public class CheckEffectReportCnf
    {
        public string eff_name = string.Empty; // 特效名称
        public int load_time; // 加载时间 单位ms
        public int inst_time; // 实例化时间 单位ms
        public int dc; // drawcall
        public int triangles; // 三角面
        public int material_count; // 材质球数量
        public int total_ps_count; // 粒子系统个数
        public float tex_mem_bytes; // 贴图内存
        public int tex_mem_count; // 贴图个数
        public string tex_info = string.Empty;
        public int animation_count;    // 动画个数
        public string asset_path = "";


        public const string EFFECT_NAME = "特效名称";
        public const string LOAD_TIME = "加载时间";
        public const string INST_TIME = "实例化时间";
        public const string TOTAL_PS_COUNT = "发射器个数";
        public const string MATERIAL_COUNT = "材质球个数";
        public const string TEX_MEM_BYTES = "贴图内存Kb";
        public const string TEX_MEM_COUNT = "贴图个数";
        public const string DRAWCALL = "drawcall";
        public const string TRIANGLES = "三角面";
        public const string TEX_INFO = "纹理信息总结";
        public const string ANIM_COUNT = "动画个数";

        public const string ASSET_PATH = "路径地址";

        public string ToDes()
        {
            string result = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                eff_name, load_time, inst_time,
                total_ps_count, material_count, tex_mem_count,
                tex_mem_bytes.ToString("0.0"), dc, triangles,
                tex_info, animation_count,asset_path);
            return result;
        }

        public static string ToTopDes()
        {
            string result = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                EFFECT_NAME, LOAD_TIME, INST_TIME,
                TOTAL_PS_COUNT, MATERIAL_COUNT, TEX_MEM_COUNT,
                TEX_MEM_BYTES, DRAWCALL, TRIANGLES,
                TEX_INFO, ANIM_COUNT, ASSET_PATH);
            return result;
        }
    }
}
