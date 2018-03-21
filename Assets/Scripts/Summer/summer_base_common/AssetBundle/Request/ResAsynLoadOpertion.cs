using UnityEngine;
namespace Summer
{
    public class ResAsynLoadOpertion : OloadOpertion
    {
        public string Path { get { return _path; } }

        protected ResourceRequest _request = null;
        public string _path;
        public ResAsynLoadOpertion(string path)
        {
            _path = path;
        }

        public override bool Update()
        {
            if (_request == null)
            {
                ResLog.Log("Res:{0}", _path);
                _request = Resources.LoadAsync(_path);
            }

            if (_request != null)
            {
                ResLog.Log("等待加载:{0},加载状态{1}", Path, _request.isDone);
                if (_request.isDone)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public override bool IsDone()
        {
            if (_request == null)
                return false;
            return _request.isDone;
        }

        public override Object GetAsset()
        {
            if (_request != null && _request.isDone)
                return _request.asset;
            else
                return null;
        }

        public override void UnloadAssetBundle()
        {

        }
    }

}
