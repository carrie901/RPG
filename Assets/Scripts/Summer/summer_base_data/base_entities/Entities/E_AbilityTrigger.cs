namespace Summer
{
	/// <summary>
    /// Buff触发点
    /// </summary>
    public enum E_AbilityTrigger
    {
        none = 0,

        buff_be_hurt = 7,                           // 在受到攻击的时候触发，大多盾类技能由此而生
        buff_on_hit = 8,
        buff_before_killed = 21,                    // 必死前
        buff_after_killed = 22,                     // 当杀死一个角色的时候，恢复自身X%的HP，这时候你就需要这个回调点，精确的在角色死亡后发生

        on_trigger_normal_attck = 23,               // 普攻触发

        on_hit = 24,                                // 击中目标
        on_hid_damage = 25,                           // 击中目标造成的伤害
        //on_attack_enemy = 24,                       // 攻击敌人     
        //on_attack_enemy_before = 25,                // 攻击别人之前
        //on_attack_enemy_damage = 26,                // 攻击别人造成伤害
        on_be_hurt = 27,                            // 收到伤害之前
        on_be_attack_damage,                        // 被攻击收到伤害
        on_owner_died,                              // 拥有该技能的单位死亡
        on_owner_spawned,                           // 拥有该技能的单位出生
        on_projectile_hit_unit,                     // 当投射物与有效单位接触


        // Buff 相关内部的一些响应
        buff_on_tick = 101,                         //一定时间触发一次
        buff_on_attach = 102,
        buff_add_layer = 103,
        buff_layer_max = 104,
        buff_remove_layer = 105,
        buff_on_detach = 106,
        max,

    }
}