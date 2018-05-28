using System.Collections.Generic;
using Object = UnityEngine.Object;


public enum E_GameResType
{
    none = 0,
    text_asset,              // 文本路径
    quanming,
    skill_icon,             // 技能 Icon表
    hero_icon,              // 英雄 icon表
    buff_icon,
    other_icon,
    buff_prefab,            // buff prefab表
    stage_icon,

    character_prefab,
    skill_prefab,
    ui_prefab,
    ui_item_prefab,
    item_icon,              // 道具 icon

    //声音
    music_bgm,              // BGM
    music_sound,            // 音效
    music_voice,            // 配乐

    // ui 特效
    ui_effect,

    shanchu,

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
    //public static Dictionary<E_GameResType, string> _suffix = new Dictionary<E_GameResType, string>();
    static ResPathManager()
    {
        // 不同的加载方式，后缀名会不一样
        _excute(E_GameResType.skill_icon, "icon/skill/");
        _excute(E_GameResType.hero_icon, "icon/hero/");
        _excute(E_GameResType.other_icon, "icon/other/");
        _excute(E_GameResType.ui_prefab, "prefabs/gui/ui/");
        _excute(E_GameResType.ui_item_prefab, "item/ui/");
        _excute(E_GameResType.item_icon, "icon/item/");
        _excute(E_GameResType.buff_icon, "icon/buff/");
        _excute(E_GameResType.stage_icon, "icon/stage/");

        _excute(E_GameResType.music_bgm, "music/bgm/");
        _excute(E_GameResType.music_sound, "music/sound/");
        _excute(E_GameResType.music_voice, "music/voice/");

        _excute(E_GameResType.ui_effect, "prefab/ui_eff/");

        _excute(E_GameResType.text_asset, "textasset/");
        _excute(E_GameResType.quanming, "");
    }

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
}
