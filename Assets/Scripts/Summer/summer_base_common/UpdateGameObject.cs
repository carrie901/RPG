using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 唯一的OnUpdate的入口
    /// 对所有的OnUpdate做同一的管理，需要提前初始化
    /// </summary>
    public class UpdateGameObject : MonoBehaviour
    {
        #region 属性

        public TimerManager _timeManager;
        public EntitesManager _entites;
        public ResLoader _rseLoader;
        public SpritePool _spritePool;

        public List<I_Update> _updateList = new List<I_Update>();

        #region static

        protected static UpdateGameObject _instance = null;
        protected static GameObject _update_root = null;
        public static UpdateGameObject Instance
        {
            get
            {
                if (_instance == null)
                {
                    _update_root = new GameObject("UpdateRoot");
                    _instance = _update_root.AddComponent<UpdateGameObject>();
                    MonoBehaviour.DontDestroyOnLoad(_update_root);

                }
                return _instance;
            }
        }

        #endregion

        #endregion

        public void OnInit()
        {
            _timeManager = TimerManager.Instance;
            _entites = EntitesManager.Instance;
            _rseLoader = ResLoader.instance;
            _spritePool = SpritePool.Instance;


            Application.targetFrameRate = 30;
        }

        void Update()
        {
            float dt = Time.deltaTime;
            _spritePool.OnUpdate(dt);
            _timeManager.OnUpdate(dt);


            _entites.OnUpdate(dt);

            _rseLoader.OnUpdate(dt);
        }

    }
}

