using UnityEngine;

namespace Summer
{
    public class CameraRadialBlurEffectSeo : CameraPostEffectBase
    {


        [Range(0, 0.1f)]
        public float blur_factor = 1.0f;            // 模糊程度，不能过高  

        [Range(0.0f, 2.0f)]
        public float lerp_factor = 0.5f;            // 清晰图像与原图插值  

        public int down_sample_factor = 2;          // 降低分辨率  

        public Vector2 blur_center =                // 模糊中心（0-1）屏幕空间，默认为中心点  
            new Vector2(0.5f, 0.5f);

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (_Material)
            {
                // 申请两块降低了分辨率的RT  
                RenderTexture rt1 = RenderTexture.GetTemporary(source.width >> down_sample_factor, source.height >> down_sample_factor, 0, source.format);
                RenderTexture rt2 = RenderTexture.GetTemporary(source.width >> down_sample_factor, source.height >> down_sample_factor, 0, source.format);
                Graphics.Blit(source, rt1);

                // 使用降低分辨率的rt进行模糊:pass0  
                _Material.SetFloat("_BlurFactor", blur_factor);
                _Material.SetVector("_BlurCenter", blur_center);
                Graphics.Blit(rt1, rt2, _Material, 0);

                // 使用rt2和原始图像lerp:pass1  
                _Material.SetTexture("_BlurTex", rt2);
                _Material.SetFloat("_LerpFactor", lerp_factor);
                Graphics.Blit(source, destination, _Material, 1);

                // 释放RT  
                RenderTexture.ReleaseTemporary(rt1);
                RenderTexture.ReleaseTemporary(rt2);
            }
            else
            {
                Graphics.Blit(source, destination);
            }
        }
    }
}

