using System.Collections.Generic;
using System.IO;
using System.Text;
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
    }
}
