
namespace Summer
{
    /// <summary>
    /// 效果类型
    /// </summary>
    public enum E_EffectType
    {
        none = 0,
        attribute = 1,                      // 属性
        value = 2,                          // 数值
        //damge = 3,                        // 伤害(单独从数值独立处理)
        //health = 4,                       // 治疗
        damage_health = 3,                    // 伤害和治疗
        change_state = 4,                   // 改变状态
        //action = 6,                       // 动作
        absorb_damage = 7,                // 抵消伤害
        //peerless = 8,                     // 无双
        //vampire = 9,                      // 吸血
        change_model = 9,                     // 改变模型，包括人物模型和武器模型
        max,
    }

    /// <summary>
    /// 效果 改变状态
    /// </summary>
    public enum E_EffectState
    {
        none = 0,
        frozen = 1,                     // 冰冻
        invincible = 2,                 // 无敌
        seal = 3,                       // 封印
        fear = 4,                       // 恐惧
        max,
    }

    /// <summary>
    /// 改变模型和武器的颜色
    /// </summary>
    public enum E_EffectModel
    {
        none = 0,
        model = 1,                      // 模型
        weapon = 2                      // 武器
    }


    public enum E_EffectValue
    {
        peer_less_less,
        peer_less_more,
        damage,
        health,
    }

    /// <summary>
    /// 效果:伤害/治疗
    /// 类型:固定值 / 攻击力百分比 / 当前血量百分比 / 最大血量百分比 /当前伤害
    /// </summary>
    public enum E_EffectDamgeAndhealth
    {
        fixation = 0,                   // 固定值
        atk = 1,                        // 攻击力 百分比
        curr_hp = 2,                    // 当前血量 百分比
        max_hp = 3,                     // 最大血量 百分比
        damage = 4,                     // 当前伤害
    }

    public enum E_EffectBuff
    {
        none = 0,
        add_buff = 1,                   //添加buffID
        remove_buff = 2,
    }

    public enum E_EffectBuffTarget
    {
        self = 0,                       //自身
        target = 1,                     //目标
    }
}
