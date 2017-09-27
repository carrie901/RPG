using UnityEngine;
using System.Collections;

namespace Summer
{
    /// <summary>
    /// 需要加载的资源已经处于加载状态了
    /// 需要等待别人加载好
    /// </summary>
    public class OabLoadWaitOpertion : OloadOpertion
    {
        public float _time_out;
        public float _load_time = 0;
        public string _assetbundle_name;
        public bool _loading = true;            //处于加载中
        public OabLoadWaitOpertion(string assetbundle_name,float time_out)
        {
            _assetbundle_name = assetbundle_name;
            _time_out = time_out;
        }

        public override bool Update()
        {
            _load_time += Time.timeScale * Time.deltaTime;
            // 1.超时就强制性质完成
            if (_load_time > _time_out)
            {
                LogManager.Error("OabDepLoadOpertion,超时加载[{0}]", _assetbundle_name);
                return true;
            }

            // 2.处于加载状态
            _loading = (AssetBundleLoader.instance.ContainsLoadAssetBundles(_assetbundle_name));
            // 3.如果还出加载状态，返回未完成
            if (_loading)
                return false;
            return true;
        }

        public override bool IsDone()
        {
            if (_loading)//处于加载状态 返回没有完成
                return false;
            return true;//已经加载完成了，返回完成
        }

        public override Object GetAsset()
        {
            return null;
        }

        public override void UnloadAssetBundle()
        {
            
        }
    }
}

