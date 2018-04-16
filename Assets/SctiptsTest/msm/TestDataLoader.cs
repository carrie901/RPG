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
        EntiityControllerManager.Instance.entites[0].InitEntity(1001001);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
