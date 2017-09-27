using UnityEngine;
using System.Collections;

public class TSingleton<T> where T : class, new()
{
    private static T _instance;
    //private static readonly object sys_lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }

    }
}
