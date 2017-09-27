

namespace Summer
{
    public enum E_SkillTrigger
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
    /// 来一个EventData的缓存池
    /// </summary>
    public class EventSkillSetData
    {
        /* public EventSkillSetData()
         {

         }*/

        public virtual void Reset()
        {

        }
    }

    public class EventSkillDataFactory
    {
        public static T Push<T>() where T : EventSkillSetData, new()
        {
            T t = new T();
            return t;
        }

        public static void Pop<T>(T t) where T : EventSkillSetData
        {
            t.Reset();
        }
    }

}

