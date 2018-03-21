#if UNITY_EDITOR

using UnityEditor;

namespace Summer
{
    /// <summary>
    /// 本地异步加载
    /// </summary>
    public class LocalAsynOpertion : OloadOpertion
    {
        public string _path;
        public int frame = 3;                   // 延迟3帧
        public bool is_complete;
        public UnityEngine.Object _obj;

        public LocalAsynOpertion(string path)
        {
            _path = path;
        }

        public override bool Update()
        {
            frame--;
            if (frame >= 0)
                return false;
            _obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(_path);
            is_complete = true;
            if (_obj == null)
                ResLog.Error("本地加载资源出错,Path:[{0}]", _path);
            return true;
        }

        public override bool IsDone()
        {
            return is_complete;
        }

        public override UnityEngine.Object GetAsset()
        {
            return _obj;
        }

        public override void UnloadAssetBundle()
        {

        }
    }
}
#endif
