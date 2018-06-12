using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-5 20:8:55
// FileName : RawPool.cs
//=============================================================================

namespace Summer
{
    public class RawPool
    {
        public static RawPool Instance = new RawPool();

        // 引用计算缓存六个

        public void LoadTextureAsync(RawImage img, string name)
        {
            ResManager.instance.LoadTextureAsync(img, ResRequestFactory.CreateRequest<Texture>(name, E_GameResType.quanming), OnComplete);
        }

        public void ReaycelTexture(RawImage img, string name)
        {
            ResManager.instance.ResetDefaultTexture(img);
        }

        public void OnComplete(Texture texture)
        {

        }
    }
}
