using UnityEngine;
using System.Collections;

namespace Summer
{
    public interface I_Effect
    {
        void OnAttach();

        void OnUpdate();

        void OnDetach();
    }
}

