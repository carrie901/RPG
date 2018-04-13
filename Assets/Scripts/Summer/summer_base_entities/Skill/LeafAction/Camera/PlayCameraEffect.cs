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
    public class PlayCameraRadialBlurEffect : SkillNodeAction
    {
        public const string DES = "径向模糊";
        public float duration;
        public float fade_in;
        public float fade_out;
        public float strength;
        public override void OnEnter()
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

        public override void OnExit()
        {
            LogExit();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override string ToDes() { return DES; }
    }


    public class PlayCameraMotionBlurEffectEventSkill : EventSetData
    {

    }

    /// <summary>
    /// 运动模糊
    /// </summary>
    public class PlayCameraMotionBlurEffect : SkillNodeAction
    {
        public const string DES = "运动模糊";
        public PlayCameraMotionBlurEffectEventSkill _data;
        public override void OnEnter()
        {
            if (_data == null)
                _data = EventDataFactory.Pop<PlayCameraMotionBlurEffectEventSkill>();
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_effect_motion_blur, _data);
            Finish();
        }

        public override void OnExit()
        {

        }

        public override void OnUpdate(float dt)
        {

        }

        public override void Destroy()
        {
            _data = null;
        }

        public override string ToDes() { return DES; }
    }
}
