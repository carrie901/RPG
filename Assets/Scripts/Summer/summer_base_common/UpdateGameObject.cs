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
        public TouchEffect _touchEffect;

        public List<I_Update> _updateList = new List<I_Update>();

        #region static

        protected static UpdateGameObject _instance = null;
        protected static GameObject _updateRoot = null;
        public static UpdateGameObject Instance
        {
            get
            {
                if (_instance == null)
                {
                    _updateRoot = new GameObject("UpdateRoot");
                    _instance = _updateRoot.AddComponent<UpdateGameObject>();
                    DontDestroyOnLoad(_updateRoot);

                }
                return _instance;
            }
        }

        #endregion

        #endregion

        [RuntimeInitializeOnLoadMethod]
        public static void OnStart()
        {
            Resources.UnloadUnusedAssets();
            // 所有需要调用OnUpdate(dt)方法的入口
            Debug.Log("---------------------必须项-->初始化UpdateGameObject-------------------");
            UpdateGameObject.Instance.OnInit();
        }

        private void OnInit()
        {
            _timeManager = TimerManager.Instance;
            _entites = EntitesManager.Instance;
            _rseLoader = ResLoader.Instance;
            _spritePool = SpritePool.Instance;


            Application.targetFrameRate = 30;
        }

        void Update()
        {
            float dt = Time.deltaTime;
            // 强制为第一个执行
            _timeManager.OnUpdate(dt);

            _spritePool.OnUpdate(dt);
          
            _entites.OnUpdate(dt);

            _rseLoader.OnUpdate(dt);
        }

        void OnApplicationQuit()
        {
            LogManager.Quit();
        }
    }
}

