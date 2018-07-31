using System;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;

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
        public const string SPLIT1 = "|";
        public static string[] split_huanhang = new string[] { "\r\n" };
        public static string[] split_douhao = new string[] { "," };

        #region Split

        public static string[] SplitString(string str_content)
        {
            return SplitString(str_content, SPLIT1);
        }

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

            return str_content.Split(new string[] { str_split }, StringSplitOptions.None);
            //return Regex.Split(str_content, Regex.Escape(str_split), RegexOptions.IgnoreCase);
        }

        #endregion

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

        // 粗暴的移除<color=#FF00ff></color>
        public static int LengthRemoveColor(string text)
        {
            text = text.Replace("</color>", "");
            string[] content = SplitString(text, "<color=#");
            int length = (content.Length - 1) * 15;
            int text_length = text.Length - length;
            if (text_length <= 0)
                text_length = 0;
            return text_length;
        }

        /// <summary>
        /// 对
        ///     ,,,
        ///     ,,,
        ///     ,,,
        /// 这样的csv文本进行解析
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static List<string[]> ParseData(string text)
        {
            List<string[]> result = new List<string[]>();
            string[] lines = text.ToStrs(StringHelper.split_huanhang);
            int length = lines.Length;

            for (int i = 0; i < length; i++)
            {
                string[] results = lines[i].ToStrs(StringHelper.split_douhao);
                if (results.Length <= 1)
                {
                    continue;
                }
                result.Add(results);
            }
            return result;
        }
    }

    public static class StringExtension
    {
        // TODO BUG 这里不能直接引用TextCsv.Loc
        public static string Loc(this string str)
        {
            LogManager.Error("当前不提供这样的功能");
            return string.Empty;
            //return TextCsv.Loc(str);
        }

        public static bool IsNullOrEmpty(this string self_str)
        {
            return string.IsNullOrEmpty(self_str);
        }

        #region Parse int/float/bool/string/int[]/float[]/bool[]/string[]/Vector3/Dictionary

        public static int ToInt(this string self_str)
        {
            int result = 0;
            int.TryParse(self_str, out result);
            return result;
        }

        public static float ToFloat(this string self_str)
        {
            float result = 0f;
            float.TryParse(self_str, out result);
            return result;
        }

        public static bool ToBool(this string self_str)
        {
            if (self_str == "1")
                return true;
            return false;
        }


        public static int[] ToInts(this string self_str, string str_plit = ",")
        {
            string[] result = self_str.Split(new string[] { str_plit }, StringSplitOptions.None);
            int length = result.Length;
            int[] value = new int[length];
            for (int i = 0; i < length; i++)
            {
                value[i] = result[i].ToInt();
            }

            return value;
        }

        public static float[] ToFloats(this string self_str, string str_plit = ",")
        {
            string[] result = self_str.Split(new[] { str_plit }, StringSplitOptions.None);
            int length = result.Length;
            float[] value = new float[length];
            for (int i = 0; i < length; i++)
            {
                value[i] = result[i].ToFloat();
            }

            return value;
        }

        public static bool[] ToBools(this string self_str, string str_plit = ",")
        {
            string[] result = self_str.Split(new[] { str_plit }, StringSplitOptions.None);
            int length = result.Length;
            bool[] value = new bool[length];
            for (int i = 0; i < length; i++)
            {
                value[i] = result[i].ToBool();
            }

            return value;
        }

        public static string[] ToStrs(this string self_str, string str_plit = ",")
        {
            string[] result = self_str.Split(new string[] { str_plit }, StringSplitOptions.None);
            return result;
        }

        public static string[] ToStrs(this string self_str, string[] str_plit)
        {
            string[] result = self_str.Split(str_plit, StringSplitOptions.None);
            return result;
        }


        public static Vector3 ToV3(this string self_str)
        {
            string[] result = self_str.Split(new[] { "," }, StringSplitOptions.None);
            if (result.Length != 3)
            {
                return Vector3.zero;
            }
            float x = result[0].ToInt();
            float y = result[0].ToInt();
            float z = result[0].ToInt();
            return new Vector3(x, y, z);
        }

        public static Dictionary<int, int> ToDic(this string self_str, string str_plit_key = "$", string str_split_value = "|")
        {
            string[] values = self_str.Split(str_split_value.ToCharArray(), StringSplitOptions.None);
            Dictionary<int, int> result = new Dictionary<int, int>();
            int length = values.Length;
            for (int i = 0; i < length; i++)
            {
                string[] keys = values[i].ToStrs(str_plit_key);
                if (keys.Length != 2) continue;
                int key = keys[0].ToInt();
                int value = keys[1].ToInt();
                if (!result.ContainsKey(key))
                    result.Add(key, value);
            }
            return result;
        }

        #endregion
    }

}
