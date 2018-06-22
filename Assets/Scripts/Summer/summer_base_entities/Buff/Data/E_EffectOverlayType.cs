using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    /// <summary>
    /// buff叠加类型
    /// </summary>
    public enum E_EffectOverlayType
    {
        /// <summary>
        /// 不可叠加可刷新
        /// </summary>
        not_overlay_can_refresh,
        /// <summary>
        /// 不可叠加不可刷新
        /// </summary>
        not_overlay_not_refresh,
        /// <summary>
        /// 可以叠加，可以刷新
        /// </summary>
        can_overlay_can_refresh,
        /// <summary>
        /// 可以叠加，不可以刷新
        /// </summary>
        can_overlay_not_refresh,
    }

    /// <summary>
    /// 数值类buff的数值类型
    /// </summary>
    public enum E_EffectSetValueType
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
    /// 数值来源 可能会和数值类型重叠
    /// </summary>
    public enum E_EffectValueSourceType
    {
        none = 0,
        health,                     // 治疗
        damage,                     // 伤害
                                    /*    add_peerless,               // 无双增加
                                        reduce_peerless,            // 无双减少*/
        max,
    }

    /// <summary>
    /// 数值类型
    /// </summary>
    public enum E_EffectValueTYpe
    {
        plus = 0,           //  1 = +1
        multiply_plus = 1,  //  1 = +1%
        zero = 2,           //  all means = 0
    }
}
