using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 需要加载的资源已经处于加载状态了
    /// 需要等待别人加载好
    /// </summary>
    public class ResWaitLoadOpertion : LoadOpertion
    {
        public float _time_out;                     // 超时时间
        public float _load_time;                    // 已经加载的时间
        public string _res_path;                    // 资源的名字
        public bool _is_complete;                   // 加载完成
        public ResWaitLoadOpertion(string res_path, float time_out)
        {
            _res_path = res_path;
            _time_out = time_out;
            _load_time = 0;
            _is_complete = false;
        }

        #region public 

        public override void UnloadRequest()
        {

        }

        #region 生命周期


        protected override void Init()
        {

        }

        protected override bool Update()
        {
            _load_time += Time.timeScale * Time.deltaTime;
            // 1.超时就强制性质完成
            if (_load_time > _time_out)
            {
                ResLog.Error("OabDepLoadOpertion,超时加载[{0}]", _res_path);
                return false;
            }

            
            // 2.处于加载状态
            _is_complete = ResLoader.instance.ContainsRes(_res_path);
            // 3.如果还出加载状态，返回未完成
            if (!_is_complete)
                return false;
            return true;
        }

        protected override void Complete()
        {

        }

        #endregion

        #endregion
    }
}

