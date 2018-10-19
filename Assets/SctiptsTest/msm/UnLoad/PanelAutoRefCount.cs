using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 1.当前资源的依赖列表
    /// </summary>
    public class PanelAutoRefCount : MonoBehaviour, I_PoolCacheRef
    {
        #region 属性

        public string _panelResPath;
        public OldRefInfo _refCount;
        public bool _isComplete;
        #endregion

        #region MONO Override

        void OnEnable()
        {
            _isComplete = true;
            //1.通过资源路径名得到以来列表
            //2.一次加载依赖列表
            
            
        }

        private void OnDisable()
        {

        }

        #endregion

        #region public 

        public int GetRefCount()
        {
            return 0;
        }

        #endregion

        #region private 

        #endregion


    }
}