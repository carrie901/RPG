namespace Summer
{
    /// <summary>
    /// 对角色某种数据更新
    /// </summary>
    public enum E_CharAttributeRegion
    {
        none = 0,
        atk = 1,                    // 攻击力 
        defense = 2,                // 防御  
        max_hp = 3,                 // 最大血量  
        cri_rate = 4,               // 暴击率 
        anti_cri = 5,               // 抗暴率 
        wreck = 6,                  // 破击率 
        block = 7,                  // 格挡率 
        cri_dmg = 8,                // 暴伤  
        anti_cri_dmg = 9,           // 抗暴伤 
        atk_speed = 10,             // 攻速  
        skill_cd = 11,               // 技能冷却cd
        move_speed = 12,            // 移速
        max,

    }

    /// <summary>
    /// 数值
    /// </summary>
    public enum E_CharDataValueRegion
    {
        none = 0,
        health,                     // 治疗
        damage,                     // 伤害
                                    /*    add_peerless,               // 无双增加
                                        reduce_peerless,            // 无双减少*/
        max,
    }

    /// <summary>
    /// 角色数据更新的方式
    /// 百分比
    /// 实际值
    /// 清零
    /// </summary>
    public enum E_DataUpdateType
    {
        plus = 0,           //  1 = +1
        multiply_plus = 1,  //  1 = +1%
        zero = 2,           //  all means = 0
    }

    /// <summary>
    /// 最大值类型
    /// </summary>
    public enum E_CharDataByValue
    {
        none = 0,
        current = 1,            //当前值
        max = 2,                //最大值
    }

    /// <summary>
    /// buff类型
    /// </summary>
    public enum E_BUFF_TYPE
    {
        none = 0,

        //effect_container = 1,               // 效果容器集合
        //data_multiple_updater = 2,          // 多重属性
        //vampire = 7,                        // 吸血
        //passive_damage = 8,                 // 反伤
        //invincible = 9,                     // 无敌
        //passive_damage_health = 10,         // 收到伤害回复血量
        //bleeding = 11,                      // 流血
        //blood = 12,                         // 回血
        //peerless_add = 13,                  // 无双增加
        //peerless_reduce = 14,               // 无双减少
        //shield = 15,                        // 护盾
        //exchange = 16,                      // 以血量兑换几种属性

        //changestate = 18,                   // 改变状态
        //normal_trigger_effect,              // 普通触发效果
        //layer_break_effect,                 // 到达一定层级之后爆发效果 目前只支持2个参数，一个为正常效果，一个为爆发效果（如果效果只有一个，那么就只有爆发效果）
        normal = 99,
        max,
    }

}



