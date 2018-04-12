using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放动作
    /// </summary>
    public class PlayAnimationEventData : EventEntityData
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
                _data = EventEntityDataFactory.Push<PlayAnimationEventData>();
            _data.animation_name = animation_name;

            RaiseEvent(E_EntityOutTrigger.play_animation, _data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
            EventEntityDataFactory.Pop(_data);
            _data = null;
        }

        public override string ToDes()
        {
            return DES;
        }
    }

}
