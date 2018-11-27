namespace Summer
{
    /// <summary>
    /// 让这个人干什么事
    /// </summary>
    public enum E_EntityInTrigger
    {
        NONE,
        /// <summary>
        /// 播放特效
        /// </summary>
        PLAY_EFFECT,                // 播放特效
        /// <summary>
        /// 播放声音
        /// </summary>
        PLAY_SOUND,                 // 播放声音
        /// <summary>
        /// 播放动作
        /// </summary>
        PLAY_ANIMATION,             // 播放动作
        /// <summary>
        /// 改变动作速度
        /// </summary>
        CHANGE_ANIMATION_SPEED,     // 改变动作速度

        //find_targets,               // 找到目标
        //export_to_target,           // 输出伤害
        /// <summary>
        /// 移动到目标地点
        /// </summary>
        MOVE_TO_TARGET_POSITION,    // 移动到目标地点
        /// <summary>
        /// 释放技能控制
        /// </summary>
        SKILL_RELEASE,              // 释放技能控制
        /// <summary>
        /// 技能结束
        /// </summary>
        SKILL_FINISH,               // 技能结束
        /// <summary>
        /// 改变状态
        /// </summary>
        CHANGE_STATE,               // 改变状态   TODO 目前感觉这个枚举是有可能引起很多未知问题的枚举
        /// <summary>
        /// 人物死亡
        /// </summary>
        ENTITY_DIE,                 // 人物死亡

        //play_camera_shake,          //镜头抖动
        //play_camera_effect,         //镜头特效
        //play_camera_offset,         //镜头偏移，提供机制回复到原始位置
        MAX,
    }
}