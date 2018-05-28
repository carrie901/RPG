﻿using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseAsynLoadOpertion : LoadOpertion
    {
        public string _path;
        public int frame = 3;
        public bool is_complete;
        public Object _obj;
        public AssetDatabaseAsynLoadOpertion(string path)
        {
            _path = path;
        }
        protected override bool Update()
        {
#if UNITY_EDITOR
            frame--;
            if (frame > 0)
                return false;
            _obj = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(_path);
            is_complete = true;
            if (_obj == null)
                LogManager.Error("本地加载资源出错,Path:[{0}]", _path);
#endif
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

        public override void UnloadRequest()
        {

        }
    }


}

