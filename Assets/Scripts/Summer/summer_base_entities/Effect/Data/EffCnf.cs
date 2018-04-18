//=============================================================================
// Author : mashao
// CreateTime : 2018-1-10 19:53:15
// FileName : EffectCnf.cs
//=============================================================================

namespace Summer
{
    public class EffCnf
    {
        public string desc;                         // 描述
        public E_EffectType effect_type;            // 效果类型
        public int sub_type;                        // 效果的子类型
        public string[] datas;                      // 参数类型数据

        public EffCnf(string text, E_EffectType type, int sub, string[] tmps)
        {
            desc = text;
            effect_type = type;
            sub_type = sub;
            datas = tmps;
        }
    }
}
