using Summer;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// 把这一部分搞成config，提交出去
/// 
/// TODO 3.22
///     由于增加了一个quanming类型,提供的是全路径，这就导致了无法通过LoadAsset来判断得到是什么类型的资源 
/// </summary>
public class ResPathManager
{
    public static Dictionary<E_GameResType, string> _path_map = new Dictionary<E_GameResType, string>();
    //public static Dictionary<E_GameResType, string> _suffix = new Dictionary<E_GameResType, string>();
    static ResPathManager()
    {
        // 不同的加载方式，后缀名会不一样
        _excute(E_GameResType.skill_icon, "icon/skill/");
        _excute(E_GameResType.hero_icon, "icon/hero/");
        _excute(E_GameResType.other_icon, "icon/other/");
        _excute(E_GameResType.ui_prefab, "Prefabs/GUI/ui/");
        _excute(E_GameResType.ui_item_prefab, "item/ui/");
        _excute(E_GameResType.item_icon, "icon/item/");
        _excute(E_GameResType.buff_icon, "icon/buff/");
        _excute(E_GameResType.stage_icon, "icon/stage/");

        _excute(E_GameResType.music_bgm, "Music/bgm/");
        _excute(E_GameResType.music_sound, "Music/sound/");
        _excute(E_GameResType.music_voice, "Music/voice/");

        _excute(E_GameResType.ui_effect, "prefab/ui_eff/");

        _excute(E_GameResType.quanming, "");
    }

    /*    public static string FindPath(E_GameResType res_type, string name)
        {
            string path = string.Empty;

            if (_path_map.ContainsKey(res_type))
            {
                path = _path_map[res_type] + name + _suffix[res_type];
                return path;
            }
            ResLog.Error("找不到信息,类型:[{0}].名字:[{1}]", res_type, name);
            return path;
        }*/

    public static string FindPath<T>(E_GameResType res_type, string name, Summer.AResourceSuffix a_suffix) where T : Object
    {
        string path = string.Empty;

        if (_path_map.ContainsKey(res_type))
        {
            path = _path_map[res_type] + name + a_suffix.GetSuffix<T>();
            return path;
        }
        ResLog.Error("找不到信息,类型:[{0}].名字:[{1}]", res_type, name);
        return path;
    }

    public static void _excute(E_GameResType type, string path)
    {
        _path_map.Add(type, path);
    }

    public static string streaming_assets_path()
    {
        if (Application.isEditor)
            return "file://" + System.Environment.CurrentDirectory.Replace("\\", "/"); // Use the build output folder directly.
        else if (Application.isMobilePlatform || Application.isConsolePlatform)
            return Application.streamingAssetsPath;
        else // For standalone player.
            return "file://" + Application.streamingAssetsPath;
    }
}
