using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class PlayCameraOffset : SkillNodeAction
    {
        public const string DES = "摄像头偏移";
        public override void OnEnter()
        {
            LogEnter();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
        }

        public override string ToDes() { return DES; }
    }

}
