﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放动作
    /// </summary>
    public class PlayAnimationEventData : EventSkillSequenceData
    {
        public string animation_name;
    }

    /// <summary>
    /// 播放角色动作
    /// </summary>
    public class PlayAnimationAction : SkillNodeAction
    {
        public const string DES = "播放动作";
        public string animation_name;
        public PlayAnimationEventData _data;
        public override void OnEnter()
        {
            LogEnter();
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayAnimationEventData>();
            _data.animation_name = animation_name;

            RaiseEvent(E_SkillSequenceTrigger.play_animation, _data);
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
