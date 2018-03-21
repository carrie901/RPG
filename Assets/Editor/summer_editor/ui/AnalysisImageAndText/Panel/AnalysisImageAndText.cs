using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// 先以打印的形式来显示
    /// 1.界面使用的Font字体(由于现在的字体混乱)的个数
    /// 2.碰撞信息，碰撞必定带着ButtonSound
    /// 3.文本文本化
    /// </summary>
    public class AnalysisImageAndText
    {


        //[MenuItem("GameObject/UI辅助工具/本地化/2.本地化文本", false, 3)]
        public static void CheckLanguageLoc1()
        {
            List<ItemRes> text_list = new List<ItemRes>();
            EditorItemResHelper.FindAllText(Selection.activeGameObject, text_list);
            int length = text_list.Count;
            for (int i = 0; i < length; i++)
            {
                (text_list[i] as ItemTextRes).SetLanguage(true);
            }
        }

        //[MenuItem("GameObject/UI辅助工具/本地化/1.置空本地化文本", false, 2)]
        public static void CheckLanguageLoc2()
        {
            List<ItemRes> text_list = new List<ItemRes>();
            EditorItemResHelper.FindAllText(Selection.activeGameObject, text_list);
            int length = text_list.Count;
            for (int i = 0; i < length; i++)
            {
                (text_list[i] as ItemTextRes).SetLanguage(false);
            }
        }

        public static void AnalysisRes(GameObject select_go, Dictionary<E_ItemResType, List<ItemRes>> res_map)
        {
            if (select_go == null)
                return;
            Image[] imgs = select_go.GetComponentsInChildren<Image>();
            RawImage[] raw_imgs = select_go.GetComponentsInChildren<RawImage>();
            Text[] texts = select_go.GetComponentsInChildren<Text>();


            res_map.Clear();
            int length = imgs.Length;
            for (int i = 0; i < length; i++)
            {
                res_map[E_ItemResType.img].Add(EditorItemResHelper.CollectImgRes(imgs[i]));
            }

            length = raw_imgs.Length;
            for (int i = 0; i < length; i++)
            {
                res_map[E_ItemResType.raw_image].Add(EditorItemResHelper.CollectRawImageRes(raw_imgs[i]));
            }


            length = texts.Length;
            for (int i = 0; i < length; i++)
            {
                res_map[E_ItemResType.text].Add(EditorItemResHelper.CollectTextRes(texts[i]));
            }
        }




    }
}

