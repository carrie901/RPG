using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class PlayCameraOffset : AskillActionLeaf
    {
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
    }

}
