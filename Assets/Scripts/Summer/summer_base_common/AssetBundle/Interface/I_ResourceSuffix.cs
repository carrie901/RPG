﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//=============================================================================
// Author : mashao
// CreateTime : 2018-3-22 15:53:35
// FileName : I_ResourceSuffix.cs
//=============================================================================

namespace Summer
{
    public abstract class AResourceSuffix
    {
        public const string IMAGE = ".png";
        public const string PREFAB = ".prefab";
        public const string ANIMATION = ".anim";
        public Dictionary<Type, string> _suffix_map = new Dictionary<Type, string>()
        {
            {typeof(Image),IMAGE },
            {typeof(GameObject),PREFAB },
            {typeof(Animation),PREFAB },
        };

        public abstract string GetSuffix<T>() where T : UnityEngine.Object;
    }

    public class ResourceSuffix : AResourceSuffix
    {
        public override string GetSuffix<T>()
        {
            return string.Empty;
        }
    }

    public class AssetDatabaseSuffix : ResourceSuffix
    {
        public override string GetSuffix<T>()
        {
            string suffix = "";
            _suffix_map.TryGetValue(typeof(T), out suffix);
            return suffix;
        }
    }
}
