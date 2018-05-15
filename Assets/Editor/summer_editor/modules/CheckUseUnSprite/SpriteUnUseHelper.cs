using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
using Summer;
using UnityEditor;
using UnityEngine;
namespace SummerEditor
{
    /// <summary>
    /// UI冗余图片扫描 针对整个UI的图集纹理扫描
    /// </summary>
    public class SpriteUnUseHelper
    {
        public static void ScanAllSprite()
        {
            // 1.找到所有的UI图片的meta文件
            // 2.读取meta文件中的Guid值  key=guid value=path
            Dictionary<string, ESpriteCnf> png_guid_map = ESpriteHelper.GetAllSpriteInfo();

            // 3.分析prefab中的guid是否被引用
            int ui_prefab_length = SpriteUnUseConst.ui_prefab_root_dirs.Length;
            for (int ui_prefab_index = 0; ui_prefab_index < ui_prefab_length; ui_prefab_index++)
            {
                // 读取所有的prefab
                string[] prefabs_path = Directory.GetFiles(SpriteUnUseConst.ui_prefab_root_dirs[ui_prefab_index], "*.prefab", SearchOption.AllDirectories);
                int length = prefabs_path.Length;
                for (int i = 0; i < length; i++)
                {
                    string path = prefabs_path[i];
                    EditorUtility.DisplayProgressBar("获取引用关系", path, (float)i / length);

                    // 读取指定位置的prefab的Guid值
                    List<string> guid_list = EGuidHelper.GetGuidsByFile(path);

                    FileCompare(png_guid_map, guid_list);
                }
            }

            EditorUtility.ClearProgressBar();

            // 没有被引用的guid写入到txt中
            WriteUnUseFiles(png_guid_map);
        }

        // 记录相关信息
        public static void WriteUnUseFiles(Dictionary<string, ESpriteCnf> png_guid_map)
        {
            List<string> un_use_files = new List<string>();
            foreach (var info in png_guid_map)
            {
                un_use_files.Add(info.Value.path);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append("没有被使用的文件: ");
            sb.Append(un_use_files.Count);
            sb.Append("\n");
            un_use_files.ForEach(kv => sb.Append(kv + "\n"));

            File.WriteAllText("..\\three_check\\Scan_Result.txt", sb.ToString());
            EditorUtility.DisplayDialog("扫描成功", string.Format("共找到{0}个冗余图片\n请在Assets/Scan_Result.txt查看结果", un_use_files.Count), "Ok");
        }

        public static void FileCompare(Dictionary<string, ESpriteCnf> png_guid_map, List<string> guid_list)
        {
            // 检索这个guid被引用了，如果引用就剔除
            int guid_length = guid_list.Count;
            for (int i_guid = 0; i_guid < guid_length; i_guid++)
            {
                string tmp_guid = guid_list[i_guid];
                if (png_guid_map.ContainsKey(tmp_guid))
                {
                    png_guid_map.Remove(tmp_guid);
                }
            }
        }
    }
}


