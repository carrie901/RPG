using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 径向模糊
    /// 针对每个像素点，首先要得到一个相对于中心（不一定就是屏幕正中心，可以自己指定）的方向\
    /// 从中心指向该像素点的方向就是径向模糊的方向，然后取当前像素点以及沿着径向模糊方向再取几个点作为采样点
    /// 采样点越靠近中心越密集越远离中心越稀疏，最后该像素点的输出就是这些采样点的均值
    /// 这样在靠近中心点的位置，采样距离小几乎为0，也就不会模糊；
    /// 而越靠近边界的位置，采样的距离越大，图像也就会越模糊。
    /// </summary>
    public class CameraRadialBlurEffect : CameraPostEffectBase
    {
        [Range(0, 0.05f)]
        public float blur_factor = 1.0f;         //模糊程度，不能过高  
        public Vector2 blur_center =             //模糊中心（0-1）屏幕空间，默认为中心点  
            new Vector2(0.5f, 0.5f);

        void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (CMainMaterial)
            {
                CMainMaterial.SetFloat("_BlurFactor", blur_factor);
                CMainMaterial.SetVector("_BlurCenter", blur_center);
                Graphics.Blit(source, destination, CMainMaterial);
            }
            else
            {
                Graphics.Blit(source, destination);
            }
        }
    }
}

