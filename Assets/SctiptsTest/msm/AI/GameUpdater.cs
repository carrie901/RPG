﻿using UnityEngine;

namespace Summer.Test
{
    class GameUpdater : MonoBehaviour
    {
        private AIEnityManager.AIEntityUpdater _aiUpdater;
        private AIEnityManager.AIEntityUpdater _requestUpdater;
        private AIEnityManager.AIEntityUpdater _behaviorUpdater;
        void Awake()
        {
            _aiUpdater =
                delegate (AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateAi(gameTime, deltaTime);
                };
            _requestUpdater =
                delegate (AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateReqeust(gameTime, deltaTime);
                };
            _behaviorUpdater =
                delegate (AIEntity entity, float gameTime, float deltaTime)
                {
                    return entity.UpdateBehavior(gameTime, deltaTime);
                };
        }
        void Update()
        {
            //update global timer
            GameTimer.Instance.UpdateTime();

            float deltaTime = GameTimer.Instance.deltaTime;
            float gameTime = GameTimer.Instance.gameTime;

            AIEnityManager entityMgr = AIEnityManager.Instance;
            //Update AI
            entityMgr.IteratorDo(_aiUpdater, gameTime, deltaTime);
            //Update Request
            entityMgr.IteratorDo(_requestUpdater, gameTime, deltaTime);
            //Update Behaivor
            entityMgr.IteratorDo(_behaviorUpdater, gameTime, deltaTime);
        }
        //----------------------------------------------------------
        //Debug UI
        //----------------------------------------------------------
        void OnGUI()
        {
            //speed up/slow down
            Time.timeScale = GUILayout.HorizontalSlider(Time.timeScale, 0, 2);
            //add unity
            if (GUILayout.Button("Add Entity"))
            {
                ResRequestInfo res_request = ResRequestFactory.CreateRequest<GameObject>("Zombie/z", E_GameResType.quanming);
                GameObject go = ResManager.instance.LoadPrefab(res_request);//GameResourceManager.instance.LoadResource("Zombie/z");
                if (go != null)
                {
                    AIEnityManager.Instance.AddEntity(go.AddComponent<AIEntity>().Init());
                }
            }
        }
    }
}
