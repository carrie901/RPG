using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放特效的参数
    /// </summary>
    public class PlayEffectEventSkill : EventSkillSetData
    {
        public string effect_name;
        public GameObject bing_obj;
    }

    /// <summary>
    /// 播放特效
    /// </summary>
    public class PlayEffect : ASkillAction
    {
        public string _effect_name;             //特效名称
        public GameObject _bing_obj;            //绑定的GameObject
        public PlayEffectEventSkill _data;
        public override void OnEnter()
        {
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayEffectEventSkill>();
            _data.effect_name = _effect_name;
            _data.bing_obj = _bing_obj;

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

