

using UnityEditor;

namespace SummerEditor
{
    public class CodeGeneratorMenuE
    {
        [MenuItem("Tools/Csv工具/1.CreateCode", false, 1)]
        public static void CreateCode()
        {
            CodeGenerator.CreateCode();
        }

        [MenuItem("Tools/Csv工具/2.WriteByte", false, 2)]
        public static void WriteByte()
        {
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
