using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-5 20:9:37
// FileName : RawLoader.cs
//=============================================================================

namespace Summer
{
    public class RawLoader : MonoBehaviour
    {
        public RawImage icon;
        public string icon_name;

        void OnDisable()
        {
            RawPool.Instance.ReaycelTexture(icon, icon_name);
        }

        void OnEnable()
        {
            RawPool.Instance.LoadTextureAsync(icon, icon_name);
        }
    }
}
