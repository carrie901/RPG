using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    #region 人物相关事件

    /// <summary>
    /// 触发人物触发的相关事件
    /// TODO 概念由Entity自身发出某一个事件，然后让entity自己做行为，目前无法确认是否有必要
    /// </summary>
    public enum E_EntityInTrigger
    {
        none,
        play_effect,                // 播放特效
        play_sound,                 // 播放声音
        play_animation,             // 播放动作
        change_animation_speed,     // 改变动作速度
        find_targets,               // 找到目标
        export_to_target,           // 输出伤害
        skill_release,              // 释放技能控制
        skill_finish,               // 技能结束

        entity_die,                 // 人物死亡

        //play_camera_shake,          //镜头抖动
        //play_camera_effect,         //镜头特效
        //play_camera_offset,         //镜头偏移，提供机制回复到原始位置
        max,
    }

    // TODO 概念 由外部触发，然后内部执行一些行为，比如我收到攻击了，我应该怎么办 可以变相的让E_BuffTrigger事件消失或者说更加纯粹华
    // 1. 比如播放动作 可以有外部触发播放一个动作，也有可能让内部触发播放一个动作
    // 2. 内部触发一个有生命周期的行为
    public enum E_EntityOutTrigger
    {
        animation_event,                // 动作事件
    }

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

    #endregion
}
