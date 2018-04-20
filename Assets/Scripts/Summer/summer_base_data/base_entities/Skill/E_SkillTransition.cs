using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 技能过度事件
    /// 序列节点和序列节点之间需要一个过度事件
    /// TODO QAQ:4.17 这个是节点之间的相关事件，居然有一些外部的相关内容。觉的是有问题的，可以通过string来代替相关的
    /// </summary>
    public enum E_SkillTransition
    {
        none = 0,
        start,
        sound,
        anim_start,
        anim_event01,
        anim_event02,
        anim_hit,                   // 动作的击打事件
        anim_finish,                // 动作播放结束
        anim_release,               // 普攻已经释放了 
    }
}

