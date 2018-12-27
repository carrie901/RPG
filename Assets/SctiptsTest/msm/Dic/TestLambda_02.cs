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
        flag = true;
    }


    public void Test01()
    {
        TestLam1 t1 = new TestLam1(10);
        TestLam1 t2 = new TestLam1(5);
        Action[] actions = new Action[10];
        for (var i = 0; i < actions.Length; i++)
        {
            int t = i;
            actions[i] = () =>
            {
                Debug.Log("t1:" + t1.Id);
                Debug.Log("t:" + t);
            };
        }
        t1 = t2;
        for (int i = 0; i < actions.Length; i++)
        {
            Action a = actions[i];
            a();
        }
    }

}

public class TestLam1
{
    public int Id;

    public TestLam1(int id)
    {
        Id = id;
    }
}

