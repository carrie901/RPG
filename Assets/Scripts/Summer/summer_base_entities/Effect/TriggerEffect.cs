using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 1.监听外部的消息，比如攻击前，受伤后，等一切由外部触发的消息
    /// 2.执行Buff内部的消息，添加，加层，移层，移除，OnTick TODO 剥离内部的回调，把外部回调和内部回调统一化
    /// 3.触发消息，比如提前结束buff(防御盾，抵消攻击次数)的Buff提前结束
    /// TODO 针对Effect触发效果结束后，同事触发结束消息
    /// 如何让Effect更加事务单一性
    /// </summary>
    public class TriggerEffect
    {
        #region 静态内部的触发类型
        public static Dictionary<E_AbilityTrigger, int> _self_trigger_map
            = new Dictionary<E_AbilityTrigger, int>()
            {
                { E_AbilityTrigger.buff_on_attach,1 },
                { E_AbilityTrigger.buff_on_tick,1},
                { E_AbilityTrigger.buff_add_layer,1 },
                { E_AbilityTrigger.buff_layer_max,1 },
                { E_AbilityTrigger.buff_remove_layer,1},
                { E_AbilityTrigger.buff_on_detach,1 }
            };
        #endregion

        public static E_EffectType spec_effect_type
            = E_EffectType.absorb_damage;          // 吸收伤害，会提前结束

        public SEffect _effect;                         // 效果
        public BaseEntity _owner;         // 效果的拥有者
        public E_AbilityTrigger _trigger;               // 触发类型
        public Buff _parent;                            // 所属buff

        public TriggerEffect(Buff parent, BaseEntity owner)
        {
            _parent = parent;
            _owner = owner;
        }

        public void InitData(E_EffectType effect_type, int sub_type, string[] param, E_AbilityTrigger trigger)
        {
            _trigger = trigger;
            _effect = EffectFactory.instance.Create(effect_type, sub_type, param, _owner, _parent);
        }

        #region Attach / Detach
        public void OnAttach()
        {
            /*if (_effect == null || _owner == null)
            {
                SEffect.Error("TriggerEffect OnAttach 效果为空|效果持有者为空");
                return;
            }
            _effect.OnAttach();
            if (_self_trigger_map.ContainsKey(_trigger)) return;
            Buff.Log("[{0}] Register :[{1}] event--->[{2}]", _owner.ToDes(), _trigger, _effect.ToDes());
            _owner.RegisterHandler(_trigger, OnExcute);*/
        }

        public void OnDetach()
        {
            /*if (_effect == null || _owner == null)
            {
                SEffect.Error("TriggerEffect OnDetach effect is null | owner is null");
                return;
            }
            _effect.OnDetach();
            if (_self_trigger_map.ContainsKey(_trigger)) return;

            Buff.Log("[{0}] UnReigster :[{1}]", _owner.ToDes(), _trigger);
            _owner.UnRegisterHandler(_trigger, OnExcute);*/
        }

        #endregion

        #region 执行Effect的效果
        public void OnExcute(EventSetData param)
        {
            SEffect.Log("Effect Raise :[{0}] Event--> Excute [{1}] effect", _trigger, _effect._cnf.desc);
            if (_effect == null) return;
            _effect.ResetInfo(param);

            bool result = _effect.OnExcute();

            // TODO 2017.11.2丑陋的代码
            if (result && spec_effect_type == _effect._cnf.effect_type && _parent != null)
            {
                _parent.RemoveSelf();
            }
        }

        public void OnReverse()
        {

            if (_effect == null) return;
            SEffect.Log("Buff Raise Reverse Buff Name:[{0}] Event--> Excute [{1}] effect", _trigger, _effect._cnf.desc);
            _effect.OnReverse();
        }

        #endregion

        public bool IsTrigger(E_AbilityTrigger trigger)
        {
            return _trigger == trigger;
        }

    }
}
