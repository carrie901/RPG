using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;

/// <summary>
/// 游戏的唯一入口
/// </summary>
public class GameStart : MonoBehaviour
{

    private void Awake()
    {
        UpdateGameObject.Instance.OnInit();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
