using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

namespace Summer
{
    //=============================================================================
    /// Author : Msm
    /// CreateTime : 2017-8-3
    /// FileName : StringHelper.cs
    /// 关于一些String的处理
    /// 
    /// 1.使用序数比较（Ordinal）会大约快 10 倍
    /// 2.强烈建议不要使用接受正规表示法字符串作为参数的静态 Regex.Match 或 Regex.Replace 方法。这些方法都是当场编译正规表示法后用过即丢
    /// 3.要避免过长的文字解析时间成本，最好的方法就是在执行时不要有文字解析的操作。一般来说就是透过某些流程将文件数据先“烘焙”成二进制格式。 
    //=============================================================================
    public class StringHelper
    {

        /// <summary>
        /// 分割字符串
        /// </summary>
        public static string[] SplitString(string str_content, string str_split)
        {
            if (!str_content.Contains(str_split))
            {
                string[] tmp = { str_content };
                return tmp;
            }
            return Regex.Split(str_content, Regex.Escape(str_split), RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///  解析(1,1,1 或11.1,10,2)为vector3
        /// </summary>
        /// <param name="str_vector3"></param>
        /// <param name="split_str"></param>
        /// <returns></returns>
        public static Vector3 StringToVector3(string str_vector3, params char[] split_str)
        {
            Vector3 ret = Vector3.zero;
            if (!string.IsNullOrEmpty(str_vector3))
            {
                var str_arr = str_vector3.Split(split_str);
                if (str_arr.Length == 3)
                {
                    float.TryParse(str_arr[0].Trim(), out ret.x);
                    float.TryParse(str_arr[1].Trim(), out ret.y);
                    float.TryParse(str_arr[2].Trim(), out ret.z);
                }
                else
                {
                    LogManager.Error("str length not 3");
                }
            }
            else
            {
                LogManager.Error("str length not 3");
            }
            return ret;
        }

        /// <summary>
        /// 类比String.StartsWith 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CustomEndsWith(string a, string b)
        {
            int ap = a.Length - 1;
            int bp = b.Length - 1;

            while (ap >= 0 && bp >= 0 && a[ap] == b[bp])
            {
                ap--;
                bp--;
            }

            return (bp < 0 && a.Length >= b.Length) || (ap < 0 && b.Length >= a.Length);
        }

        /// <summary>
        /// 类比String.EndsWith 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool CustomStartsWith(string a, string b)
        {
            int a_len = a.Length;
            int b_len = b.Length;
            int ap = 0; int bp = 0;

            while (ap < a_len && bp < b_len && a[ap] == b[bp])
            {
                ap++;
                bp++;
            }

            return (bp == b_len && a_len >= b_len) || (ap == a_len && b_len >= a_len);
        }
    }

}
