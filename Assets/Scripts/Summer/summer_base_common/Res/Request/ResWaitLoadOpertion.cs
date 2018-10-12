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

        public override void OnUpdate()
        {
            if (_isComplete) return;

            _loadTime += Time.timeScale * Time.deltaTime;

            // 1.处于加载状态
            _isComplete = ResLoader.instance.ContainsRes(_resPath);

            // 2.超时就强制性质完成
            if (_loadTime > _timeOut)
            {
                _isComplete = true;
            }
        }

        public override bool IsDone()
        {
            ResLog.Log("ResWaitLoadOpertion:[{0}] Is [{1}]", _resPath, _isComplete);
            return _isComplete;
        }

        public void OutResult()
        {
            if (_loadTime > _timeOut)
            {
                Error = string.Format("ResWaitLoadOpertion 超时加载[{0}]", _resPath);
                LogManager.Error(Error);
            }
        }

        #endregion

    }
}

