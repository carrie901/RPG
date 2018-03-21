﻿using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Summer
{
    public interface I_ResourceLoad
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        Object LoadAsset(string path);

        //Object[] LoadAssetAll(string path);

        /// <summary>
        /// 异步加载
        /// </summary>
        OloadOpertion LoadAssetAsync(string path);

        /// <summary>
        /// 处于加载中
        /// </summary>
        bool HasInLoading(string name);

        bool UnloadAssetBundle(string assetbundle_path);

        void Update();
    }

    public interface I_TextureLoad
    {
        Texture LoadTexture(RawImage img, string name, E_GameResType res_type);

        void LoadTextureAsync(RawImage img, string name, E_GameResType res_type, Action<Texture> complete);
    }

    public interface I_AudioLoad
    {
        AudioClip LoadAudio(AudioSource audio_source, string name, E_GameResType res_type);

        void LoadAudioAsync(AudioSource audio_source, string name, E_GameResType res_type, Action<AudioClip> complete);
    }

    public interface I_PrefabLoad
    {
        GameObject LoadPrefab(string name, E_GameResType res_type);

        void LoadPrefabAsync(string name, E_GameResType res_type, Action<GameObject> complete);
    }

}

