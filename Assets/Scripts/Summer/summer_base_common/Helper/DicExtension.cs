using System;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-1 16:21:13
// FileName : DicExtension.cs
//=============================================================================

namespace Summer
{
    public static class DicExtension
    {
        /// <summary>
        /// 提供一个方法遍历所有项
        /// </summary>
        public static void Foreach<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TKey, TValue> action, int max_count = 10000)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < max_count)
            {
                action(enumerator.Current.Key, enumerator.Current.Value);
            }
            if (i >= max_count)
                LogManager.Error("Dictionary Foreach Error");
        }

        /// <summary>
        /// 提供一个方法遍历所有key值
        /// </summary>
        public static void ForeachKey<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TKey> action, int max_count = 10000)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < max_count)
            {
                action(enumerator.Current.Key);
            }
            if (i >= max_count)
                LogManager.Error("Dictionary Foreach Error");
        }

        /// <summary>
        /// 提供一个方法遍历所有value值
        /// </summary>
        public static void ForeachValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, Action<TValue> action, int max_count = 1000)
        {
            if (action == null) return;
            var enumerator = dic.GetEnumerator();
            int i = 0;
            while (enumerator.MoveNext() && i++ < max_count)
            {
                action(enumerator.Current.Value);
            }
            if (i >= max_count)
                LogManager.Error("Dictionary Foreach Error");
        }

    }
}
