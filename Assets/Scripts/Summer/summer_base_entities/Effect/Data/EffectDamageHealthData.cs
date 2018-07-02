/*

//=============================================================================
// Author : mashao
// CreateTime : 2018-1-12 15:37:27
// FileName : EffectDamageHealthData.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// TODO 特殊性质的参数，不太建议这样的参数配置形式，为了符合策划的需求前行修改为这样
    /// </summary>
    public class EffectDamageHealthData : I_EffectParam
    {
        public int interval_time;
        public float _calc_data;                          // value 数值

        public string value_text = string.Empty;

        public void ParseParam(TextNode text_node)
        {
            /*int.TryParse(cnf.datas[0], out interval_time);
            float.TryParse(cnf.datas[1], out _calc_data);#1#
        }

        public string GetValueText()
        {
            return value_text;
        }
    }
}
*/
