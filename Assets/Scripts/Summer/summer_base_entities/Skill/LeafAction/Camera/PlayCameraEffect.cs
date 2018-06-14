using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    public class PlayCameraRadialBlurEffectEventSkill : EventSetData
    {
        public float duration;
        public float fade_in;
        public float fade_out;
        public float strength;
    }

    /// <summary>
    /// 径向模糊:图像旋转成从中心辐射。
    /// </summary>
    public class PlayCameraRadialBlurEffect : SkillLeafNode
    {
        public const string DES = "径向模糊";
        public float duration;
        public float fade_in;
        public float fade_out;
        public float strength;
        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();
            PlayCameraRadialBlurEffectEventSkill data = EventDataFactory.Pop<PlayCameraRadialBlurEffectEventSkill>();
            data.duration = duration;
            data.fade_in = fade_in;
            data.fade_out = fade_out;
            data.strength = strength;

            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_effect_radial_blur, data);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {
            LogExit();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override string ToDes() { return DES; }
    }   
}
