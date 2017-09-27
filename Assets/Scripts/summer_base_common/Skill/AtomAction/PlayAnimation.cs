using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放动作
    /// </summary>
    public class PlayAnimationEventSkill : EventSkillSetData
    {
        public string animation_name;
    }

    public class PlayAnimation : ASkillAction
    {
        public string _animation_name;
        public PlayAnimationEventSkill _data;
        public override void OnEnter()
        {
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayAnimationEventSkill>();
            _data.animation_name = _animation_name;

            RaiseEvent(E_SkillTrigger.play_effect, _data);
            Finish();
        }

        public override void OnExit()
        {
            EventSkillDataFactory.Pop(_data);
            _data = null;
        }

        public override void OnUpdate(float dt)
        {
            
        }
    }

}
