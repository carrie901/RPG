
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放角色动作
    /// </summary>
    public class ChangeAnimationSpeedLeafNode : SkillLeafNode
    {
        public const string DES = "改变动作速度";

        //public int frame_count = 10;
        //public int curr_frame = 0;
        public float speed = 0;
        public override void OnEnter()
        {
            LogEnter();
            AnimationSpeedEventData data = EventDataFactory.Pop<AnimationSpeedEventData>();
            data.animation_speed = speed;

            RaiseEvent(E_EntityInTrigger.change_animation_speed, data);
            //curr_frame = Time.frameCount;
        }

/*        public override void OnUpdate(float dt)
        {
            if (Time.frameCount - curr_frame > frame_count)
            {
                AnimationSpeedEventData data = EventDataFactory.Pop<AnimationSpeedEventData>();
                data.animation_speed = 1;
                RaiseEvent(E_EntityInTrigger.change_animation_speed, data);
                base.OnUpdate(dt);
            }
        }*/

        public override void OnExit()
        {
            LogExit();
        }

        public override string ToDes()
        {
            return DES;
        }
    }

}

