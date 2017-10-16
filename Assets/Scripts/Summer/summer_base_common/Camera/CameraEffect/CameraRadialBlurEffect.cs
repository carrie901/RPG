using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class CameraRadialBlurEffect : CameraPostEffectBase
    {

        [Range(0, 0.05f)]
        public float blur_factor = 1.0f;         //模糊程度，不能过高  
        
        public Vector2 blur_center =             //模糊中心（0-1）屏幕空间，默认为中心点  
            new Vector2(0.5f, 0.5f);

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (_Material)
            {
                _Material.SetFloat("_BlurFactor", blur_factor);
                _Material.SetVector("_BlurCenter", blur_center);
                Graphics.Blit(source, destination, _Material);
            }
            else
            {
                Graphics.Blit(source, destination);
            }

        }
    }
}

