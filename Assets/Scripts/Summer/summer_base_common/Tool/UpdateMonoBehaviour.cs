using UnityEngine;
using Summer.Loader;
namespace Summer
{
    public class UpdateMonoBehaviour : MonoBehaviour
    {
        public TimerManager timer;
        public EntitesManager entites;
        public ResLoader rse_loader;
        private void Start()
        {
            timer = TimerManager.Instance;
            entites = EntitesManager.Instance;
            rse_loader = ResLoader.instance;
        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;
            timer.OnUpdate(dt);

            entites.OnUpdate(dt);

            rse_loader.OnUpdate(dt);
        }
    }
}

