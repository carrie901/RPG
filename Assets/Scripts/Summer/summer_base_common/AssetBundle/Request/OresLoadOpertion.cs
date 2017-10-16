using UnityEngine;
using System.Collections;

namespace Summer
{
    public class OresLoadOpertion : OloadOpertion
    {
        protected ResourceRequest _request = null;
        public string _path;
        public OresLoadOpertion(string path)
        {
            _path = path;
        }

        public override bool Update()
        {
            if (_request != null)
                return false;
            _request = Resources.LoadAsync(_path);
            if (_request != null)
                return true;
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
