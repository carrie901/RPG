using Summer;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
/// 把这一部分搞成config，提交出去
/// </summary>
public class ResPathManager
{
    public static Dictionary<E_GameResType, string> _path_map = new Dictionary<E_GameResType, string>();
    public static Dictionary<E_GameResType, string> _suffix = new Dictionary<E_GameResType, string>();
    static ResPathManager()
    {

        _excute(E_GameResType.skill_icon, "icon/skill/", string.Empty/*".png"*/);
        _excute(E_GameResType.hero_icon, "icon/hero/", string.Empty/*".png"*/);
        _excute(E_GameResType.ui_prefab, "prefab/ui/", string.Empty/* ".prefab"*/);
        _excute(E_GameResType.ui_item_prefab, "item/ui/", string.Empty);
        _excute(E_GameResType.cvs_byte, "data/", string.Empty/* ".bytes"*/);
    }

    public static string find_path(E_GameResType res_type, string name)
    {
        string path = string.Empty;

        if (_path_map.ContainsKey(res_type))
        {
            path = _path_map[res_type] + name + _suffix[res_type];
            return path;
        }
        LogManager.Error("找不到信息,类型:[{0}].名字:[{1}]", res_type, name);
        return path;
    }

    public static void _excute(E_GameResType type, string path, string suffix)
    {
        _path_map.Add(type, path);
        _suffix.Add(type, suffix);
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
