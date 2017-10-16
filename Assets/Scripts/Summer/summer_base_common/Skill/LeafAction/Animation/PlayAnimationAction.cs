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

    public class PlayAnimationAction : SkillNodeAction
    {
        public const string DES = "播放动作";
        public string animation_name;
        public PlayAnimationEventSkill _data;
        public override void OnEnter()
        {
            LogEnter();
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayAnimationEventSkill>();
            _data.animation_name = animation_name;

            RaiseEvent(E_SkillTriggerEvent.play_effect, _data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
            EventSkillDataFactory.Pop(_data);
            _data = null;
        }

        public override string ToDes()
        {
            return DES;
        }
    }

}
