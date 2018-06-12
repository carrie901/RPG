using System;
using UnityEngine;
using UnityEngine.UI;

namespace Summer
{
    #region I_ResManager

    public interface I_ResManager : I_TextureLoad, /*I_AudioLoad,*/ I_PrefabLoad, I_AnimationClipLoad/*, I_SpriteLoad*/
    {

    }

    #endregion

    #region 纹理

    public interface I_TextureLoad
    {

        Texture LoadTexture(ResRequestInfo res_request);

        Texture LoadTexture(RawImage img, ResRequestInfo res_request);

        void LoadTextureAsync(RawImage img, ResRequestInfo res_request, Action<Texture> complete);
    }

    #endregion

    #region 音乐加载
    /*public interface I_AudioLoad
    {
        //AudioClip LoadAudio(AudioSource audio_source, ResRequestInfo res_request);

        //void LoadAudioAsync(AudioSource audio_source, ResRequestInfo res_request);
    }*/

    #endregion

    #region Prefab 加载

    public interface I_PrefabLoad
    {
        GameObject LoadPrefab(ResRequestInfo res_request, bool is_copy = true);

        void LoadPrefabAsync(ResRequestInfo res_request, Action<GameObject> complete);
    }

    #endregion

    #region AnimationClip

    public interface I_AnimationClipLoad
    {
        AnimationClip LoadAnimationClip(ResRequestInfo res_request);

        void LoadAnimationClipAsync(ResRequestInfo res_request, Action<AnimationClip> complete);
    }

    #endregion

    #region Sprite 

    public interface I_SpriteLoad
    {
        Sprite LoadSprite(ResRequestInfo res_request);

        void LoadSpriteAsync(Image img, ResRequestInfo res_request, Action<Sprite> complete = null);
    }

    #endregion

}
