using System.Collections.Generic;
using Summer;
using Object = UnityEngine.Object;


public enum E_GameResType 
{
    none = 0,
    quanming,
    character_prefab,
    skill_prefab,
    text_asset,
    // music
    music_sound,
    music_bgm,
    music_voice,
    ui_prefab,
}

/// <summary>
/// 把这一部分搞成config，提交出去
/// 
/// TODO 3.22
///     由于增加了一个quanming类型,提供的是全路径，这就导致了无法通过LoadAsset来判断得到是什么类型的资源 
/// </summary>
public class ResPathManager
{
    public static Dictionary<E_GameResType, string> _path_map = new Dictionary<E_GameResType, string>();
    public static AResourceSuffix _suffix;
    static ResPathManager()
    {
        // 不同的加载方式，后缀名会不一样
        _excute(E_GameResType.quanming, "", "");
    }

    public static string FindPath<T>(E_GameResType res_type, string name) where T : Object
    {
        string path = string.Empty;

        if (_path_map.ContainsKey(res_type))
        {
            path = _path_map[res_type] + name + _suffix.GetSuffix<T>();
            return path;
        }
        ResLog.Error("找不到信息,类型:[{0}].名字:[{1}]", res_type, name);
        return path;
    }

    public static void _excute(E_GameResType type, string path, string suffix)
    {
        _path_map.Add(type, path);
    }
}
