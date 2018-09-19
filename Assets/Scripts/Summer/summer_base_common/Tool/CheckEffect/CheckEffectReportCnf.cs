using System.Collections.Generic;

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
        public string _effName = string.Empty;                              // 特效名称
        public int _loadTime;                                               // 加载时间 单位ms
        public int _instTime;                                               // 实例化时间 单位ms
        public int _dc;                                                     // drawcall
        public int _triangles;                                              // 三角面
        public int _materialCount;                                          // 材质球数量
        public int _totalPsCount;                                           // 粒子系统个数
        public int _texMemBytes;                                            // 贴图内存
        public int _texMemCount;                                            // 贴图个数
        public string _texInfo = string.Empty;
        public int _animationCount;                                         // 动画个数
        public string _assetPath = "";

        public List<string> _texs = new List<string>();
        public List<int> _texMems = new List<int>();

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


        public void SetInfo(List<string> info)
        {
            _texs.Clear();
            _effName = info[0];
            _loadTime = int.Parse(info[1]);
            _instTime = int.Parse(info[2]);
            _totalPsCount = int.Parse(info[3]);
            _materialCount = int.Parse(info[4]);
            _texMemCount = int.Parse(info[5]);
            _texMemBytes = int.Parse(info[6]);
            _dc = int.Parse(info[7]);
            _triangles = int.Parse(info[8]);

            string[] texs = info[9].Split('|');
            int length = texs.Length;
            for (int i = 0; i < length; i++)
            {
                if (texs[i].Length == 0) continue;
                string[] content = texs[i].Split(':');
                string str1 = content[0];
                _texs.Add(str1);
                _texMems.Add(int.Parse(content[1]));
            }

            _animationCount = int.Parse(info[10]);
            _assetPath = info[11];
        }

        public string ToDes()
        {
            string result = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                _effName, _loadTime, _instTime,
                _totalPsCount, _materialCount, _texMemCount,
                _texMemBytes.ToString("0.0"), _dc, _triangles,
                _texInfo, _animationCount, _assetPath);
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
