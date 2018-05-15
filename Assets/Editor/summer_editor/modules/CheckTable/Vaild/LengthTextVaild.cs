using UnityEngine;
using System.Collections.Generic;
using System.Text;

//=============================================================================
/// Author : mashao
/// CreateTime : 2018-2-1 14:54:33
/// FileName : LengthTextVaild.cs
//=============================================================================

namespace SummerEditor
{
    // 长于一定的长度
    public class LengthTextVaild : I_TableVaild
    {
        public int length;                                      // 长度
        public string CheckVaild(List<string> infos)
        {
            StringBuilder sb = new StringBuilder();
            int info_length = infos.Count;
            for (int i = 0; i < info_length; i++)
            {
                string[] contents = infos[i].Split('$');
                if (contents.Length != length)
                {
                    sb.AppendLine(string.Format(TableVaildConst.length_error, infos[i]));
                    Debug.LogError("长度有问题:" + infos[i]);
                }
            }

            return sb.ToString();
        }

        public void SetVaildData(string[] datas)
        {
            length = int.Parse(datas[1]);
        }
    }
}
