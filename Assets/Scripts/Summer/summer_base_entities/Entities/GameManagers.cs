using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;

public class GameManagers : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {


    }

    private void OnEnable()
    {
        CameraEffectManager.instance.RegisterHandler();
    }

    private void OnDisable()
    {
        CameraEffectManager.instance.UnRegisterHandler();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
