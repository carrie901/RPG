using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScriptableObject : ScriptableObject
{
    public string testName = "";

    //注意：基本数据类型以外的成员类型需要加 SerializeField 关键字
    [UnityEngine.SerializeField]
    public List<MyDataInfo> myData = new List<MyDataInfo>();
}
