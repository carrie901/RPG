using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer.Test
{
    public class GameStarter : MonoBehaviour
    {

        void Awake()
        {
            
        }
        void Start()
        {
            //init timer
            GameTimer.Instance.Init();
            //add game updater and trigger update
            gameObject.AddComponent<GameUpdater>();
        }
        void Destroy()
        {
           
        }
    }
}