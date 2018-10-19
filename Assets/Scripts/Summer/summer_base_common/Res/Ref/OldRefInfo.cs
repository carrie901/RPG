using System;
using UnityEngine;
namespace Summer
{
    /// <summary>
    /// 老的引用信息
    /// </summary>
    public class OldRefInfo : MonoBehaviour
    {
        public string ResPath;
        void OnEnable()
        {
            Load();
        }

        void OnDisable()
        {
            UnLoad();
        }

        public virtual void Load()
        {

        }

        public virtual void UnLoad() { }

        public virtual string GetResPath() { return ResPath; }

        public virtual void SetResPath(string respath) { }
    }
}

