#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseLoader : I_ResourceLoad
    {
        public static AssetDatabaseLoader instance = new AssetDatabaseLoader();
        public const string EVN = "Assets/res_bundle/";

        public List<LoadOpertion> _load_opertions                                  //加载的请求
         = new List<LoadOpertion>(32);

        #region I_ResourceLoad

        public Object LoadAsset(string path)
        {
            return AssetDatabase.LoadAssetAtPath<Object>(EVN + path);
        }

        public LoadOpertion LoadAssetAsync(string path)
        {
            AssetDatabaseAsynLoadOpertion asyn_local = new AssetDatabaseAsynLoadOpertion(EVN + path);
            _load_opertions.Add(asyn_local);
            asyn_local.OnInit();
            return asyn_local;
        }

        public bool HasInLoading(string name)
        {
            return true;
        }

        public bool UnloadAll()
        {
            return true;
        }

        public bool UnloadAssetBundle(string assetbundle_path)
        {
            return true;
        }

        public void OnUpdate()
        {
            int length = _load_opertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (_load_opertions[i].OnUpdate())
                {
                    _load_opertions.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}
#endif
