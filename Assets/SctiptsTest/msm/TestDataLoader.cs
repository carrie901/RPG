using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;

public class TestDataLoader : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StaticCnfLoader.LoadAllCsvFile();

        BaseEntity entity = EntityPool.Instance.Pop(1001001);
        EntitesManager.Instance.AddEntity(entity);
        GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_set_player, entity);
        EntitesManager.Instance.SetManual(entity);

        int count = 0;
        for (int i = 0; i < count; i++)
        {
            BaseEntity tmp = EntityPool.Instance.Pop(1001001);
            tmp.InitPosRot();
            EntitesManager.Instance.AddEntity(tmp);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
