﻿using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放特效
    /// </summary>
    public class PlayEffectLeafNode : SkillLeafNode
    {
        public const string DES = "播放特效";
        public string effect_name;             //特效名称
        //public GameObject bing_obj;            //绑定的GameObject
        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();
            PlayEffectEventSkill data = EventDataFactory.Pop<PlayEffectEventSkill>();
            data.effect_name = effect_name;
            //data.bing_obj = bing_obj;
            RaiseEvent(E_EntityInTrigger.play_effect, data);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {
            LogExit();
        }
        public override string ToDes() { return DES; }
    }
}

