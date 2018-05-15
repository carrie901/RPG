using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Summer;
using UnityEditor;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-8 11:45:14
// FileName : ESpriteHelper.cs
//=============================================================================

namespace SummerEditor
{
    public class ESpriteHelper
    {
        /// <summary>
        /// 得到所有的Sprite图集的地址
        /// </summary>
        public static List<string> GetAllSpritePath()
        {
            string[] files = Directory.GetFiles(ESpriteConst.ui_sprite_root_dir, "*.*", SearchOption.AllDirectories);

            EndsWithFilter suffix = new EndsWithFilter();
            suffix.AddSuffix(ESpriteConst.sprite_suffix);
            List<string> infos = SuffixHelper.Filter(files, suffix);
            return infos;
        }

        public static Dictionary<string, ESpriteCnf> GetAllSpriteInfo()
        {
            List<string> all_sprite_paths = GetAllSpritePath();

            Dictionary<string, ESpriteCnf> gui_spritepacking_map = new Dictionary<string, ESpriteCnf>();
            int length = all_sprite_paths.Count;
            for (int i = 0; i < length; i++)
            {
                ESpriteCnf sprite_cnf = new ESpriteCnf();
                sprite_cnf.SetPath(all_sprite_paths[i]);

                if (gui_spritepacking_map.ContainsKey(sprite_cnf.guid))
                {
                    Debug.LogError("guid:" + sprite_cnf.guid + " \n" + all_sprite_paths[i] + "\n" +
                                   gui_spritepacking_map[sprite_cnf.guid]);
                }
                else
                {
                    gui_spritepacking_map.Add(sprite_cnf.guid, sprite_cnf);
                }
                EditorUtility.DisplayProgressBar("扫描图片中的Guid和SpritePackingTag", all_sprite_paths[i], (float)i / length);
            }
            
            EditorUtility.ClearProgressBar();
            return gui_spritepacking_map;
        }

        /// <summary>
        /// 找到meta文件中的spritePackingTag
        /// </summary>
        public static string FindSpritePackingTag(string meta_text)
        {
            string[] contents = meta_text.Split('\n');
            int length = contents.Length;
            for (int i = 0; i < length; i++)
            {
                string text = contents[i];
                bool result = text.Contains("spritePackingTag:");
                if (result)
                {
                    string info = contents[i].Replace("spritePackingTag:", "");
                    return info.Trim();
                }
            }
            return string.Empty;
        }
    }

    public class ESpriteConst
    {
        /// <summary>
        /// 图集的目标地址
        /// </summary>
        public static string ui_sprite_root_dir = Application.dataPath + "/UIResources/UITexture";

        /// <summary>
        /// Sprite 的后缀名
        /// </summary>
        public static List<string> sprite_suffix = new List<string>() { ".png.meta", ".jpg.meta" };
    }
}
