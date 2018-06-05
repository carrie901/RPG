using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TestLambda_02 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public bool flag = true;
    // Update is called once per frame
    void Update()
    {



        if (flag) return;
        Test01();
    }

    public void Test03()
    {
        UnityEngine.Profiling.Profiler.BeginSample("NewClass");
        NewClass new_class = new NewClass();
        UnityEngine.Profiling.Profiler.EndSample();
    }

    public void Test01()
    {
        Action[] actions = new Action[10];
        int j = 0;
        for (var i = 0; i < actions.Length; i++)
        {
            actions[i] = () =>
            {
                int tmp = i;
                int tmpj = j;
            };
        }
    }

}


public class NewClass
{
    public List<int> sb = new List<int>()
    {
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        0,
        10,
        1,
        1,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
           1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        0,
        10,
        1,
        1,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
           1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        0,
        10,
        1,
        1,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
        1,
        1,
        1,
        11,
        1,
        1,
    };
    public NewClass()
    {

    }
}