using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    public class PlayCameraRadialBlurEffectEventSkill : EventSkillSetData
    {
        public float duration;
        public float fade_in;
        public float fade_out;
        public float strength;
    }

    /// <summary>
    /// 径向模糊:图像旋转成从中心辐射。
    /// </summary>
    public class PlayCameraRadialBlurEffect : ASkillAction
    {
        public PlayCameraRadialBlurEffectEventSkill _data;
        public float duration;
        public float fade_in;
        public float fade_out;
        public float strength;
        public override void OnEnter()
        {
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayCameraRadialBlurEffectEventSkill>();
            _data.duration = duration;
            _data.fade_in = fade_in;
            _data.fade_out = fade_out;
            _data.strength = strength;

            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_effect_radial_blur, _data);
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
            EventSkillDataFactory.Pop(_data);
            _data = null;
        }
    }


    public class PlayCameraMotionBlurEffectEventSkill : EventSkillSetData
    {
       
    }

    /// <summary>
    /// 运动模糊
    /// </summary>
    public class PlayCameraMotionBlurEffect : ASkillAction
    {
        public PlayCameraMotionBlurEffectEventSkill _data;
        public override void OnEnter()
        {
            if (_data == null)
                _data = EventSkillDataFactory.Push<PlayCameraMotionBlurEffectEventSkill>();
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
            EventSkillDataFactory.Pop(_data);
            _data = null;
        }
    }
}
