using System;
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetInfo
    {

        //资源对象  
        public Object _object;
        public string _asset_name;
        //资源类型  
        public E_GameResType AssetType { get; private set; }
        //路径  
        public string Path { get; set; }
        //名字
        public string Name { get { return _asset_name; } }

        //读取次数  
        public int RefCount { get; set; }

        public AssetInfo(Object obj, string asset_name, E_GameResType asset_type)
        {
            AssetType = asset_type;
            _object = obj;
            _asset_name = asset_name;
        }

        public T GetAsset<T>() where T : Object
        {
            if (_object == null)
            {
                LogManager.Error("AssetInfo is Error,Info:[{0}]", ToString());
            }
            T t = _object as T;
            if (t == null)
            {
                LogManager.Error("AssetInfo is Error,Info:[{0}]", ToString());
            }
            return t;
        }

        public override string ToString()
        {
            return string.Format("Name:[{0}],AssetType:[{1}]", Name, AssetType);
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


