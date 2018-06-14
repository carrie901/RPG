using UnityEngine;
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
        public string ResPath;                                       //路径  
        //public string ResName { get; private set; }                 //名字
        public int RefCount;                                    //读取次数  

        public AssetInfo(Object obj, ResRequestInfo res_info)
        {
            _object = obj;
            ResPath = res_info.res_path;
            RefCount = 0;
        }

        public AssetInfo(Object obj, string res_name, string res_path)
        {
            _object = obj;
            ResPath = res_path;
            RefCount = 0;
            ResLog.Assert(!string.IsNullOrEmpty(ResPath), "名字有异常:[{0}]", _object);
        }

        public AssetInfo(Object obj, string res_path)
        {
            _object = obj;
            ResPath = res_path;
            RefCount = 0;
            ResLog.Assert(!string.IsNullOrEmpty(ResPath), "名字有异常:[{0}]", _object);
        }


        public T GetAsset<T>() where T : Object
        {
            T t = _object as T;
            if (t == null)
            {
                ResLog.Error("AssetInfo Error,Info:[{0}]", ResPath);
            }
            return t;
        }
    }
}


