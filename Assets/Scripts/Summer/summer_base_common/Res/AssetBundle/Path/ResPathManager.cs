using System.Collections.Generic;
using Summer;
using Object = UnityEngine.Object;


/// <summary>
/// TODO 需要剔除的部分，这部分应该是属于外部内容，不同的项目GameResType类型也不一样
/// </summary>
public enum E_GameResType
{
    NONE = 0,
    QUANMING,
    CHARACTER_PREFAB,
    SKILL_PREFAB,
    TEXT_ASSET,
    // music
    MUSIC_SOUND,
    MUSIC_BGM,
    MUSIC_VOICE,
    UI_PREFAB,
}

public class GameResTypeComparer : IEqualityComparer<E_GameResType>
{
    public static GameResTypeComparer Instance = new GameResTypeComparer();
    private GameResTypeComparer() { }
    public bool Equals(E_GameResType x, E_GameResType y) { return x == y; }

    public int GetHashCode(E_GameResType obj) { return (int)obj; }
}

/// <summary>
/// 把这一部分搞成config，提交出去
/// 
/// TODO 3.22
///     由于增加了一个quanming类型,提供的是全路径，这就导致了无法通过LoadAsset来判断得到是什么类型的资源 
/// </summary>
public class ResPathManager
{
    public static Dictionary<E_GameResType, string> _pathMap = new Dictionary<E_GameResType, string>();
    public static AResourceSuffix _suffix;
    static ResPathManager()
    {
        // 不同的加载方式，后缀名会不一样
        _excute(E_GameResType.QUANMING, "", "");
        _excute(E_GameResType.UI_PREFAB, "res_bundle/prefab/ui/", "");
    }

    public static string FindPath<T>(E_GameResType resType, string name) where T : Object
    {
        string path = string.Empty;

        if (_pathMap.ContainsKey(resType))
        {
            path = _pathMap[resType] + name + _suffix.GetSuffix<T>();
            return path;
        }
        ResLog.Error("找不到信息,类型:[{0}].名字:[{1}]", resType, name);
        return path;
    }

    public static void _excute(E_GameResType type, string path, string suffix)
    {
        _pathMap.Add(type, path);
    }
}
