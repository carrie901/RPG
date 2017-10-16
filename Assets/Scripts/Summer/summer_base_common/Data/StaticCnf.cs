using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Summer;

public class StaticCnf
{
    public static Dictionary<Type, IDictionary> cnf_map = new Dictionary<Type, IDictionary>();

    public static Dictionary<int, T> FindMap<T>() where T : BaseCsv
    {
        Type type = typeof(T);

        if (!cnf_map.ContainsKey(type))
        {
            LogManager.Error("not find: " + type);
            return null;
        }

        return cnf_map[type] as Dictionary<int, T>;
    }

    public static T FindData<T>(int id) where T : BaseCsv
    {
        Type type = typeof(T);
        if (!cnf_map.ContainsKey(type))
        {
            return null;
        }

        Dictionary<int, T> tmp_map = cnf_map[type] as Dictionary<int, T>;
        if (tmp_map == null)
        {
            LogManager.Error(type.Name + "表结构有问题");
            return null;
        }
        if (!tmp_map.ContainsKey(id))
        {
            LogManager.Error(type.Name + "表中未找到Id为:" + id + "的行!");
            return null;
        }

        return tmp_map[id];
    }

    public static bool IsExists<T>(int id) where T : BaseCsv
    {

        Dictionary<int, T> dict = FindMap<T>();

        return dict.ContainsKey(id);
    }

    public static void Add<T>(Dictionary<int, T> cnf) where T : BaseCsv
    {
        Type type = typeof(T);
        cnf_map.Add(type, cnf);
    }
}
