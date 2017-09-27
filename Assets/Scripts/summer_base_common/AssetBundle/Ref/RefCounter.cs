using UnityEngine;
using System.Collections;

namespace Summer
{
    public class RefCounter : MonoBehaviour
    {
        public string ref_name = string.Empty;
        public E_GameResType ref_type;
        void Awake()
        {
            ResManager.instance.RefIncrease(ref_name, ref_type);
        }

        void OnDestroy()
        {
            ResManager.instance.RefDecrease(ref_name, ref_type);
        }
    }
}

