/*namespace Summer
{
    /// <summary>
    /// 修改属性
    /// </summary>
public class EffectAttribute : SEffect
{
    public EffectAttributeParam _param = new EffectAttributeParam();
    public float _cumulative_data;

    public override string GetValueText()
    {
        return _param.GetValueText();
    }

    public override void _on_parse()
    {
        _param.ParseParam(_cnf);
    }

    public override bool _on_excute()
    {

       /* // 1.找到要更新的属性
        PropertyIntParam property_value = _owner.property.FindProperty(_param._region);

        //TODO 2.根据层级计算属性具体伤害值（如果直接计算层级最终的结果。那么再重新增加层级的时候，进行reset操作，再添加）
        int old_value = property_value.Value;

        // 3.按照百分比/固定值更新属性
        BuffHelper.Calc(property_value, _param._calc_type, (int)_param._calc_data);
        // 4.广播数据
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_effect_excute, this);
        //_send_event_data(old_value, property_value.Value);
        Log("Effect Excute---> 属性更新 attribute:[{0}],before:[{1}],after[{2}]", _param._region, old_value, property_value.Value);
        _cumulative_data += _param._calc_data;#1#
        return false;
    }

    public override void _on_reverse()
    {
       /* // 1.找到要更新的属性
        PropertyIntParam property_value = _owner.property.FindProperty(_param._region);

        // 2.根据层级计算属性具体伤害值（如果直接计算层级最终的结果。那么再重新增加层级的时候，进行reset操作，再添加）
        int origin = property_value.Value;

        // 3.按照百分比/固定值更新属性
        BuffHelper.Calc(property_value, _param._calc_type, -(int)_cumulative_data);

        Log("Effect Reverse---> attribute:[{0}],before:[{1}],after[{2}]", _param._region, origin, property_value.Value);#1#
    }
}
}
*/
