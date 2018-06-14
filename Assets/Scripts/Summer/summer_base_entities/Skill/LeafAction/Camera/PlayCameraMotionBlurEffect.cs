using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    public class PlayCameraMotionBlurEffectEventSkill : EventSetData
    {

    }

    /// <summary>
    /// 运动模糊
    /// </summary>
    public class PlayCameraMotionBlurEffect : SkillLeafNode
    {
        public const string DES = "运动模糊";
        public PlayCameraMotionBlurEffectEventSkill _data;
        public override void OnEnter(EntityBlackBoard blackboard)
        {
            if (_data == null)
                _data = EventDataFactory.Pop<PlayCameraMotionBlurEffectEventSkill>();
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_effect_motion_blur, _data);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {

        }

        public override void OnUpdate(float dt, EntityBlackBoard blackboard)
        {

        }

        public override void Destroy()
        {
            _data = null;
        }

        public override string ToDes() { return DES; }
    }
}
