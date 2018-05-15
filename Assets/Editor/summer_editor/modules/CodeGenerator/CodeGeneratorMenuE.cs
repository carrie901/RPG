

using UnityEditor;

namespace SummerEditor
{
    public class CodeGeneratorMenuE
    {
        [MenuItem("Tools/Csv工具/1.生成cs代码", false, 1)]
        public static void CreateCode()
        {
            CodeGenerator.CreateCode();
        }

        [MenuItem("Tools/Csv工具/2.生成二进制资源", false, 2)]
        public static void WriteByte()
        {
            CodeGenerator.WriteByte();
            //ConfigManager.Instance.ReadLocalConfig();
            //ConfigManager.Instance.WriteByteConfig();
        }

        [MenuItem("Tools/Csv工具/3.ReadByte", false, 3)]
        public static void ReadByte()
        {
            CodeGenerator.ReadByte();
        }

        [MenuItem("Tools/Csv工具//4.WriteLocal", false, 4)]
        public static void WriteLocal()
        {
            CodeGenerator.WriteLocal();
        }
    }
}
