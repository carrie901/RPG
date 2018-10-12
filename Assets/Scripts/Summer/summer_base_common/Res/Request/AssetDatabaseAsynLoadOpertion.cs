#if UNITY_EDITOR
using System;
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseAsynLoadOpertion<T> : ResLoadOpertion where T : Object
    {
        private int _frame = 1;
        private T _obj;
        public AssetDatabaseAsynLoadOpertion(string path)
        {
            RequestResPath = path;
            _frame = UnityEngine.Random.Range(1, 10);
        }

        #region public 

        #region 生命周期

        protected override void Init()
        {

        }

        protected override bool Update()
        {
            _frame--;
            if (_frame > 0) return false;

            _obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(RequestResPath);
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
            if (_assetInfo == null)
            {
                _assetInfo = new AssetInfo(_obj, RequestResPath);
            }
        }

        #endregion

        #endregion
    }
}
#endif

