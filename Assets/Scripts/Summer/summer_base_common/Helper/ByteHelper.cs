using System;
using System.Collections.Generic;
using System.IO;

namespace Summer
{
    /// <summary>
    /// 代码生成的辅助工具类，区别于CodeGeneratorHelper的地方在于一个在Ediator中一个属于外部
    /// </summary>
    public class ByteHelper
    {
        #region string BinaryReader

        public static int ReadInt(BinaryReader br)
        {
            return br.ReadInt32();
        }

        public static float ReadFloat(BinaryReader br)
        {
            return br.ReadSingle();
        }

        public static bool ReadBool(BinaryReader br)
        {
            return br.ReadBoolean();
        }

        public static string ReadString(BinaryReader br)
        {
            string result = br.ReadString();
            result = string.Intern(result);
            return result;
        }

        public static int[] ReadIntS(BinaryReader br)
        {
            int length = br.ReadInt32();
            int[] value = new int[length];
            for (int i = 0; i < length; i++)
                value[i] = br.ReadInt32();
            return value;
        }

        public static float[] ReadFloatS(BinaryReader br)
        {
            int length = br.ReadInt32();
            float[] value = new float[length];
            for (int i = 0; i < length; i++)
                value[i] = br.ReadSingle();
            return value;
        }

        public static bool[] ReadBoolS(BinaryReader br)
        {
            int length = br.ReadInt32();
            bool[] value = new bool[length];
            for (int i = 0; i < length; i++)
                value[i] = br.ReadBoolean();
            return value;
        }

        /// <summary>
        /// 强制性 只能用在cnf 静态数据表格中使用 
        /// </summary>
        /// <param name="br"></param>
        /// <returns></returns>
        public static string[] ReadStringS(BinaryReader br)
        {
            int length = br.ReadInt32();
            string[] value = new string[length];
            for (int i = 0; i < length; i++)
            {
                string result = br.ReadString();
                result = string.Intern(result);
                value[i] = result;
            }

            return value;
        }


        #endregion

        #region string BinaryReader

        public static void WriteInt(BinaryWriter bw, int value)
        {
            bw.Write(value);
        }

        public static void WriteBool(BinaryWriter bw, bool value)
        {
            bw.Write(value);
        }

        public static void WriteFloat(BinaryWriter bw, float value)
        {
            bw.Write(value);
        }

        public static void WriteString(BinaryWriter bw, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                bw.Write("");
            }
            else
            {
                bw.Write(value);
            }
        }

        public static void WriteIntS(BinaryWriter bw, int[] value)
        {
            int length = value.Length;
            WriteInt(bw, length);
            for (int i = 0; i < length; i++)
                bw.Write(value[i]);
        }

        public static void WriteBoolS(BinaryWriter bw, bool[] value)
        {
            int length = value.Length;
            WriteInt(bw, length);
            for (int i = 0; i < length; i++)
                bw.Write(value[i]);
        }

        public static void WriteFloatS(BinaryWriter bw, float[] value)
        {
            int length = value.Length;
            WriteInt(bw, length);
            for (int i = 0; i < length; i++)
                bw.Write(value[i]);
        }

        public static void WriteStringS(BinaryWriter bw, string[] value)
        {
            int length = value.Length;
            WriteInt(bw, length);
            for (int i = 0; i < length; i++)
            {
                WriteString(bw, value[i]);
            }
        }

        #endregion

        #region Parse int/float/bool/string/int[]/float[]/bool[]/string[]/Vector3/Dictionary

        public static int ToInt(string self_str)
        {
            int result;
            int.TryParse(self_str, out result);
            return result;
        }

        public static float ToFloat(string self_str)
        {
            float result;
            float.TryParse(self_str, out result);
            return result;
        }

        public static bool ToBool(string self_str)
        {
            if (self_str == "1")
                return true;
            return false;
        }

        public static string ToStr(string self_str)
        {
            if(string.IsNullOrEmpty(self_str))return String.Empty;
            self_str = string.Intern(self_str);
            return self_str;
        }

        public static int[] ToInts(string self_str)
        {
            string[] result = self_str.ToStrs(CnfConst.SPLIT_LIST);
            int length = result.Length;
            int[] value = new int[length];
            for (int i = 0; i < length; i++)
            {
                value[i] = result[i].ToInt();
            }

            return value;
        }

        public static float[] ToFloats(string self_str)
        {
            string[] result = self_str.ToStrs(CnfConst.SPLIT_LIST);
            int length = result.Length;
            float[] value = new float[length];
            for (int i = 0; i < length; i++)
            {
                value[i] = result[i].ToFloat();
            }

            return value;
        }

        public static bool[] ToBools(string self_str)
        {
            string[] result = self_str.ToStrs(CnfConst.SPLIT_LIST);
            int length = result.Length;
            bool[] value = new bool[length];
            for (int i = 0; i < length; i++)
            {
                value[i] = result[i].ToBool();
            }

            return value;
        }

        public static string[] ToStrs(string self_str)
        {
            if (string.IsNullOrEmpty(self_str)) return new string[] { };
            string[] result = self_str.Split(new[] { CnfConst.SPLIT_LIST }, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }

        public static Dictionary<int, int> ToDic(string self_str, string str_plit_key = "$", string str_split_value = "|")
        {
            string[] values = self_str.ToStrs(str_split_value);
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

        #region (int float bool) to string

        public static int ToOutInt(int self_str) { return self_str; }

        public static float ToOutFloat(float self_str) { return self_str; }

        public static string ToOutBool(bool self_str)
        {
            if (self_str)
                return "1";
            return "0";
        }

        public static string ToOutStr(string self_str) { return self_str; }

        public static string ToOutInts(int[] self_str)
        {
            string result = string.Empty;
            int length = self_str.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                    result += ToOutInt(self_str[i]);
                else
                    result += ToOutInt(self_str[i]) + CnfConst.SPLIT_LIST;
            }
            return result;
        }

        public static string ToOutFloats(float[] self_str)
        {
            string result = string.Empty;
            int length = self_str.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                    result += ToOutFloat(self_str[i]);
                else
                    result += ToOutFloat(self_str[i]) + CnfConst.SPLIT_LIST;
            }
            return result;
        }

        public static string ToOutBools(bool[] self_str)
        {
            string result = string.Empty;
            int length = self_str.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                    result += ToOutBool(self_str[i]);
                else
                    result += ToOutBool(self_str[i]) + CnfConst.SPLIT_LIST;
            }
            return result;
        }

        public static string ToOutStrs(string[] self_str)
        {
            string result = string.Empty;
            int length = self_str.Length;
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                    result += ToOutStr(self_str[i]);
                else
                    result += ToOutStr(self_str[i]) + CnfConst.SPLIT_LIST;
            }
            return result;
        }

        public static Dictionary<int, int> ToOutDic(string self_str, string str_plit_key = "$", string str_split_value = "|")
        {
            string[] values = self_str.ToStrs(str_split_value);
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
