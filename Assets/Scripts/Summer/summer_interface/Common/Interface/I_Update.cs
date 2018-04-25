using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public interface I_Update
    {
        //int Priority { get; set; }
        void OnUpdate(float dt);
    }
}

