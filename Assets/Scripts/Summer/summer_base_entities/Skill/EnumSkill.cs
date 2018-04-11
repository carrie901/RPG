using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 技能流程触发的相关事件
    /// </summary>
    public enum E_SkillSequenceTrigger
    {
        none,
        play_effect,                //播放特效
        play_sound,                 //播放声音
        play_animation,             //播放动作


        //play_camera_shake,          //镜头抖动
        //play_camera_effect,         //镜头特效
        //play_camera_offset,         //镜头偏移，提供机制回复到原始位置
        max,
    }


    /// <summary>
    /// 技能过度事件
    /// 序列节点和序列节点之间需要一个过度事件
    /// </summary>
    public enum E_SkillTransition
    {
        none = 0,
        start,
        sound,
        anim_hit,                   // 动作的击打事件
        anim_finish,                // 动作播放结束
    }

    public class SkillTriggerEventFactory
    {
        public static Dictionary<string, E_SkillTransition> _event_map
            = new Dictionary<string, E_SkillTransition>();

        public static bool _init = false;

        public static E_SkillTransition GetEvent(string name)
        {
            if (!_init)
            {
                _init = true;
                _init_event();
            }
            E_SkillTransition transition = E_SkillTransition.none;
            _event_map.TryGetValue(name, out transition);
            if (transition == E_SkillTransition.none)
            {
                LogManager.Error("找不到对应的技能事件:[{0}]", name);
            }

            return transition;
        }

        public static void _init_event()
        {
            _event_map.Add("FINISH", E_SkillTransition.start);
        }
    }


    /// <summary>
    /// 来一个EventData的缓存池
    /// </summary>
    public class EventSkillSequenceData
    {
        public virtual void Reset()
        {

        }
    }

    public class EventSkillDataFactory
    {
        public static T Push<T>() where T : EventSkillSequenceData, new()
        {
            T t = new T();
            return t;
        }

        public static void Pop<T>(T t) where T : EventSkillSequenceData
        {
            t.Reset();
        }
    }

}

