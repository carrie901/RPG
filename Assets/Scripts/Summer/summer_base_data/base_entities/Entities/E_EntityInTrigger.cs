namespace Summer
{
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

        change_state,               // 改变状态   TODO 目前感觉这个枚举是有可能引起很多未知问题的枚举
        entity_die,                 // 人物死亡

        //play_camera_shake,          //镜头抖动
        //play_camera_effect,         //镜头特效
        //play_camera_offset,         //镜头偏移，提供机制回复到原始位置
        max,
    }
}