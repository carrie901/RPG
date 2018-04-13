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
    public class PlayCameraShake : SkillNodeAction
    {
        public const string DES = "镜头抖动";
        public PlayCameraShakeEventSkill _data;

        public override void OnEnter()
        {
            if (_data == null)
                _data = EventDataFactory.Pop<PlayCameraShakeEventSkill>();
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_shake, _data);
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
