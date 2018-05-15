using System;
using UnityEngine;
using System.Collections.Generic;

//=============================================================================
// Author : moony
// CreateTime : 2017-12-15 11:41:1
// FileName : TableVaild.cs
//=============================================================================
namespace SummerEditor
{
    /// <summary>
    /// 数据校验工场，对外提供数据校验类
    /// </summary>
    public class TableVaildFactory
    {
        public const string VAILD_1 = "exist_";                 // exist_
        public const string LENGTH = "length_";                 // length_
        public const string SIZE = "size_";                     // size_

        public static Dictionary<string, Type> vaild_map = new Dictionary<string, Type>()
        {
            { VAILD_1,typeof(ExistTextVaild)},
            { LENGTH,typeof(SizeTextVaild)},
            { SIZE,typeof(LengthTextVaild)}
        };

        // text=检测的公式 exist_表格_字段
        public static I_TableVaild Create(string text)
        {
            string[] infos = text.Split('_');

            string type = infos[0];
            if (!vaild_map.ContainsKey(type))
            {
                Debug.LogErrorFormat("无效的规则:[{0}]", text);
                return null;
            }

            I_TableVaild vaild = Activator.CreateInstance(vaild_map[type]) as I_TableVaild;
            if (vaild == null) return null;
            vaild.SetVaildData(infos);
            return vaild;
        }
    }
}
