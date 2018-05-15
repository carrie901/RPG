using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    public class EditorFontHelper
    {
        //有效字体
        public static List<string> font_list = new List<string>()
        {
            "summer",           //正常的字体使用
            "cd_time",          // 技能的倒计时CD
        };

        /// <summary>
        /// 验证UI中字体的合格程度
        /// 1.字体是否有碰撞
        /// 2.字体的引用是否符合标准
        /// </summary>
        public static void CheckTextRef()
        {
            List<ItemRes> text_list = new List<ItemRes>();
            EditorItemResHelper.FindAllText(Selection.activeGameObject, text_list);

            Dictionary<string, List<ItemTextRes>> font_map = new Dictionary<string, List<ItemTextRes>>();
            foreach (var info in text_list)
            {
                if (!font_map.ContainsKey(info.ItemName))
                {
                    font_map.Add(info.ItemName, new List<ItemTextRes>());
                }
                font_map[info.ItemName].Add(info as ItemTextRes);
            }

            Debug.Log("----------------字体引用----------------- 一共引用了" + font_map.Count);
            foreach (var info in font_map)
            {
                Debug.LogError(info.Key);
            }
            foreach (var info in font_map)
            {
                List<ItemTextRes> text_res_list = info.Value;
                for (int i = 0; i < text_res_list.Count; i++)
                {
                    Debug.LogError(text_res_list[i].target_gameobject.name + "_" + text_res_list[i].ItemName, text_res_list[i].target_gameobject);
                }
            }

            /*foreach (var info in font_map)
                {
                    if (!font_list.Contains(info.Key))
                    {
                        List<ItemTextRes> text_res_list = info.Value;
                        for (int i = 0; i < text_res_list.Count; i++)
                        {
                            Debug.LogError(text_res_list[i].target_gameobject.name + "_" + text_res_list[i].ItemName, text_res_list[i].target_gameobject);
                        }
                    }
                }*/
        }

        public static void CheckTextRaycast()
        {

            List<ItemRes> text_list = new List<ItemRes>();
            EditorItemResHelper.FindAllText(Selection.activeGameObject, text_list);
            Debug.Log("----------------字体带碰撞-----------------");
            foreach (var info in text_list)
            {
                if (info.raycast_target)
                {
                    Debug.Log(info.target_gameobject.name, info.target_gameobject);
                }
            }
        }

        public static void SetTextRaycast()
        {

            List<ItemRes> text_list = new List<ItemRes>();
            EditorItemResHelper.FindAllText(Selection.activeGameObject, text_list);
            Debug.Log("----------------字体带碰撞-----------------");
            foreach (var info in text_list)
            {
                if (info.raycast_target)
                {
                    info.raycast_target = false;
                }
            }
            //PrefabUtility.ReplacePrefab(Selection.activeGameObject,)
            //EditorSceneManager.SaveOpenScenes();
            //EditorApplication.SaveScene(EditorApplication.currentScene);
            EditorUtility.DisplayDialog("去掉字体的碰撞框", "自动保存Prefab功能未完成，需要你手动保存prefab，多次点击Apply，然后多次点击control+S", "确定");
        }
    }

}
