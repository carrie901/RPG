using UnityEngine;
using System.Collections;

//=============================================================================
/// Author : mashao
/// CreateTime : 2018-1-19 17:26:48
/// FileName : EffectAttributeMaxHp.cs
//=============================================================================

/*
namespace Summer
{
    
    /// <summary>
    /// 修改属性 属性为生命最大值
    /// 最大生命的规则是 上限提升，同时当前血量+ 提升的数值，
    /// 回退的时候，只回退属性，如果数值没有超过最大血量那么数值不变，如果数值超过最大血量。那么=最大血量
    /// </summary>
	public class EffectAttributeMaxHp : EffectAttribute
    {
        public override bool _on_excute()
        {
           /* PropertyIntParam property_value = _owner.property.FindProperty(_param._region);
            int old_value = property_value.Value;

            BuffHelper.Calc(property_value, _param._calc_type, (int)_param._calc_data);

            // 当前血量的修改
            int old_curr_hp_value = _owner.property.GetHp();
            // 最大血量的增量
            int add_value = property_value.Value - old_value;
            DamageInterFace.AddHp(_owner, add_value);
            int new_curr_hp_value = _owner.property.GetHp();
            Log("由于最大血量的改变，导致当前血量收到影响，当前血量:before:[{0}],after[{1}]", old_curr_hp_value, new_curr_hp_value);

            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_effect_excute, this);
            Log("Effect Excute---> 属性更新 attribute:[{0}],before:[{1}],after[{2}]", _param._region, old_value, property_value.Value);
            _cumulative_data += _param._calc_data;#1#
            return false;
        }

        public override void _on_reverse()
        {
            /*base._on_reverse();
            int old_value = _owner.property.GetHp();
            _owner.NormalizeHp();
            int new_value = _owner.property.GetHp();
            Log("由于最大血量的改变，导致当前血量收到影响，当前血量:before:[{0}],after[{1}]", old_value, new_value);#1#
        }

    }
}
*/
