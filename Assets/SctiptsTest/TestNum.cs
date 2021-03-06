﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TestNum : MonoBehaviour
{
    public StringBuilder sb = new StringBuilder();
    // Use this for initialization
    void Start()
    {
        int input_num = 11;
        int count = (input_num / 2) + 1;
        for (int i = 0; i < input_num; i++)
        {
            if (i < count)
            {
                //Debug.Log(2 * i + 1);
                sb.AppendLine(Print01(2 * i + 1, input_num));
            }
            else
            {

                //Debug.Log(input_num - (i - count + 1) * 2);
                sb.AppendLine(Print01((input_num - i) * 2 + 1/*input_num - (i - count + 1) * 2*/, input_num));
            }
        }
        Debug.Log(sb.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        Cal1();
    }

    public string Print01(int num, int input_num)
    {
        string sb = string.Empty;
        int count = (input_num - num) / 2;
        for (int i = 0; i < count; i++)
        {
            sb += " ";
        }

        for (int i = 0; i < num; i++)
            sb += "*";
        //Debug.Log(sb);
        return sb;
    }

    public bool _flag1 = false;

    public float _curr;
    public float _end;
    public float _yearAdd;

    public void Cal1()
    {
        if (!_flag1) return;
        _flag1 = false;

        int i = 0;
        while (_curr < _end)
        {
            _curr = _curr * (1 + _yearAdd);
            i++;
        }

        Debug.Log(i);
    }


}
