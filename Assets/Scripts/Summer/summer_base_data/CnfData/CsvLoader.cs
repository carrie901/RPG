using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// CSV读取工具
    /// </summary>
    public class CsvLoader
    {
        public const char CVS_SPLIT = '&';// '|';       // 默认分割符号
        public const string STRING_EMPTY = "";
        public const int MIN_LINE = 4;                  // 最小行数
        public static string _csvFileRoot = Application.dataPath + "\\..\\Data\\Tables\\";//"E:\\work_three\\trunk\\three_config\\tables\\";

        #region 二进制加载

        /// <summary>
        /// 二进制读取文本资源
        /// </summary>
        public static Dictionary<int, T> LoadBinary<T>(string fileName) where T : BaseCsv, new()
        {
            byte[] bytes = _load_cvs_dat(fileName);

            MemoryStream ms = new MemoryStream(bytes);
            BinaryReader br = new BinaryReader(ms);

            // 1.读取二进制内容
            int length = br.ReadInt32();

            Dictionary<int, T> tMap = new Dictionary<int, T>(length);
            for (int i = 0; i < length; i++)
            {
                // 2.生成结构类
                T csv = new T();
                // 3.初始化属性
                csv.InitByReader(br);
                // 4.添加到集合
                tMap.Add(csv.GetId(), csv);
            }
            br.Close();
            ms.Close();
            ms.Dispose();
            return tMap;
        }

        public static void WriteBinary<T>(Dictionary<int, T> data, BinaryWriter bw) where T : BaseCsv
        {
            bw.Write(data.Count);
            foreach (var info in data)
            {
                info.Value.InitByWriter(bw);
            }
        }

        #endregion

        #region 直接加载cvs文件 应用于本地
        public static Dictionary<int, T> LoadFile<T>(string fileName) where T : BaseCsv, new()
        {
            // 1.读取文本内容
            List<string[]> contents = _load_csv_file(fileName);
            int length = contents.Count;
            Dictionary<int, T> tMap = new Dictionary<int, T>(length);

            for (int i = 0; i < length; i++)
            {
                // 2.生成结构类
                T csv = new T();
                Type type = csv.GetType();
                // 3.得到结构类的属性
                FieldInfo[] files = type.GetFields();
                // 4.初始化属性
                for (int j = 0; j < files.Length; j++)
                {
                    _field_set_value(contents[i][j], files[j], csv);
                }
                // 5.添加到集合
                tMap.Add(csv.GetId(), csv);
            }
            return tMap;
        }

        // 读取CSV文件
        public static List<string[]> _load_csv_file(string fileName)
        {
            // TODO 需要去掉Cnf同时又要添加csv 这段代码的读取是有上下门的潜规则在 
            fileName = _csvFileRoot + fileName;
            fileName = fileName.Replace("Cnf", string.Empty);
            string[] fileData = File.ReadAllLines(fileName + ".csv");
            List<string[]> result = new List<string[]>(fileData.Length);

            if (fileData.Length <= MIN_LINE)
            {
                return result;
            }



            for (int i = MIN_LINE; i < fileData.Length; i++)
            {
                string[] line = fileData[i].Split(',');
                result.Add(line);
            }

            return result;
        }

        public static byte[] _load_cvs_dat(string fileName)
        {
            byte[] bytes = ResManager.instance.LoadByte(fileName, E_GameResType.TEXT_ASSET);
            return bytes;
        }

        // 给属性赋值
        public static void _field_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            string fieldName = fieldInfo.FieldType.Name;
            switch (fieldName)
            {
                case "Int32":
                    _internal_int_set_value(value, fieldInfo, csv);
                    break;
                case "Int32[]":
                    _internal_ints_set_value(value, fieldInfo, csv);
                    break;
                case "String":
                    _internal_string_set_value(value, fieldInfo, csv);
                    break;
                case "String[]":
                    _internal_strings_set_value(value, fieldInfo, csv);
                    break;
                case "Boolean":
                    _internal_bool_set_value(value, fieldInfo, csv);
                    break;
                case "Boolean[]":
                    _internal_bools_set_value(value, fieldInfo, csv);
                    break;
                case "Single":
                    _internal_float_set_value(value, fieldInfo, csv);
                    break;
                case "Single[]":
                    _internal_floats_set_value(value, fieldInfo, csv);
                    break;
                default:
                    Debug.LogError("csvloader找不到对应的类型:" + fieldName + "数值:" + value);
                    break;
            }
        }

        #region internal set value

        public static void _internal_int_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    fieldInfo.SetValue(csv, 0);
                else
                    fieldInfo.SetValue(csv, Int32.Parse(value));
            }
            catch (Exception e)
            {
                LogManager.Error("[{0}],[{1}]", value, e.Message);
            }

        }

        public static void _internal_ints_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            int[] intResult;

            if (!string.IsNullOrEmpty(value))
            {
                string[] strArr = value.Split(CVS_SPLIT);
                intResult = new int[strArr.Length];
                for (int i = 0; i < strArr.Length; i++)
                    intResult[i] = Int32.Parse(strArr[i]);
            }
            else
                intResult = new int[0];

            fieldInfo.SetValue(csv, intResult);
        }

        public static void _internal_string_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            if (string.IsNullOrEmpty(value))
                fieldInfo.SetValue(csv, "");
            else
                fieldInfo.SetValue(csv, value);
        }

        public static void _internal_strings_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            string[] strResult;
            if (string.IsNullOrEmpty(value))
                strResult = new string[0];
            else
                strResult = value.Split(CVS_SPLIT);

            fieldInfo.SetValue(csv, strResult);
        }

        public static void _internal_float_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            if (string.IsNullOrEmpty(value))
                fieldInfo.SetValue(csv, 0);
            else
                fieldInfo.SetValue(csv, float.Parse(value));
        }

        public static void _internal_floats_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            float[] floatResult;

            if (!string.IsNullOrEmpty(value))
            {
                string[] strArr = value.Split(CVS_SPLIT);
                floatResult = new float[strArr.Length];
                for (int i = 0; i < strArr.Length; i++)
                    floatResult[i] = float.Parse(strArr[i]);
            }
            else
                floatResult = new float[0];

            fieldInfo.SetValue(csv, floatResult);
        }

        public static void _internal_bool_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            if (string.IsNullOrEmpty(value))
                fieldInfo.SetValue(csv, false);
            else
                fieldInfo.SetValue(csv, value == "1");
        }

        public static void _internal_bools_set_value(string value, FieldInfo fieldInfo, BaseCsv csv)
        {
            bool[] boolResult;

            if (!string.IsNullOrEmpty(value))
            {
                string[] strArr = value.Split(CVS_SPLIT);
                boolResult = new bool[strArr.Length];
                for (int i = 0; i < strArr.Length; i++)
                    boolResult[i] = bool.Parse(strArr[i]);
            }
            else
                boolResult = new bool[0];

            fieldInfo.SetValue(csv, boolResult);
        }

        #endregion

        #endregion
    }
}
