using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLambda_01 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestFor();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TestFor()
    {
        Action[] actions = new Action[5];

        
        int i = 0;
        for (i = 0; i < actions.Length; i++)
        {
            InnerClass inner_class = new InnerClass();//关键在这里
            inner_class.i = i;
            actions[i] = inner_class.DoIt;
            //actions[i] = () => Debug.Log(i.ToString("f2"));
        }
        foreach (var item in actions)
        {
            item();
        }
    }

    public class InnerClass//这里是模拟编译器为lambda表达式生成的类，我暂时命名为InnerClass，实际上编译器生成的这个内部类有自己的命名规则。
    {
        public int i;//这个是捕获的for循环中的那个变量。

        public void DoIt()
        {
            Debug.Log(i.ToString("f2"));
            //Console.WriteLine(i);
        }
    }

}
