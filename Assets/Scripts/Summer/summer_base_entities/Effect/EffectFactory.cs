using System;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class EffectFactory
    {

        public static EffectFactory instance = new EffectFactory();
        public Dictionary<int, Dictionary<int, Type>> _effect_map = new Dictionary<int, Dictionary<int, Type>>();

        public EffectScriptableObject _data;
        private EffectFactory()
        {
            _data = Resources.Load<EffectScriptableObject>("EffectFactory");
            /* _effect_map.Clear();

             #region 添加属性
             // 属性
             Dictionary<int, Type> attribute_map = new Dictionary<int, Type>();
             foreach (E_CharAttributeRegion region in Enum.GetValues(typeof(E_CharAttributeRegion)))
             {
                 if (region == E_CharAttributeRegion.none || region == E_CharAttributeRegion.max) continue;
                 if (region == E_CharAttributeRegion.move_speed)
                     attribute_map.Add((int)region, typeof(EffectAttributeMoveSpeed));
                 else if (region == E_CharAttributeRegion.max_hp)
                     attribute_map.Add((int)region, typeof(EffectAttributeMaxHp));
                 else
                     attribute_map.Add((int)region, typeof(EffectAttribute));
             }

             _effect_map.Add((int)E_EffectType.attribute, attribute_map);

             #endregion

             #region 添加数值
             // 数值
             Dictionary<int, Type> value_map = new Dictionary<int, Type>
             {
                 {(int) E_EffectValue.peer_less_less, typeof (EffectPeerLess)},
                 {(int) E_EffectValue.peer_less_more, typeof (EffectPeerLess)},
                 {(int) E_EffectValue.damage, typeof (EffectDamage)},
                 {(int) E_EffectValue.health, typeof (EffectHealth)}
             };

             _effect_map.Add((int)E_EffectType.value, value_map);

             #endregion

             #region 添加状态

             // 状态
             Dictionary<int, Type> state_map = new Dictionary<int, Type>
             {
                 {(int) E_EffectState.frozen, typeof (EffectFrozen)},
                 {(int) E_EffectState.invincible, typeof (EffectInvincible)},
                 {(int) E_EffectState.seal, typeof (EffectSealSkill)},
                 {(int) E_EffectState.fear, typeof (EffectFearSkill)}
             };
             _effect_map.Add((int)E_EffectType.change_state, state_map);

             #endregion

             #region 模型

             // 改变模型
             Dictionary<int, Type> model_map = new Dictionary<int, Type>
             {
                 {(int) E_EffectModel.model, typeof (EffectChangeModel)},
                 {(int) E_EffectModel.weapon, typeof (EffectChangeWeapon)}
             };
             _effect_map.Add((int)E_EffectType.change_model, model_map);

             #endregion*/
        }

        /* public SEffect Create(E_EffectType effect_type, int sub_type, string[] param, BaseEntity owner, Buff parent)
         {
             if (effect_type == 0) return null;

             int e_type = (int)effect_type;
             EffCnf cnf = new EffCnf("", effect_type, sub_type, param);

             Dictionary<int, Type> effects;
             _effect_map.TryGetValue(e_type, out effects);
             if (effects == null)
             {
                 EffectLog.Error("没有大类型为[{0}]的效果", e_type);
                 return null;
             }

             Type type;
             effects.TryGetValue(sub_type, out type);
             if (type == null)
             {
                 EffectLog.Error("没有大类型为[{0}],小类型为[{1}]的效果", e_type, sub_type);
                 return null;
             }
             SEffect effect = Activator.CreateInstance(type) as SEffect;

             if (effect != null)
                 effect.Init(cnf, owner, parent);
             else
                 BuffLog.Log("找不到对应的效果" + effect_type);
             return effect;
         }*/

        public BaseEffect Create(BaseBuff target_buff, EffectTemplateInfo info)
        {
            string type_name = "Summer.EffectAttribute";
            Type type = Type.GetType(type_name);
            BaseEffect effect = Activator.CreateInstance(type) as BaseEffect;
            if (effect == null) return null;
            effect.OnInit(target_buff._bid, info);
            return effect;
        }
    }
}
