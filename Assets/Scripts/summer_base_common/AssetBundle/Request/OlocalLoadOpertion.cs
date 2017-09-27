#if UNITY_EDITOR
using UnityEditor;
using Object = UnityEngine.Object;

namespace Summer
{
    public class OlocalLoadOpertion : OloadOpertion
    {
        public string _path;
        public int frame = 3;
        public bool is_complete;
        public Object _obj;
        public OlocalLoadOpertion(string path)
        {
            _path = path;
        }
        public override bool Update()
        {
            frame--;
            if (frame > 0)
                return false;
            _obj = AssetDatabase.LoadAssetAtPath<Object>(_path);
            is_complete = true;
            if (_obj == null)
                LogManager.Error("本地加载资源出错,Path:[{0}]", _path);
            return true;
        }

        public override bool IsDone()
        {
            return is_complete;
        }

        public override Object GetAsset()
        {
            return _obj;
        }

        public override void UnloadAssetBundle()
        {

        }
    }
#endif

}

