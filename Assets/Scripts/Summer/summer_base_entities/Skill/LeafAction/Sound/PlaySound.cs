using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class PlaySound : SkillLeafNode
    {
        public const string DES = "播放声音";
        public string sound_name;              //特效名称
        public Vector3 _position;

        public override void OnEnter()
        {
            LogEnter();

            PlaySoundEventData data = EventDataFactory.Pop<PlaySoundEventData>();
            data.sound_name = sound_name;
            data.position = _position;

            RaiseEvent(E_EntityInTrigger.play_sound, data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override void OnUpdate(float dt)
        {

        }

        public override string ToDes() { return DES; }
    }

    public class PlaySoundByAnimation : PlaySound
    {

    }
}

