//=============================================================================
// Author : mashao
// CreateTime : 2018-2-2 14:48:12
// FileName : EClassDefine.cs
//=============================================================================

using System.Collections.Generic;
using System.IO;
using System.Text;
using Summer;

namespace SummerEditor
{
    /// <summary>
    /// 类定义
    /// </summary>
    public class EClassDefine
    {
        public string file_path;                // 文件路径
        public string comment;                  // 注释
        public string class_name;               // 类名字

        public List<EProperty> properties       // 属性定义 
            = new List<EProperty>();

        public string other_info = string.Empty;

        // 通过信息形成文本
        public string ToDes()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.IO;");
            string tab = "\t";
            // 1.空间名
            sb.AppendLine(CodeGeneratorConstE.NAMESPACE);
            sb.AppendLine("{");
            // 2.注释
            CodeGeneratorHelperE.AppendComment(tab, sb, comment);

            // 3.类名
            sb.AppendLine(tab + "public class " + class_name+ " : BaseCsv");
            sb.AppendLine(tab + "{");
            // 4.属性
            int prop_length = properties.Count;
            for (int i = 0; i < prop_length; i++)
            {
                sb.AppendLine(properties[i].ToDes(tab + tab));
            }

            sb.AppendLine("");
            // 5.属性Local读取
            {
                sb.AppendLine(tab + tab + "//属性Local读取");
                sb.AppendLine(tab + tab + "public void ToLocalRead(List<string> contents)");
                sb.AppendLine(tab + tab + "{");
                for (int i = 0; i < prop_length; i++)
                {
                    sb.AppendLine(properties[i].ToLocalRead(tab, "contents[" + i + "]"));
                }
                sb.AppendLine(tab + tab + "}");
            }
            sb.AppendLine("");
            //6.属性Byte读取
            {
                sb.AppendLine(tab + tab + "//属性Byte读取");
                sb.AppendLine(tab + tab + "public void ToByteRead(BinaryReader br)");
                sb.AppendLine(tab + tab + "{");
                for (int i = 0; i < prop_length; i++)
                {
                    sb.AppendLine(properties[i].ToByteRead(tab));
                }
                sb.AppendLine(tab + tab + "}");
            }
            sb.AppendLine("");
            //7.属性Byte写入
            {
                sb.AppendLine(tab + tab + "//属性Byte写入");
                sb.AppendLine(tab + tab + "public void ToByteWrite(BinaryWriter bw)");
                sb.AppendLine(tab + tab + "{");
                for (int i = 0; i < prop_length; i++)
                {
                    sb.AppendLine(properties[i].ToByteWrite(tab));
                }
                sb.AppendLine(tab + tab + "}");
            }

            // ToLocalWrite
            {
                sb.AppendLine(tab + tab + "public string ToLocalWrite()");
                sb.AppendLine(tab + tab + "{");
                sb.Append(tab + tab + tab + "return ");
                for (int i = 0; i < prop_length; i++)
                {
                    if (i == 0)
                        sb.AppendLine(properties[i].ToLocalWrite("") + "+\", \"+");
                    else if (i == prop_length - 1)
                        sb.AppendLine(tab + tab + tab + tab + properties[i].ToLocalWrite("") + ";");
                    else
                        sb.AppendLine(properties[i].ToLocalWrite(tab) + "+\", \"+");
                }
                sb.AppendLine(tab + tab + "}");
            }
            sb.AppendLine(tab + "}");
            sb.AppendLine("}");
            sb.AppendLine(string.Empty);
            return sb.ToString();
        }

        public void AddProp(List<string> comment_name_list, List<string> prop_type_list, List<string> prop_name_list)
        {
            if (prop_name_list.Count != comment_name_list.Count || prop_name_list.Count != prop_type_list.Count)
                return;

            for (int i = 0; i < prop_name_list.Count; i++)
            {
                EProperty prop = new EProperty(comment_name_list[i], prop_type_list[i], prop_name_list[i]);
                properties.Add(prop);
            }
        }
    }
}
