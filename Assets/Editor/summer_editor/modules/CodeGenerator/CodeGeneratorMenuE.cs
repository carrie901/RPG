

using System;
using UnityEditor;
using Summer;
namespace SummerEditor
{
    public class CodeGeneratorMenuE
    {
        [MenuItem("Tools/Csv工具/1.生成cs代码", false, 1)]
        public static bool CreateCode()
        {
            try
            {
                CodeGenerator.CreateCode();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [MenuItem("Tools/Csv工具/2.生成二进制资源", false, 2)]
        public static void WriteByte()
        {
            StaticCnf.Clear();
            ConfigManager.ReadLocalConfig();
            ConfigManager.WriteByteConfig();
        }

        [MenuItem("Tools/Csv工具/3.检验二进制", false, 3)]
        public static void ReadByte()
        {
            CodeGenerator.ReadByte();
        }

        //[MenuItem("Tools/Csv工具//4.WriteLocal", false, 4)]
        public static void WriteLocal()
        {
            CodeGenerator.WriteLocal();
        }
    }
}
