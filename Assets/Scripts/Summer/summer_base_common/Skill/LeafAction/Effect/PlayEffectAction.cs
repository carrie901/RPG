using System;
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
    public class PlayEffectAction : SkillNodeAction
    {
        public const string DES = "播放特效";
        public string effect_name;             //特效名称
        public GameObject bing_obj;            //绑定的GameObject
        public PlayEffectEventSkill _data;
        public override void OnEnter()
        {
            LogEnter();
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayEffectEventSkill>();
            _data.effect_name = effect_name;
            _data.bing_obj = bing_obj;

            RaiseEvent(E_SkillTriggerEvent.play_effect, _data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
            EventSkillDataFactory.Pop(_data);
            _data = null;
        }
        public override string ToDes() { return DES; }
    }
}

