using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public interface I_Effect
    {
        void Cast();
        void Reverse();
    }

    public interface I_EffectContainer : I_Effect
    {
        List<Effect> FindEffects();
    }

}

