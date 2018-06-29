
using UnityEngine;

namespace Summer
{

    /// <summary>
    /// 效果类型=属性
    /// </summary>
    public class EffectAttributeParam : I_EffectParam
    {
        public E_CharAttributeRegion _region;               // 类型
        public E_DataUpdateType _calc_type;             // +1 or +1% 百分比
        public float _calc_data;                            // value 数值

        public string value_text = string.Empty;            // 描述文本

        public void ParseParam(EffCnf cnf)
        {
            _region = (E_CharAttributeRegion)cnf.sub_type;

            int calc_type;
            int.TryParse(cnf.datas[0], out calc_type);
            _calc_type = (E_DataUpdateType)calc_type;

            float.TryParse(cnf.datas[1], out _calc_data);
            _parse_value_text();
        }

        public void _parse_value_text()
        {
            value_text = string.Empty;
            if (_calc_type == E_DataUpdateType.multiply_plus)
            {
                if (_calc_data > 0)
                    value_text = string.Format("+{0}%", Mathf.Abs(_calc_data));
                else if (_calc_data < 0)
                    value_text = string.Format("-{0}%", Mathf.Abs(_calc_data));
            }
            else if (_calc_type == E_DataUpdateType.plus)
            {
                if (_calc_data > 0)
                    value_text = string.Format("+{0}", Mathf.Abs(_calc_data));
                else if (_calc_data < 0)
                    value_text = string.Format("-{0}", Mathf.Abs(_calc_data));
            }

        }

        public string GetValueText()
        {
            return value_text;
        }
    }

    public class EffectAttributeParamNew : I_EffectParamNew
    {
        public int value;
        public void ParseParam(string text)
        {

        }
    }
}
