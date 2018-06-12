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
        public string res_path;

        void OnDisable()
        {
            RawPool.Instance.ReaycelTexture(icon, res_path);
        }

        void OnEnable()
        {
            RawPool.Instance.LoadTextureAsync(icon, res_path);
        }
    }
}
