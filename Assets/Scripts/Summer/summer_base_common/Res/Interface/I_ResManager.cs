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

        Texture LoadTexture(string resPath);

        Texture LoadTexture(RawImage img, string resPath);

        void LoadTextureAsync(RawImage img, string resPath, Action<Texture> complete);
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
        GameObject LoadPrefab(string resName, bool isCopy = true);

        void LoadPrefabAsync(string resName, Action<GameObject> complete = null);
    }

    #endregion

    #region AnimationClip

    public interface I_AnimationClipLoad
    {
        AnimationClip LoadAnimationClip(string resPath);

        void LoadAnimationClipAsync(string resPath, Action<AnimationClip> complete);
    }

    #endregion

    #region Sprite 

    public interface I_SpriteLoad
    {
        Sprite LoadSprite(string resPath);

        void LoadSpriteAsync(Image img, string resPath, Action<Sprite> complete = null);
    }

    #endregion

}
