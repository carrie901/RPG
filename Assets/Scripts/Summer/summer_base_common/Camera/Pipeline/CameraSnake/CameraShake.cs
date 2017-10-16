using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public abstract class CameraShake
    {
        public abstract bool IsEnd();
        public abstract void Process(CameraPipelineData data, float t);

    }

}
