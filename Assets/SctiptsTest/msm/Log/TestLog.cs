using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLog : MonoBehaviour
{

    public string a = "";
    public bool flag = false;
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            a += i.ToString();
        }
        a = string.Intern(a);
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            Print(1 + "12345678989123424324234324123423412343241234567898912342432423432412342341234324123456789891234243242343241234234123432412345678989123424324234324123423412343241234567898912342432423432412342341234324123456789891234243242343241234234123432412345678989123424324234324123423412343241234567898912342432423432412342341234324123456789891234243242343241234234123432412345678989123424324234324123423412343241234567898912342432423432412342341234324");
        }
    }

    public void Print(string ins)
    {
        //string.Intern(ins);
        //Debug.Log(ins);
    }
}
