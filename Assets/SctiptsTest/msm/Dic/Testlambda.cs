using System;
using System.Collections.Generic;
using Summer.Tool;
using UnityEngine;

namespace Summer
{


    public class Testlambda : MonoBehaviour
    {
        public bool flag;
        Dictionary<int, int> table = new Dictionary<int, int>();
        public int count;

        public Action<int, int> p_call;

        public void CallVariable(int k, int v)
        {
            count = k + v;

        }

        void Start()
        {
            p_call = CallVariable;
            table.Add(1, 1);
            table.Add(2, 2);
            table.Add(3, 3);
            table.Add(4, 4);
            table.Add(5, 5);
            count = 0;
        }
        void Update()
        {
            if (!flag) return;
            UnityEngine.Profiling.Profiler.BeginSample("AnonymousWithoutParam");  // 未使用外部变量的匿名函数
            AnonymousVariable();
            UnityEngine.Profiling.Profiler.EndSample();
            UnityEngine.Profiling.Profiler.BeginSample("FunctionWithoutVariable"); // 未使用外部变量的成员函数
            FunctionWithoutVariable();
            UnityEngine.Profiling.Profiler.EndSample();
            UnityEngine.Profiling.Profiler.BeginSample("AnonymousParam"); // 使用外部变量的匿名函数
            AnonymousWithoutVariable();
            UnityEngine.Profiling.Profiler.EndSample();
            UnityEngine.Profiling.Profiler.BeginSample("FunctionVariable"); // 使用外部变量的成员函数
            FunctionVariable();
            UnityEngine.Profiling.Profiler.EndSample();
        }

        void AnonymousWithoutVariable()
        {
            table.Foreach((k, v) =>
            {
                int c = 0;
                c = k + v;
            });
        }

        // 未使用外部变量的成员函数
        void FunctionWithoutVariable()
        {
            table.Foreach(p_call);
        }

        // 使用外部变量的匿名函数 AnonymousParam
        void AnonymousVariable()
        {
            table.Foreach((k, v) =>
            {
                count = k + v;
            });
        }

        // 使用外部变量的成员函数
        void FunctionVariable()
        {
            table.Foreach(AddtVariable);
        }

        void AddtVariable(int k, int v)
        {
            count = k + v;
        }
    }
}