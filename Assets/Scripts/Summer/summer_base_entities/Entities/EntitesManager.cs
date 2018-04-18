using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class EntitesManager : MonoBehaviour
    {
        #region 属性

        public static EntitesManager Instance;
        public List<BaseEntity> entites = new List<BaseEntity>();

        public BaseEntity manual { get; private set; }
        public float _last_time;
        #endregion

        #region Mono

        void Awake()
        {
            _last_time = Time.realtimeSinceStartup;
            LogManager.Assert(Instance == null, "EntiityControllerManager已经被实例化");
            Instance = this;
        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.realtimeSinceStartup - _last_time;
            _last_time = Time.realtimeSinceStartup;
            int length = entites.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                entites[i].OnUpdate(dt);
            }
        }

        #endregion

        #region

        public void AddEntity(BaseEntity entity)
        {
            entites.Add(entity);
        }

        public void SetManual(BaseEntity entity)
        {
            manual = entity;
        }

        public void RemoveEntity(BaseEntity entity)
        {

        }

        #endregion
    }
}

