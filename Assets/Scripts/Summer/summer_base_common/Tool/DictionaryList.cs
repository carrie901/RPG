using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 15:18:30
// FileName : DictionaryList.cs
//=============================================================================

namespace Summer.Tool
{
    /// <summary>
    /// 可以使用数组访问的Dictionary
    /// </summary>
    public class DictionaryList<Tkey, TValue> : IDisposable
    {

        private Dictionary<Tkey, TValue> _dic = new Dictionary<Tkey, TValue>();
        private List<TValue> _list = new List<TValue>();

        /// <summary>
        /// 获取一个值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue GetValue(Tkey key)
        {
            TValue tar = default(TValue);
            _dic.TryGetValue(key, out tar);
            return tar;
        }

        /// <summary>
        /// 获取特定个数的值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TValue GetValueAt(int index)
        {
            return _list[index];
        }

        /// <summary>
        /// 添加一个项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public bool Add(Tkey key, TValue value)
        {
            if (!_dic.ContainsKey(key))
            {
                _dic.Add(key, value);
                _list.Add(value);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 移除一个项目
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(Tkey key)
        {
            TValue tar = default(TValue);
            _dic.TryGetValue(key, out tar);

            if (tar != null)
            {
                _list.Remove(tar);
                _dic.Remove(key);
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 清理数据结构
        /// </summary>
        public void Clear()
        {
            _dic.Clear();
            _list.Clear();
        }

        public bool ContainsKey(Tkey key)
        {
            return _dic.ContainsKey(key);
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count
        {
            get
            {
                return _dic.Count;
            }
        }

        public void Dispose()
        {
            Clear();
            _dic = null;
            _list = null;
        }
    }
}
