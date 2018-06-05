using System.Collections;
using System.Collections.Generic;
using Summer.AI;
using UnityEngine;

namespace Summer.Test
{
    public class AIEnityManager : TSingleton<AIEnityManager>
    {

        public delegate int AIEntityUpdater(BtEntityAi entity, float game_time, float delta_time);
        public List<BtEntityAi> _entites = new List<BtEntityAi>();

        public AIEnityManager()
        {
        }
        public void AddEntity(BtEntityAi e)
        {
            _entites.Add(e);
        }
        public void IteratorDo(AIEntityUpdater updater, float game_time, float delta_time)
        {
            foreach (BtEntityAi e in _entites)
            {
                updater(e, game_time, delta_time);
            }
        }

    }

}

