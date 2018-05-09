using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 唯一的OnUpdate的入口
    /// 对所有的OnUpdate做同一的管理，需要提前初始化
    /// </summary>
    public class UpdateGameObject : MonoBehaviour
    {
        public TimerManager time_manager;

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

        public void OnInit()
        {
            time_manager = TimerManager.Instance;
        }

        void Update()
        {
            time_manager.OnUpdate(Time.deltaTime);
        }

    }
}

