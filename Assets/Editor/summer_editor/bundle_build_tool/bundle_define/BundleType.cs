using UnityEngine;

namespace SummerEditor
{
    /// <summary>
    /// Bundle的类型
    /// </summary>
    public enum BundleType
    {
        none = 0,
        script,         // .cs
        shader,         // .shader or build-in shader with name
        font,           // .ttf
        texture,        // .tga, .png, .jpg, .tif, .psd, .exr
        material,       // .mat
        animation,      // .anim
        controller,     // .controller
        fbx,            // .fbx
        text_asset,      // .txt, .bytes
        prefab,         // .prefab
        unity_map,       // .unity
    }
}

