using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class EntitesManager : I_Update
    {
        #region 属性

        public static EntitesManager Instance =new EntitesManager();
        public List<BaseEntity> entites = new List<BaseEntity>();

        public BaseEntity Manual { get; private set; }
        #endregion


        #region

        public void OnUpdate(float dt)
        {
            int length = entites.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                entites[i].OnUpdate(dt);
            }
        }

        public void AddEntity(BaseEntity entity)
        {
            entites.Add(entity);
        }

        public void SetManual(BaseEntity entity)
        {
            Manual = entity;
        }

        public void RemoveEntity(BaseEntity entity)
        {

        }

        #endregion
    }
}

