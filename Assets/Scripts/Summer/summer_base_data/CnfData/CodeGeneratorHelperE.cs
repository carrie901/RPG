using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Summer;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-2 19:43:54
// FileName : CodeGeneratorHelperE.cs
//=============================================================================

namespace Summer
{
    public class CodeGeneratorHelperE
    {

        public static Dictionary<string, string[]> data_map = new Dictionary<string, string[]>()
        {
            {"int", new[] { "ToInt","ReadInt","WriteInt","ToOutInt"}},
            {"float", new [] {"ToFloat","ReadFloat","WriteFloat","ToOutFloat"}},
            {"string", new [] { "ToStr", "ReadString","WriteString","ToOutStr"}},
            {"bool", new [] {"ToBool","ReadBool","WriteBool","ToOutBool"}},
            {"int[]", new [] {"ToInts","ReadIntS","WriteIntS","ToOutInts"}},
            {"float[]", new [] {"ToFloats","ReadFloatS","WriteFloatS","ToOutFloats"}},
            {"string[]", new [] {"ToStrs","ReadStringS","WriteStringS","ToOutStrs"}},
            {"bool[]", new [] {"ToBools","ReadBoolS","WriteBoolS","ToOutBools"}},
        };

        // 这一块可以做一点改变 读取指定目录 分成File和Resouces目录
        public static Dictionary<string, BaseCsvInfo> LoadFileContent()
        {
            Dictionary<string, BaseCsvInfo> csv_infos = new Dictionary<string, BaseCsvInfo>();
            string[] csvs_path = Directory.GetFiles(CodeGeneratorConstE.csv_path);
            // 2.依次读取File信息转成Txt
            int length = csvs_path.Length;
            for (int i = 0; i < length; i++)
            {
                // 忽略文件
                string with_out_extension_name = Path.GetFileNameWithoutExtension(csvs_path[i]);
                if (CodeGeneratorConstE.ingore_file.Contains(with_out_extension_name) || string.IsNullOrEmpty(with_out_extension_name)) continue;

                BaseCsvInfo csv_info = new BaseCsvInfo(csvs_path[i]);
                csv_infos.Add(csv_info.class_name, csv_info);
            }
            return csv_infos;
        }

        // 名字大写
        public static string NormalizeName(string file_name)
        {
            string[] name_list = file_name.Split('_');
            string name = string.Empty;//name_list[0].Substring(0, 1).ToUpper()+ name_list[0].Substring(1);
            int length = name_list.Length;

            for (int i = 0; i < length; i++)
            {
                name += name_list[i].Substring(0, 1).ToUpper() + name_list[i].Substring(1);
            }
            return name;
        }

        #region 注释Format

        /// <summary>
        /// 添加注释
        /// </summary>
        public static void AppendComment(string tab, StringBuilder sb, string comment)
        {
            sb.AppendLine(tab + "/// <summary>");
            sb.AppendLine(tab + "/// " + comment);
            sb.AppendLine(tab + "/// </summary>");
        }

        #endregion

        #region 代码生成的string Format

        public static string LocalRead(string prop_type, string prop_name, string value)
        {
            StringBuilder sb = new StringBuilder();
            if (data_map.ContainsKey(prop_type))
            {
                sb.AppendFormat("{0} = ByteHelper.{1}({2});", prop_name, data_map[prop_type][0], value);
            }
            else
            {
                LogManager.Log("CodeGeneratorHelper Error.Prop_type:{0},prop_name:{1}", prop_type, prop_name);
            }
            return sb.ToString();
        }

        public static string LocalWrite(string prop_type, string prop_name)
        {
            StringBuilder sb = new StringBuilder();
            if (data_map.ContainsKey(prop_type))
            {
                sb.AppendFormat("ByteHelper.{0}({1})", data_map[prop_type][3], prop_name);
            }
            else
            {
                LogManager.Log("CodeGeneratorHelper Error.Prop_type:{0},prop_name:{1}", prop_type, prop_name);
            }
            return sb.ToString();
        }

        public static string ByteRead(string prop_type, string prop_name)
        {
            StringBuilder sb = new StringBuilder();
            if (data_map.ContainsKey(prop_type))
            {
                sb.AppendFormat("{0} = ByteHelper.{1}(br);", prop_name, data_map[prop_type][1]);
            }
            else
            {
                LogManager.Log("CodeGeneratorHelper Error.Prop_type:{0},prop_name:{1}", prop_type, prop_name);
            }
            return sb.ToString();
        }

        public static string ByteWtrite(string prop_type, string prop_name)
        {
            StringBuilder sb = new StringBuilder();
            if (data_map.ContainsKey(prop_type))
            {
                sb.AppendFormat("ByteHelper.{1}(bw,{0});", prop_name, data_map[prop_type][2]);
            }
            else
            {
                LogManager.Log("CodeGeneratorHelper Error.Prop_type:{0},prop_name:{1}", prop_type, prop_name);
            }
            return sb.ToString();
        }

        #endregion

        #region AppendLine

        public static void AppendLine(StringBuilder sb, int tab, string format, params object[] args)
        {
            string info = string.Format(format, args);
            for (int i = 0; i < tab; i++)
                sb.Append("\t");
            sb.AppendLine(info);
        }

        public static void AppendLine(StringBuilder sb, int tab, string format)
        {
            string info = format;
            for (int i = 0; i < tab; i++)
                sb.Append("\t");
            sb.AppendLine(info);
        }

        #endregion

        /*public static CodeTypeReference GetCodeType(ETypeDefine type)
        {
            CodeTypeReference ret_type = null;
            switch (type)
            {
                /* case ETypeDefine.Char:
                     ret_type = new CodeTypeReference(typeof(System.Char));
                     break;
                 case ETypeDefine.Double:
                     ret_type = new CodeTypeReference(typeof(System.Double));
                     break;
                 case ETypeDefine.Float:
                     ret_type = new CodeTypeReference(typeof(System.Decimal));
                     break;
                 case ETypeDefine.Int:
                     ret_type = new CodeTypeReference(typeof(System.Int32));
                     break;
                 case ETypeDefine.Short:
                     ret_type = new CodeTypeReference(typeof(System.Int16));
                     break;
                 case ETypeDefine.String:
                     ret_type = new CodeTypeReference(typeof(System.String));
                     break;#1#
            }
            return ret_type;
        }*/
    }
}
