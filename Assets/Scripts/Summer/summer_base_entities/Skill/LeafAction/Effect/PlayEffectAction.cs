using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 播放特效的参数
    /// </summary>
    public class PlayEffectEventSkill : EventSetData
    {
        public string effect_name;
        public GameObject bing_obj;

        public override void Reset()
        {
            bing_obj = null;
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
        public override void OnEnter()
        {
            LogEnter();
            PlayEffectEventSkill data = EventDataFactory.Pop<PlayEffectEventSkill>();
            data.effect_name = effect_name;
            data.bing_obj = bing_obj;
            PoolVfxObject vfx_go = TransformPool.Instance.Pop<PoolVfxObject>(effect_name);
            vfx_go.SetLifeTime(2f);
            RaiseEvent(E_EntityInTrigger.play_effect, data);
            Finish();
        }

        public override void OnExit()
        {
            LogExit();
        }
        public override string ToDes() { return DES; }
    }
}

