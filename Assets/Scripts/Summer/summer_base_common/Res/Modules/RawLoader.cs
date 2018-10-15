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
        public RawImage _icon;
        public string _resPath;

        void OnDisable()
        {
            RawPool.Instance.ReaycelTexture(_icon, _resPath);
        }

        void OnEnable()
        {
            RawPool.Instance.LoadTextureAsync(_icon, _resPath);
        }
    }
}
