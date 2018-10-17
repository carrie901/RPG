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
    /*[System.Serializable]
    public class AssetInfo
    {
        //资源对象  
        public Object _object;
        /// <summary>
        /// 资源的相对路径 Raw/xx/xx
        /// </summary>
        public string ResPath { get; private set; }                                     // 路径  
        //public string ResName { get; private set; }                                   // 名字
        public int RefCount { get; private set; }                                               // 读取次数  

        public AssetInfo(Object obj, string resPath)
        {
            _object = obj;
            ResPath = resPath;
            RefCount = 0;
            ResLog.Assert(obj != null, "AssetInof的Obj为空,路径:[{0}]", _object);
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
                ResLog.Error("AssetInfo Error,Object 类型不对.Path:[{0}],提取类型:[{1}],实际类型:[{2}]", ResPath, typeof(T), _object);
            }
            RefCount++;
            return t;
        }

        public bool CheckType<T>() where T : Object
        {
            return _object is T;
        }

        public void UnLoad()
        {
            RefCount--;
        }

        public void UnLoadReal()
        {
            //UnityEngine.Object.Destroy(_object);
            _object = null;
        }
    }*/
}


