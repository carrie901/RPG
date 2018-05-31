using Object = UnityEngine.Object;

namespace Summer
{
    /// <summary>
    /// TODO 资源信息需要在加载之前创建，还是在加载之后之后创建
    ///     有上面的信息来得到资源加载的错误信息
    /// </summary>
    public class AssetInfo
    {
        //资源对象  
        public Object _object;
        public string ResPath { get; set; }                                        //路径  
        //public string ResName { get; private set; }                 //名字
        public int RefCount { get; set; }                                       //读取次数  

        public AssetInfo(Object obj, ResRequestInfo res_info)
        {
            _object = obj;
            //ResName = res_info.res_name;
            ResPath = res_info.res_path;
        }

        public AssetInfo(Object obj, string res_name, string res_path)
        {
            _object = obj;
            //ResName = obj.name;
            ResPath = res_path;
            ResLog.Assert(!string.IsNullOrEmpty(ResPath), "名字有异常:[{0}]", _object);
        }

        /*public AssetInfo(Object obj)
        {
            _object = obj;
            //ResName = obj.name;
            ResPath = obj.name;
            ResLog.Assert(!string.IsNullOrEmpty(ResPath), "名字有异常:[{0}]", _object);
        }*/

        public AssetInfo(Object obj,string res_path)
        {
            _object = obj;
            //ResName = obj.name;
            ResPath = res_path;
            ResLog.Assert(!string.IsNullOrEmpty(ResPath), "名字有异常:[{0}]", _object);
        }


        public T GetAsset<T>() where T : Object
        {
            T t = _object as T;
            if (t == null)
            {
                ResLog.Error("AssetInfo is Error,Info:[{0}],GameResType:[{1}]", ResPath);
            }
            return t;
        }
    }
}


