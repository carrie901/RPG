using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Summer;
using UnityEngine;

/// <summary>
/// CSV读取工具
/// </summary>
public class CsvLoader
{
    public const char CVS_SPLIT = '$';// '|';       // 默认分割符号
    public const string STRING_EMPTY = "";
    public const int MIN_LINE = 3;                  // 最小行数
    public static string csv_file_root = "E:\\work_three\\trunk\\three_config\\tables\\";
    #region 二进制加载

    public static Dictionary<int, T> LoadBinary<T>(string file_name) where T : BaseCsv, new()
    {
        byte[] bytes = _load_cvs_dat(file_name);

        MemoryStream ms = new MemoryStream(bytes);
        BinaryReader br = new BinaryReader(ms);

        // 1.读取二进制内容
        int length = br.ReadInt32();

        Dictionary<int, T> t_map = new Dictionary<int, T>(length);
        for (int i = 0; i < length; i++)
        {
            // 2.生成结构类
            T csv = new T();
            // 3.初始化属性
            csv.InitByReader(br);
            // 4.添加到集合
            t_map.Add(csv.GetId(), csv);
        }
        br.Close();
        ms.Close();
        ms.Dispose();
        return t_map;
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
    public static Dictionary<int, T> LoadFile<T>(string file_name) where T : BaseCsv, new()
    {
        // 1.读取文本内容
        List<string[]> contents = _load_csv_file(file_name);
        int length = contents.Count;
        Dictionary<int, T> t_map = new Dictionary<int, T>(length);

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
            t_map.Add(csv.GetId(), csv);
        }
        return t_map;
    }

    // 读取CSV文件
    public static List<string[]> _load_csv_file(string file_name)
    {
        // TODO 需要去掉Cnf同时又要添加csv 这段代码的读取是有上下门的潜规则在 
        file_name = csv_file_root + file_name;
        file_name = file_name.Replace("Cnf", string.Empty);
        string[] file_data = File.ReadAllLines(file_name + ".csv");
        List<string[]> result = new List<string[]>(file_data.Length);

        if (file_data.Length <= MIN_LINE)
        {
            return result;
        }



        for (int i = MIN_LINE; i < file_data.Length; i++)
        {
            string[] line = file_data[i].Split(',');
            result.Add(line);
        }

        return result;
    }

    public static byte[] _load_cvs_dat(string file_name)
    {
        byte[] bytes = ResManager.instance.LoadByte(file_name, E_GameResType.cvs_byte);

        return bytes;
    }

    // 给属性赋值
    public static void _field_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        string field_name = field_info.FieldType.Name;
        switch (field_name)
        {
            case "Int32":
                _internal_int_set_value(value, field_info, csv);
                break;
            case "Int32[]":
                _internal_ints_set_value(value, field_info, csv);
                break;
            case "String":
                _internal_string_set_value(value, field_info, csv);
                break;
            case "String[]":
                _internal_strings_set_value(value, field_info, csv);
                break;
            case "Boolean":
                _internal_bool_set_value(value, field_info, csv);
                break;
            case "Boolean[]":
                _internal_bools_set_value(value, field_info, csv);
                break;
            case "Single":
                _internal_float_set_value(value, field_info, csv);
                break;
            case "Single[]":
                _internal_floats_set_value(value, field_info, csv);
                break;
            default:
                Debug.LogError("csvloader找不到对应的类型:" + field_name + "数值:" + value);
                break;
        }
    }

    #region internal set value

    public static void _internal_int_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        if (string.IsNullOrEmpty(value))
            field_info.SetValue(csv, 0);
        else
            field_info.SetValue(csv, Int32.Parse(value));
    }

    public static void _internal_ints_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        int[] int_result;

        if (!string.IsNullOrEmpty(value))
        {
            string[] str_arr = value.Split(CVS_SPLIT);
            int_result = new int[str_arr.Length];
            for (int i = 0; i < str_arr.Length; i++)
                int_result[i] = Int32.Parse(str_arr[i]);
        }
        else
            int_result = new int[0];

        field_info.SetValue(csv, int_result);
    }

    public static void _internal_string_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        if (string.IsNullOrEmpty(value))
            field_info.SetValue(csv, "");
        else
            field_info.SetValue(csv, value);
    }

    public static void _internal_strings_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        string[] str_result;
        if (string.IsNullOrEmpty(value))
            str_result = new string[0];
        else
            str_result = value.Split(CVS_SPLIT);

        field_info.SetValue(csv, str_result);
    }

    public static void _internal_float_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        if (string.IsNullOrEmpty(value))
            field_info.SetValue(csv, 0);
        else
            field_info.SetValue(csv, float.Parse(value));
    }

    public static void _internal_floats_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        float[] float_result;

        if (!string.IsNullOrEmpty(value))
        {
            string[] str_arr = value.Split(CVS_SPLIT);
            float_result = new float[str_arr.Length];
            for (int i = 0; i < str_arr.Length; i++)
                float_result[i] = float.Parse(str_arr[i]);
        }
        else
            float_result = new float[0];

        field_info.SetValue(csv, float_result);
    }

    public static void _internal_bool_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        if (string.IsNullOrEmpty(value))
            field_info.SetValue(csv, false);
        else
            field_info.SetValue(csv, value == "1" ? true : false);
    }

    public static void _internal_bools_set_value(string value, FieldInfo field_info, BaseCsv csv)
    {
        bool[] bool_result;

        if (!string.IsNullOrEmpty(value))
        {
            string[] str_arr = value.Split(CVS_SPLIT);
            bool_result = new bool[str_arr.Length];
            for (int i = 0; i < str_arr.Length; i++)
                bool_result[i] = bool.Parse(str_arr[i]);
        }
        else
            bool_result = new bool[0];

        field_info.SetValue(csv, bool_result);
    }

    #endregion

    #endregion
}
