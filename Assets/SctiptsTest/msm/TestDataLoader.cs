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
        EntitesManager.Instance.SetManual(entity);

        for (int i = 0; i < 8; i++)
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
