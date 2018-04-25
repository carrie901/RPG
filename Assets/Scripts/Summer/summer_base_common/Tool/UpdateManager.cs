using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class UpdateManager : MonoBehaviour
    {
        public TimerManager timer;
        public EntitesManager entites;
        private void Start()
        {
            timer = TimerManager.Instance;
            entites = EntitesManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;
            timer.OnUpdate(dt);

            entites.OnUpdate(dt);
        }
    }
}

