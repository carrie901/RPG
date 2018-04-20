using System.Collections;
using System.Collections.Generic;
using Summer;
using UnityEngine;
using UnityEngine.Profiling;


public enum E_Test
{
    one,
    two,
}
public class TestEnum : MonoBehaviour
{

    public E_Test test = E_Test.one;
    public Dictionary<E_Test, int> data = new Dictionary<E_Test, int>();
    // Use this for initialization
    void Start()
    {
        data.Add(E_Test.one, 1);
        data.Add(E_Test.two, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Profiling.Profiler.BeginSample("01");  // 未使用外部变量的匿名函数
        TestFun(E_Test.two);
        //UnityEngine.Profiling.Profiler.EndSample();
    }

    public void TestFun(E_Test t)
    {
        UnityEngine.Profiling.Profiler.BeginSample("01");
/*        int num01 = data[t];
        if (data.ContainsKey(t))
        {

        }*/

        foreach (var var in data)
        {
            
        }
        UnityEngine.Profiling.Profiler.EndSample();


        UnityEngine.Profiling.Profiler.BeginSample("02");
        int num = 0;
        data.TryGetValue(t, out num);
        UnityEngine.Profiling.Profiler.EndSample();

        if (test == t)
        {

        }
        else
        {

        }
    }
}
