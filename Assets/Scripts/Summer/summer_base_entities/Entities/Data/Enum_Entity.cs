﻿using System.Collections;
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
        find_targets,               // 找到目标
        export_to_target,           // 输出伤害
        skill_release,              // 释放技能控制
        skill_finish,               // 技能结束
        
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

    }

    #endregion
}
