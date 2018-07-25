
using UnityEditor;
using UnityEngine;

namespace SummerEditor
{
    public class EditorTextMenu
    {
        [MenuItem("GameObject/UI辅助工具/检测/字体检测/1.字体引用个数检测", false, 1)]
        public static void CheckTextRef()
        {
            EditorFontHelper.CheckTextRef();
        }

        [MenuItem("GameObject/UI辅助工具/检测/字体检测/2.字体带碰撞属性", false, 2)]
        public static void CheckTextRaycast()
        {
            EditorFontHelper.CheckTextRaycast();
        }

        [MenuItem("GameObject/UI辅助工具/检测/字体检测/3.去掉字体的碰撞框", false, 3)]
        public static void SetTextRaycast()
        {
            EditorFontHelper.SetTextRaycast();
        }

        [MenuItem("GameObject/UI_Panel保存工具", false, 0)]
        public static void CheckPrefab()
        {
            MonoBehaviour[] monos = Selection.activeGameObject.GetComponentsInChildren<MonoBehaviour>();

            for (int i = 0; i < monos.Length; i++)
            {
                PrefabApply.CheckGameObject(monos[i]);
            }
            EditorUtility.SetDirty(Selection.activeGameObject);
            AssetDatabase.SaveAssets();

        }
    }
}
