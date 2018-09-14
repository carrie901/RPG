#if UNITY_EDITOR
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseAsynLoadOpertion : LoadOpertion
    {
        private int _frame = 3;
        private Object _obj;
        private AssetInfo _asetInfo;
        public AssetDatabaseAsynLoadOpertion(string path)
        {
            RequestResPath = path;
        }

        #region public 

        public override void UnloadRequest()
        {
            base.UnloadRequest();
            _obj = null;
            _asetInfo = null;
        }

        #region 生命周期

        protected override void Init()
        {

        }

        protected override bool Update()
        {
            _frame--;
            if (_frame > 0) return false;




            _obj = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(RequestResPath);
            if (_obj != null)
            {
                return true;
            }
            else
            {
                ResLog.Error("本地加载资源出错,Path:[{0}]", RequestResPath);
                ForceExit(string.Format("本地加载资源出错,Path:[{0}]", RequestResPath));
                return false;
            }

        }

        protected override void Complete()
        {
            if (_asetInfo == null)
            {
                _asetInfo = new AssetInfo(_obj, RequestResPath);
            }
        }

        #endregion

        #endregion
    }
}
#endif

