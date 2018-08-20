using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;

public class TableViewColDesc
{
    public string property_name;
    public string title_text;

    public TextAnchor Alignment;
    public string format;
    public float width_in_percent;

    public MemberInfo mem_info;

    public string FormatObject(object obj)
    {
        return PAEditorUtil.MemberToString(obj, mem_info, format);
    }

    public int Compare(object o1, object o2)
    {
        object fv1 = PAEditorUtil.MemberValue(o1, mem_info);
        object fv2 = PAEditorUtil.MemberValue(o2, mem_info);

        IComparable fc1 = fv1 as IComparable;
        IComparable fc2 = fv2 as IComparable;
        if (fc1 == null || fc2 == null)
        {
            return fv1.ToString().CompareTo(fv2.ToString());
        }

        return fc1.CompareTo(fc2);
    }
}
