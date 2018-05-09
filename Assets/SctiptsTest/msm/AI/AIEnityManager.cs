using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Test
{
    public class AIEnityManager : TSingleton<AIEnityManager>
    {

        public delegate int AIEntityUpdater(AIEntity entity, float game_time, float delta_time);
        public List<AIEntity> _entites = new List<AIEntity>();

        public AIEnityManager()
        {
        }
        public void AddEntity(AIEntity e)
        {
            _entites.Add(e);
        }
        public void IteratorDo(AIEntityUpdater updater, float game_time, float delta_time)
        {
            foreach (AIEntity e in _entites)
            {
                updater(e, game_time, delta_time);
            }
        }

    }

}

