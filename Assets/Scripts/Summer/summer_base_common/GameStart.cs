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

    }

    // Use this for initialization
    void Start()
    {
        // 所有需要调用OnUpdate(dt)方法的入口
        UpdateGameObject.Instance.OnInit();
        // 读取csv表格内容
        ConfigManager.ReadLocalConfig();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
