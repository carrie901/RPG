using UnityEngine;
using System.Collections;
using Summer;

/// <summary>
/// 数值更新的参数，有别于属性更新参数
/// </summary>
public class BuffParamValueData : I_BuffParam
{
    public E_CharAttributeType _region;                //针对的属性
    public E_CharDataUpdateType _calc_type;         //+1 or +1%
    public int _calc_data;                          //value
    public E_CharDataByValue by_value;              // 当前值/最大值
    public bool _is_positive;                       //正负
    public void ParseParam(string param)
    {
        string[] contents = StringHelper.SplitString(param, "/");
        by_value = (E_CharDataByValue)int.Parse(contents[3]);
        _region = (E_CharAttributeType)int.Parse(contents[1]);
        _calc_type = (E_CharDataUpdateType)int.Parse(contents[2]);
        _calc_data = int.Parse(contents[4]);
        _is_positive = int.Parse(contents[0]) == 0;
        if (!_is_positive)
            _calc_data = 0 - _calc_data;

    }
}
