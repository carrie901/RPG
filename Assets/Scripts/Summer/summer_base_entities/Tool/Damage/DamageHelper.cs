﻿namespace Summer
{
    public class DamageHelper
    {
        public static void ExportDamageToTarget(BaseEntity caster, BaseEntity target, float damge)
        {
            // 目标已经死亡
            if (target.IsDead()) return;
            // 目标被击中
            target.RaiseEvent(E_Entity_Event.on_be_hurt, null);
            // 被打中收到多少伤害
            target.RaiseEvent(E_Entity_Event.on_be_attack_damage, null);
        }
    }
}