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

        Texture LoadTexture(ResRequestInfo resRequest);

        Texture LoadTexture(RawImage img, ResRequestInfo resRequest);

        void LoadTextureAsync(RawImage img, ResRequestInfo resRequest, Action<Texture> complete);
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
        GameObject LoadPrefab(string resName, E_GameResType resType = E_GameResType.QUANMING, bool isCopy = true);

        void LoadPrefabAsync(string resName, E_GameResType resType = E_GameResType.QUANMING, Action<GameObject> complete = null);
    }

    #endregion

    #region AnimationClip

    public interface I_AnimationClipLoad
    {
        AnimationClip LoadAnimationClip(ResRequestInfo resRequest);

        void LoadAnimationClipAsync(ResRequestInfo resRequest, Action<AnimationClip> complete);
    }

    #endregion

    #region Sprite 

    public interface I_SpriteLoad
    {
        Sprite LoadSprite(ResRequestInfo resRequest);

        void LoadSpriteAsync(Image img, ResRequestInfo resRequest, Action<Sprite> complete = null);
    }

    #endregion

}
