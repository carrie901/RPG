using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    /// <summary>
    /// 播放特效的参数
    /// </summary>
    public class PlaySoundEventSkill : EventSkillSetData
    {
        public string sound_name;
        public Vector3 position;
    }

    public class PlaySoundAction : ASkillAction
    {

        public string _sound_name;              //特效名称
        public Vector3 _position;

        public PlaySoundEventSkill _data;
        public override void OnEnter()
        {
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlaySoundEventSkill>();
            _data.sound_name = _sound_name;
            _data.position = _position;

            RaiseEvent(E_SkillTrigger.play_sound, _data);
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

    public class PlaySoundActionByAnimation : PlaySoundAction
    {
        
    }
}

