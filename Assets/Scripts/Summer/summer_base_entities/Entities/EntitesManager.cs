
using System.Collections.Generic;

namespace Summer
{
    public class EntitesManager : I_Update
    {
        #region 属性

        public static EntitesManager Instance = new EntitesManager();
        public List<BaseEntity> _entites = new List<BaseEntity>();

        public BaseEntity Manual { get; private set; }
        #endregion

        #region

        public void OnUpdate(float dt)
        {
            int length = _entites.Count;
            for (int i = length - 1; i >= 0; i--)
                _entites[i]._entityAi.UpdateAi(0, dt);

            for (int i = length - 1; i >= 0; i--)
                _entites[i]._entityAi.UpdateReqeust(0, dt);

            for (int i = length - 1; i >= 0; i--)
                _entites[i]._entityAi.UpdateBehavior(0, dt);


            for (int i = length - 1; i >= 0; i--)
            {
                _entites[i].OnUpdate(dt);
            }


        }

        public void AddEntity(BaseEntity entity)
        {
            _entites.Add(entity);
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

