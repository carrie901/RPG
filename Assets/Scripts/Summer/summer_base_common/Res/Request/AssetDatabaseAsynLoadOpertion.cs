#if UNITY_EDITOR
//using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseAsynLoadOpertion : ResLoadOpertion
    {
        private const int MaxFrame = 3;
        private int _frame;
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
            _frame++;
            if (_frame <= MaxFrame) return false;
            return true;
        }

        protected override void Complete() { }

        public override I_ObjectInfo GetAsset<T>(string resPath)
        {
            T obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(RequestResPath);
            if (obj == null)
            {
                ResLog.Error("本地加载资源出错,Path:[{0}]", RequestResPath);
                ForceExit(string.Format("本地加载资源出错,Path:[{0}]", RequestResPath));
                return null;
            }
            I_ObjectInfo objectInfo = new ResInfo(obj, RequestResPath);
            return objectInfo;
        }

        #endregion

        #endregion
    }
}
#endif

