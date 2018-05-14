using System.Text;
using Summer;

//=============================================================================
// Author : mashao
// CreateTime : 2018-2-2 14:47:35
// FileName : EProperty.cs
//=============================================================================

namespace SummerEditor
{
    /// <summary>
    /// 属性定义只支持Get
    /// </summary>
    public class EProperty
    {
        public string comment;                                          // 描述
        public string prop_type;                                        // 属性名字
        public string prop_name;                                        // 变量名
        public string value;

        public EProperty(string comment, string prop_type, string prop_name)
        {
            this.comment = comment;
            this.prop_type = prop_type;
            this.prop_name = prop_name;

        }

        public string ToDes(string tab)
        {
            StringBuilder sb = new StringBuilder();

            CodeGeneratorHelper.AppendComment(tab, sb, comment);
            sb.AppendLine(tab + string.Format("public {0} {1};", prop_type, prop_name));
            return sb.ToString();
        }

        public string ToLocalRead(string tab, string data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tab + tab + tab + CodeGeneratorHelper.LocalRead(prop_type, prop_name, data));
            return sb.ToString();
        }

        public string ToLocalWrite(string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tab + tab + tab + tab + CodeGeneratorHelper.LocalWrite(prop_type, prop_name));
            return sb.ToString();
        }

        public string ToByteRead(string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tab + tab + tab + CodeGeneratorHelper.ByteRead(prop_type, prop_name));
            return sb.ToString();
        }

        public string ToByteWrite(string tab)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tab + tab + tab + CodeGeneratorHelper.ByteWtrite(prop_type, prop_name));
            return sb.ToString();
        }
    }
}
