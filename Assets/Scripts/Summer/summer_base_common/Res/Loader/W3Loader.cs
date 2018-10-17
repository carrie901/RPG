using Object = UnityEngine.Object;
namespace Summer
{
    public class W3Loader : I_ResourceLoad
    {
        public static W3Loader instance = new W3Loader();

        public I_ObjectInfo LoadAsset<T>(string resPath) where T : Object
        {
            return null;
        }

        public ResLoadOpertion LoadAssetAsync<T>(string resPath) where T : Object
        {
            return null;
        }

        public bool UnloadAll()
        {
            return false;
        }

        public bool UnloadAsset(I_ObjectInfo objectInfo)
        {
            return false;
        }
        public bool UnLoadAssetRef(I_ObjectInfo objectInfo)
        {
            return false;
        }
        public void OnUpdate() { }
        public string GetResPath(string resPath)
        {
            return resPath;
        }
        public void CheckInfo()
        {

        }
    }
}

