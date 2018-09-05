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

        public TimerManager time_manager;
        public EntitesManager entites;
        public ResLoader rse_loader;
        public SpritePool sprite_pool;

        public List<I_Update> _update_list = new List<I_Update>();

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
            time_manager = TimerManager.Instance;
            entites = EntitesManager.Instance;
            rse_loader = ResLoader.instance;
            sprite_pool = SpritePool.Instance;


            Application.targetFrameRate = 30;
        }

        void Update()
        {
            float dt = Time.deltaTime;
            sprite_pool.OnUpdate(dt);
            time_manager.OnUpdate(dt);


            entites.OnUpdate(dt);

            rse_loader.OnUpdate(dt);
        }

    }
}

