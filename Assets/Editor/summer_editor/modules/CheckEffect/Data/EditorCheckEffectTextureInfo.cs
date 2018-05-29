using System.Collections.Generic;
using UnityEngine;
//=============================================================================
/// Author : mashao
/// CreateTime : 2018-2-9 11:13:6
/// FileName : EditorCheckEffectTextureInfo.cs
//=============================================================================

namespace SummerEditor
{
    /// <summary>
    /// 特效的名称，占用内存，纹理个数
    /// </summary>
    public class EditorCheckEffectTextureInfo
    {
        public string EffectName { get; set; }
        public float TexMemSize { get; private set; }
        public int TexCount { get; private set; }
        public Dictionary<string, float> tex_mem_map = new Dictionary<string, float>();

        public void AddTexList(Object[] objs)
        {
            foreach (Object t in objs)
            {
                if (t is Texture2D)
                {
                    TexCount++;
                    float mem = (UnityEngine.Profiling.Profiler.GetRuntimeMemorySize(t) / 2f);
                    TexMemSize += mem;
                    tex_mem_map.Add(t.name, mem);
                }
            }
        }

        public string ToDes()
        {
            string tex_mem = string.Empty;
            foreach (var t in tex_mem_map)
            {
                tex_mem += t.Key + ":" + (int)(t.Value / 1024) + "|";
            }
            return string.Format("{0},{1},{2},{3}", EffectName, TexMemSize / 1024f, TexCount, tex_mem);
        }
    }
}
