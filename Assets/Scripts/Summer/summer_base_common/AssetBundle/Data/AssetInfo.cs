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
        public string _asset_name;
        //资源类型  
        public E_GameResType GameResType { get; private set; }
        public E_AssetType AssetType { get; private set; }
        //路径  
        public string Path { get; set; }
        //名字
        public string Name { get { return _asset_name; } }

        //读取次数  
        public int RefCount { get; set; }

        public AssetInfo(Object obj, string asset_name, E_GameResType game_res_type)
        {
            GameResType = game_res_type;
            _object = obj;
            _asset_name = asset_name;
            AssetType = E_AssetType.none;
        }

        public AssetInfo(Object obj)
        {
            GameResType = E_GameResType.shanchu;
            _object = obj;
            _asset_name = obj.name;

            AssetType = E_AssetType.teshu;
            ResLog.Assert(!string.IsNullOrEmpty(_asset_name), "名字有异常:[{0}]", _object);
        }

        public T GetAsset<T>() where T : Object
        {
            T t = _object as T;
            if (t == null)
            {
                ResLog.Error("AssetInfo is Error,Info:[{0}],GameResType:[{1}]", Name, GameResType);
            }
            return t;
        }

        public string get_load_info()
        {
            return string.Format("{0},{1},{2}", _asset_name, load_time, async);
        }

        //TODO 
        //FIXME
        public float load_time = 0;
        public bool async = false;
    }
}


