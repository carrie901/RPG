using UnityEngine;
using System.Collections;

namespace Summer
{


    /// <summary>
    /// 角色某种属性更新
    /// </summary>
    public enum E_CharAttributeType
    {
        none = 0,

        atk = 1,                    //攻击力 
        defense = 2,                //防御  
        max_hp = 3,                 //最大血量  
        cri_rate = 4,               //暴击率 
        anti_cri = 5,               //抗暴率 
        wreck = 6,                  //破击率 
        block = 7,                  //格挡率 
        atk_speed = 8,              //攻速  
        cri_dmg = 9,                //暴伤  
        anti_cri_dmg = 10,          //抗暴伤 
        block_intensity = 11,       //格挡免伤率   
        atk_intensity = 12,         //伤害率 
        def_intensity = 13,         //免伤率 
        suck_blood = 14,            //吸血率 
        anti_suck_blood = 15,       //吸血抵抗率
        reflect_dmg_rate = 16,      //反伤率
                                    //hp = 17,                    //血量  血量和无双有点特殊性质
                                    //peerless = 18,              //无双
        move_speed = 19,              //移速
        max,

    }

    /// <summary>
    /// 角色数据更新的方式
    /// 百分比
    /// 实际值
    /// 清零
    /// </summary>
    public enum E_CharDataUpdateType
    {
        none = 0,
        plus = 1,           //  1 = +1
        multiply_plus = 2,  //  1 = +1%
        zero = 3,           //  all means = 0
    }

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
        data_updater = 1,                   //单属性更新
        data_multiple_updater = 2,          //多重属性，最多四层
        vampire = 7,                        //吸血
        passive_damage = 8,                 //反伤
        invincible = 9,                     //无敌
        passive_damage_health = 10,         //收到伤害回复血量
        bleeding = 11,                      //流血
        blood = 12,                         //回血
        peerless_add = 13,                  //无双增加
        peerless_reduce = 14,               //无双减少
        shield = 15,                        //护盾
        exchange = 16,                      //以血量兑换几种属性

        max,
    }


    /// <summary>
    /// Buff触发点
    /// </summary>
    public enum E_BuffTrigger
    {
        on_update_peerless,                 //更新无双值/愤怒值

        on_update_property,                 //更新属性值

        on_trigger_invincible,              //无敌

        on_buff_damage,                     //buff伤害
        on_buff_health,                     //buff治疗

        on_attack_enemy,                    //攻击敌人     
        on_attack_enemy_before,             //攻击别人之前
        on_attack_enemy_damage,             //攻击别人造成伤害
        on_def_damage_before,               //收到伤害之前
        on_def_damage,                      //收到伤害
    }


    /// <summary>
    /// 来一个BuffEventData的缓存池
    /// </summary>
    public class EventBuffSetData
    {

    }
}