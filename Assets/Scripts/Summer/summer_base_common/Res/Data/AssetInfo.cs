﻿using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    /// <summary>
    /// TODO 资源信息需要在加载之前创建，还是在加载之后之后创建
    ///     有上面的信息来得到资源加载的错误信息
    /// TODO Bug
    ///     如果内部有重名的情况下就会发生错误
    /// </summary>
    [System.Serializable]
    public class AssetInfo
    {
        //资源对象  
        public Object _object;
        public string ResPath { get; private set; }                                     // 路径  
        //public string ResName { get; private set; }                                   // 名字
        public int RefCount { get; set; }                                               // 读取次数  

        public AssetInfo(Object obj, ResRequestInfo resInfo)
        {
            _object = obj;
            ResPath = resInfo.ResPath;
            RefCount = 0;
        }

        public AssetInfo(Object obj, string resName, string resPath)
        {
            _object = obj;
            ResPath = resPath;
            RefCount = 0;
            ResLog.Assert(!string.IsNullOrEmpty(ResPath), "名字有异常:[{0}]", _object);
        }

        public AssetInfo(Object obj, string resPath)
        {
            _object = obj;
            ResPath = resPath;
            RefCount = 0;
            ResLog.Assert(!string.IsNullOrEmpty(ResPath), "名字有异常:[{0}]", _object);
        }

        public T GetAsset<T>() where T : Object
        {
            if (_object == null)
            {
                ResLog.Error("AssetInfo Error,Object Is Null. Path:[{0}]", ResPath);
                return null;
            }
            T t = _object as T;
            if (t == null)
            {
                ResLog.Error("AssetInfo Error,Object 类型不对.Path:[{0}]", ResPath);
            }
            return t;
        }

        public void UnLoad()
        {
            _object = null;

        }
    }
}


