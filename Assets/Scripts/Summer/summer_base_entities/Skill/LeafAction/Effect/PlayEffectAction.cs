﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放特效的参数
    /// </summary>
    public class PlayEffectEventSkill : EventEntityData
    {
        public string effect_name;
        public GameObject bing_obj;

        public override void Reset()
        {
            bing_obj=null;
        }
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
                _data = EventEntityDataFactory.Push<PlayEffectEventSkill>();
            _data.effect_name = effect_name;
            _data.bing_obj = bing_obj;
            PoolVfxObject vfx_go = TransformPool.Instance.Pop<PoolVfxObject>(effect_name);
            vfx_go.SetLifeTime(2f);
            RaiseEvent(E_EntityInTrigger.play_effect, _data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
            EventEntityDataFactory.Pop(_data);
            _data = null;
        }
        public override string ToDes() { return DES; }
    }
}

