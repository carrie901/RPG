using System.IO;
using System.Collections.Generic;
using System.Text;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 代码生成工具 CSV TO CS
    /// </summary>
    public class CodeGenerator
    {
        private static StringBuilder sb = new StringBuilder();

        /// <summary>
        /// 根据.csv创建.cs文件
        /// </summary>
        public static void CreateCode()
        {
            // 1.读取指定目录下的所有文件
            Dictionary<string, BaseCsvInfo> csv_infos = CnfHelper.LoadFileContent();
            // 2.创建数据结构类
            List<EClassDefine> class_map = CreateClass(csv_infos);
            // 确认文件夹已经创建
            FileHelper.CreateDirectory(CnfConst.cnf_path);
            // 3.导出
            int length = class_map.Count;
            for (int i = 0; i < length; i++)
                File.WriteAllText(class_map[i].file_path, class_map[i].ToDes());

            string config_text = CreateClassManager(csv_infos);
            File.WriteAllText(CodeGeneratorConstE.config_cs_path, config_text);
            //EditorUtility.DisplayDialog("Csv转成Cs", "运行结束，请查看结果", "Ok");
        }

        /// <summary>
        /// 根据.csv文件生成对应的二进制文件
        /// </summary>
        public static void WriteByte()
        {
            EditorUtility.DisplayDialog("生成二进制文件", "查看结果", "OK");
        }

        //[MenuItem("AutoCsv/3.ReadByte", false, 3)]
        public static void ReadByte()
        {
            ConfigManager.ReadByteConfig();
            EditorUtility.DisplayDialog("检验二进制", "查看结果", "OK");
        }

        //[MenuItem("AutoCsv/4.WriteLocal", false, 4)]
        public static void WriteLocal()
        {
            //ConfigManager.Instance.ReadLocalConfig();
            /*ConfigManager.Instance.ReadByteConfig();
            StringBuilder string_builder = new StringBuilder();
            foreach (var v in ConfigManager.Instance.avgcnf)
            {
                string_builder.AppendLine(v.Value.ToLocalWrite());
            }
            FileHelper.WriteTxtByFile(CodeGeneratorConstE.csv_path + "1.csv", string_builder.ToString());*/
        }

        // 创建数据结构类
        public static List<EClassDefine> CreateClass(Dictionary<string, BaseCsvInfo> csv_infos)
        {
            List<EClassDefine> class_map = new List<EClassDefine>();
            List<BaseCsvInfo> csv_list = new List<BaseCsvInfo>(csv_infos.Values);
            int length = csv_list.Count;
            for (int i = 0; i < length; i++)
            {
                BaseCsvInfo csv_info = csv_list[i];
                EClassDefine class_info = new EClassDefine();
                // 4.根据内容生成data
                class_info.AddProp(csv_info._propDes, csv_info._propType, csv_info._propName);
                class_info.file_path = csv_info._classPath;
                class_info.class_name = csv_info._className;
                class_info.comment = string.Empty;
                class_map.Add(class_info);
            }

            return class_map;
        }

        public static string CreateClassManager(Dictionary<string, BaseCsvInfo> csv_map)
        {
            List<BaseCsvInfo> csv_infos = new List<BaseCsvInfo>(csv_map.Values);
            sb.Remove(0, sb.Length);
            int length;

            // using
            CodeGeneratorHelperE.AppendLine(sb, 0, "using System;");
            CodeGeneratorHelperE.AppendLine(sb, 0, "using System.Collections.Generic;");
            CodeGeneratorHelperE.AppendLine(sb, 0, "using System.IO;");
            CodeGeneratorHelperE.AppendLine(sb, 0, "using Summer;");
            // 类名
            CodeGeneratorHelperE.AppendLine(sb, 0, "public class " + CodeGeneratorConstE.config_manager);
            CodeGeneratorHelperE.AppendLine(sb, 0, "{");
            //CodeGeneratorHelperE.AppendLine(sb, 1, "public static {0} Instance = new {0}();", CodeGeneratorConstE.config_manager);

            {
                // 属性
                /*length = csv_infos.Count;
                for (int i = 0; i < length; i++)
                {
                    CodeGeneratorHelperE.AppendLine(sb, 1, "public Dictionary<int, {0}> {1} = new Dictionary<int, {0}>();", csv_infos[i].class_name, csv_infos[i].class_name.ToLower());
                }*/
            }

            CodeGeneratorHelperE.AppendLine(sb, 0, string.Empty);
            // void ReadLocalConfig
            {
                CodeGeneratorHelperE.AppendLine(sb, 1, "public static void ReadLocalConfig()");
                CodeGeneratorHelperE.AppendLine(sb, 1, "{");
                CodeGeneratorHelperE.AppendLine(sb, 1, "StaticCnf.Clear();");
                CodeGeneratorHelperE.AppendLine(sb, 2, "int length = 0;");
                CodeGeneratorHelperE.AppendLine(sb, 2, "Dictionary<string, BaseCsvInfo> csv_infos = CnfHelper.LoadFileContent();");
                {
                    CodeGeneratorHelperE.AppendLine(sb, 2, "BaseCsvInfo info = null;");
                    length = csv_infos.Count;
                    for (int i = 0; i < length; i++)
                    {
                        CodeGeneratorHelperE.AppendLine(sb, 0, string.Empty);
                        string key = csv_infos[i]._className;
                        CodeGeneratorHelperE.AppendLine(sb, 2, "info = csv_infos[\"{0}\"];", key);
                        CodeGeneratorHelperE.AppendLine(sb, 2, "Dictionary<int, {0}> {1} = new Dictionary<int, {0}>();", key, key.ToLower());
                        //CodeGeneratorHelperE.AppendLine(sb, 2, "{0}.Clear();", key.ToLower());
                        CodeGeneratorHelperE.AppendLine(sb, 2, "length = info.datas.Count;");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "for (int i = 0; i < length; i++)");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "{");
                        CodeGeneratorHelperE.AppendLine(sb, 3, "{0} tmp = new {0}();", key);
                        CodeGeneratorHelperE.AppendLine(sb, 3, "tmp.ToLocalRead(info.datas[i]);");
                        CodeGeneratorHelperE.AppendLine(sb, 3, "{0}.Add(tmp.id, tmp); ", key.ToLower());
                        CodeGeneratorHelperE.AppendLine(sb, 2, "}");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "StaticCnf.Add({0});", key.ToLower());
                    }
                }
                CodeGeneratorHelperE.AppendLine(sb, 1, "}");
            }

            // void ReadByteConfig
            {
                CodeGeneratorHelperE.AppendLine(sb, 1, "public static void ReadByteConfig()");
                CodeGeneratorHelperE.AppendLine(sb, 1, "{");
                CodeGeneratorHelperE.AppendLine(sb, 1, "StaticCnf.Clear();");
                {
                    CodeGeneratorHelperE.AppendLine(sb, 2, "byte[] bytes = ResManager.instance.LoadByte(CnfConst.DATA_BYTE_NAME);");
                    CodeGeneratorHelperE.AppendLine(sb, 2, "MemoryStream ms = new MemoryStream(bytes);");
                    CodeGeneratorHelperE.AppendLine(sb, 2, "BinaryReader br = new BinaryReader(ms);");
                    CodeGeneratorHelperE.AppendLine(sb, 2, "int length = 0;");
                    length = csv_infos.Count;

                    for (int i = 0; i < length; i++)
                    {
                        CodeGeneratorHelperE.AppendLine(sb, 0, string.Empty);
                        string key = csv_infos[i]._className;
                        CodeGeneratorHelperE.AppendLine(sb, 2, "length = br.ReadInt32();");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "Dictionary<int, {0}> {1} = new Dictionary<int, {0}>();", key, key.ToLower());
                        //CodeGeneratorHelperE.AppendLine(sb, 2, "{0}.Clear();", key.ToLower());
                        CodeGeneratorHelperE.AppendLine(sb, 2, "for (int i = 0; i < length; i++)");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "{");
                        CodeGeneratorHelperE.AppendLine(sb, 3, "{0} tmp = new {0}();", key);
                        CodeGeneratorHelperE.AppendLine(sb, 3, "tmp.ToByteRead(br);", i);
                        CodeGeneratorHelperE.AppendLine(sb, 3, "{0}.Add(tmp.id, tmp); ", key.ToLower());
                        CodeGeneratorHelperE.AppendLine(sb, 2, "}");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "StaticCnf.Add({0});", key.ToLower());
                    }

                    CodeGeneratorHelperE.AppendLine(sb, 2, "ms.Flush();");
                    CodeGeneratorHelperE.AppendLine(sb, 2, "ms.Close();");
                    CodeGeneratorHelperE.AppendLine(sb, 2, "br.Close();");
                    CodeGeneratorHelperE.AppendLine(sb, 2, "ms.Dispose();");
                }
                CodeGeneratorHelperE.AppendLine(sb, 1, "}");
            }

            // void WriteByteConfig
            {
                CodeGeneratorHelperE.AppendLine(sb, 1, "public static void WriteByteConfig()");
                CodeGeneratorHelperE.AppendLine(sb, 1, "{");
                CodeGeneratorHelperE.AppendLine(sb, 2, "FileInfo file_info = new FileInfo(CnfConst.data_byte_path);");
                CodeGeneratorHelperE.AppendLine(sb, 3, "if (file_info.Exists)");
                CodeGeneratorHelperE.AppendLine(sb, 2, "file_info.Delete();");
                CodeGeneratorHelperE.AppendLine(sb, 2, "FileStream file_stream = file_info.Create();");
                CodeGeneratorHelperE.AppendLine(sb, 2, "BinaryWriter bw = new BinaryWriter(file_stream);");
                {
                    CodeGeneratorHelperE.AppendLine(sb, 2, "int length = 0;");
                    length = csv_infos.Count;
                    for (int i = 0; i < length; i++)
                    {
                        CodeGeneratorHelperE.AppendLine(sb, 0, string.Empty);

                        string key = csv_infos[i]._className;
                        CodeGeneratorHelperE.AppendLine(sb, 2, "List<{0}> tmp_{1} = new List<{0}>(StaticCnf.FindMap<{0}>().Values);", key, key.ToLower());
                        CodeGeneratorHelperE.AppendLine(sb, 2, "length = tmp_{0}.Count;", key.ToLower());
                        CodeGeneratorHelperE.AppendLine(sb, 2, "bw.Write(length);");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "for (int i = 0; i < length; i++)");
                        CodeGeneratorHelperE.AppendLine(sb, 2, "{");
                        CodeGeneratorHelperE.AppendLine(sb, 3, "{0} data = tmp_{1}[i];", key, key.ToLower());
                        CodeGeneratorHelperE.AppendLine(sb, 3, "data.ToByteWrite(bw);", i);
                        CodeGeneratorHelperE.AppendLine(sb, 2, "}");
                    }
                }

                CodeGeneratorHelperE.AppendLine(sb, 2, "file_stream.Flush();");
                CodeGeneratorHelperE.AppendLine(sb, 2, "file_stream.Close();");

                CodeGeneratorHelperE.AppendLine(sb, 2, "bw.Close();");
                CodeGeneratorHelperE.AppendLine(sb, 2, "file_stream.Dispose();");
                CodeGeneratorHelperE.AppendLine(sb, 1, "}");
            }
            CodeGeneratorHelperE.AppendLine(sb, 0, "}");
            return sb.ToString();
        }
    }
}