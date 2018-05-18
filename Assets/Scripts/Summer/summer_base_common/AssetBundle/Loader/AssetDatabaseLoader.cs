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

        public List<OloadOpertion> _load_opertions                                  //加载的请求
         = new List<OloadOpertion>(32);

        #region I_ResourceLoad

        public Object LoadAsset(string path)
        {
            return AssetDatabase.LoadAssetAtPath<Object>(EVN + path);
        }

        public OloadOpertion LoadAssetAsync(string path)
        {
            LocalAsynOpertion asyn_local = new LocalAsynOpertion(EVN + path);
            _load_opertions.Add(asyn_local);
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

        public void Update()
        {
            int length = _load_opertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (_load_opertions[i].Update())
                {
                    _load_opertions.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}
#endif
