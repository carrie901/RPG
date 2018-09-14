using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 需要加载的资源已经处于加载状态了
    /// 需要等待别人加载好
    /// </summary>
    public class ResWaitLoadOpertion : LoadOpertion
    {
        public float _timeOut;                     // 超时时间
        public float _loadTime;                    // 已经加载的时间
        public string _resPath;                    // 资源的名字
        public bool _isComplete;                   // 加载完成
        public ResWaitLoadOpertion(string resPath, float timeOut)
        {
            _resPath = resPath;
            _timeOut = timeOut;
            _loadTime = 0;
            _isComplete = false;
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
            _loadTime += Time.timeScale * Time.deltaTime;
            // 1.超时就强制性质完成
            if (_loadTime > _timeOut)
            {
                ResLog.Error("OabDepLoadOpertion,超时加载[{0}]", _resPath);
                return false;
            }

            
            // 2.处于加载状态
            _isComplete = ResLoader.instance.ContainsRes(_resPath);
            // 3.如果还出加载状态，返回未完成
            if (!_isComplete)
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

