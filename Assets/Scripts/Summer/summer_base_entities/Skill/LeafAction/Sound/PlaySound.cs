﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    /// <summary>
    /// 播放声音
    /// </summary>
    public class PlaySoundEventSkill : EventEntityData
    {
        public string sound_name;
        public Vector3 position;
    }

    public class PlaySoundAction : SkillNodeAction
    {
        public const string DES = "播放声音";
        public string sound_name;              //特效名称
        public Vector3 _position;

        public PlaySoundEventSkill _data;
        public override void OnEnter()
        {
            LogEnter();
            if (_data == null)
                _data = EventEntityDataFactory.Push<PlaySoundEventSkill>();
            _data.sound_name = sound_name;
            _data.position = _position;

            RaiseEvent(E_EntityOutTrigger.play_sound, _data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
            EventEntityDataFactory.Pop(_data);
            _data = null;
        }

        public override void OnUpdate(float dt)
        {
            
        }

        public override string ToDes() { return DES; }
    }

    public class PlaySoundActionByAnimation : PlaySoundAction
    {
        
    }
}

