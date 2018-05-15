using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 检测选中的GameObject 使用了哪些图集和纹理
    /// </summary>
    public class TextureAndSpriteScanHelper
    {
        [MenuItem("GameObject/UI辅助工具/检测/图片/检索使用了哪些图集和纹理", false, 2)]
        public static void QueryTextureMenu()
        {
            QueryTexture();
        }


        // 检索指定UI中的所有Image所用的图集
        public static void QueryTexture()
        {
            // 原理:找到Prefab上的所有的guid值和所有的纹理和图集做比较
            // 通过Sprite的guid来查询Sprite的详细信息

            // 1.其中的Guid和Packing Tag <Key=Guid,Value=PackingTag>
            Dictionary<string, ESpriteCnf> all_sprite_guids = ESpriteHelper.GetAllSpriteInfo();

            // 2.查找Prefab的实际保存的路径
            string path = EPrefabHelper.FindPrefabPath(Selection.activeGameObject);

            // 3.读取指定位置的prefab的Guid值集合
            List<string> guid_list = EGuidHelper.GetGuidsByFile(path);

            // 4.统计用到的纹理tag
            List<string> sprite_packing_tags = new List<string>();
            // 5.统计使用到的空的tag
            List<ESpriteCnf> empty_list = new List<ESpriteCnf>();
            int length = guid_list.Count;
            for (int i = 0; i < length; i++)
            {
                if (!all_sprite_guids.ContainsKey(guid_list[i]))
                    continue;

                ESpriteCnf info = all_sprite_guids[guid_list[i]];
                if (string.IsNullOrEmpty(info.packingtag))
                {
                    empty_list.Add(info);
                    continue;
                }

                if (sprite_packing_tags.Contains(info.packingtag))
                    continue;
                sprite_packing_tags.Add(info.packingtag);
            }

            Debug.Log("使用到的Sprite Packing Tag Count:" + sprite_packing_tags.Count);
            for (int i = 0; i < sprite_packing_tags.Count; i++)
                Debug.Log(sprite_packing_tags[i]);

            Debug.Log("没有使用Sprite Packing Tag 或者是Texture 的");
            for (int i = 0; i < empty_list.Count; i++)
                Debug.Log("Path:" + empty_list[i].path);
            // 输出相关信息
        }
    }
}

