using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class PlayCameraShakeEventSkill : EventSetData
    {

    }

    /// <summary>
    /// 镜头抖动
    /// </summary>
    public class PlayCameraShake : SkillLeafNode
    {
        public const string DES = "镜头抖动";

        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();
            PlayCameraShakeEventSkill data = EventDataFactory.Pop<PlayCameraShakeEventSkill>();
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_shake, data);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {

        }

        public override string ToDes() { return DES; }
    }

}
