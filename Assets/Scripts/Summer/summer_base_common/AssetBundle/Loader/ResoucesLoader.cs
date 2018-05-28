using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class ResoucesLoader : I_ResourceLoad
    {
        public static ResoucesLoader instance = new ResoucesLoader();
        public List<ResAsynLoadOpertion> _load_opertions                                  //加载的请求
          = new List<ResAsynLoadOpertion>(32);

        public Dictionary<string, int> _loading =
            new Dictionary<string, int>();
        #region I_ResourceLoad

        public Object LoadAsset(string path)
        {
            return Resources.Load(path);
        }

        public LoadOpertion LoadAssetAsync(string path)
        {
            ResAsynLoadOpertion res_opertion = AddRequest(path);
            return res_opertion;
        }

        public bool HasInLoading(string res_path)
        {
            return _loading.ContainsKey(res_path);
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
                    RemoveRequest(_load_opertions[i]);
                }
            }
        }

        #endregion

        #region private 

        protected ResAsynLoadOpertion AddRequest(string res_path)
        {
            ResAsynLoadOpertion res_opertion = new ResAsynLoadOpertion(res_path);
            res_opertion.OnInit();
            _load_opertions.Add(res_opertion);
            _loading.Add(res_path, 1);
            return res_opertion;
        }

        protected void RemoveRequest(ResAsynLoadOpertion opertion)
        {
            _load_opertions.Remove(opertion);
            _loading.Remove(opertion.Path);
        }

        #endregion
    }
}
