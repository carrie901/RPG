#if UNITY_EDITOR
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseAsynLoadOpertion : LoadOpertion
    {
        public int frame = 3;
        public Object _obj;
        public AssetInfo _aset_info;
        public AssetDatabaseAsynLoadOpertion(string path)
        {
            RequestResPath = path;
        }

        #region public 

        public override void UnloadRequest()
        {
            base.UnloadRequest();
            _obj = null;
            _aset_info = null;
        }

        #region 生命周期

        protected override void Init()
        {

        }

        protected override bool Update()
        {
            frame--;
            if (frame > 0) return false;




            _obj = UnityEditor.AssetDatabase.LoadAssetAtPath<Object>(RequestResPath);
            if (_obj != null)
            {
                return true;
            }
            else
            {
                LogManager.Error("本地加载资源出错,Path:[{0}]", RequestResPath);
                ForceExit(string.Format("本地加载资源出错,Path:[{0}]", RequestResPath));
                return false;
            }

        }

        protected override void Complete()
        {
            if (_aset_info == null)
            {
                _aset_info = new AssetInfo(_obj, RequestResPath);
            }
        }

        #endregion

        #endregion
    }
}
#endif

