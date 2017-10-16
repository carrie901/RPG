using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class ResoucesLoader : I_ResourceLoad
    {
        public static ResoucesLoader instance = new ResoucesLoader();
        public List<OloadOpertion> _load_opertions                                  //加载的请求
          = new List<OloadOpertion>(32);

        #region I_ResourceLoad

        public Object LoadAsset(string path)
        {
            return Resources.Load(path);
        }

        public OloadOpertion LoadAssetAsync(string path)
        {
            OresLoadOpertion res_opertion = new OresLoadOpertion(path);
            _load_opertions.Add(res_opertion);
            return res_opertion;
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
