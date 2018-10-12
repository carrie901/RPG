using UnityEngine;
namespace Summer
{
    public class ResAsynLoadOpertion : ResLoadOpertion
    {

        protected ResourceRequest _request;
        public string _path;
        public ResAsynLoadOpertion(string path)
        {
            RequestResPath = path;
        }

        #region pubilc

        public override void UnloadRequest()
        {
            base.UnloadRequest();
            _request = null;
        }

        #region 生命周期

        protected override void Init()
        {
            ResLog.Log("Res:{0}", _path);
            _request = Resources.LoadAsync(_path);
        }

        protected override bool Update()
        {
            if (_request == null) return false;
            ResLog.Log("等待加载:{0},加载状态{1}", RequestResPath, _request.isDone);
            return _request.isDone;
        }

        protected override void Complete()
        {
            if (_assetInfo == null)
            {
                _assetInfo = new AssetInfo(_request.asset, RequestResPath);
            }
        }

        #endregion

        #endregion
    }

}
