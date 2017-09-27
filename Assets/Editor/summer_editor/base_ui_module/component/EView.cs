using UnityEngine;
using System.Collections;
using UnityEditor;

namespace SummerEditor
{
    public class EView
    {
        public static void Label(Rect position, string text)
        {
            //GUI.Label(position, text);
            EditorGUI.LabelField(position, text);
        }

        public static bool Button(Rect position, string text, GUIStyle style)
        {
            return GUI.Button(position, text, style);
        }

        public static bool Toggle(Rect position, bool value, string text)
        {
            return EditorGUI.Toggle(position, value, text);
        }

        public static int Toolbar(Rect position, int selected, string[] texts)
        {
            return GUI.Toolbar(position, selected, texts);
        }

        public static void DrawTexture(Rect position, Texture image)
        {
            GUI.DrawTexture(position, image);
        }

        public static void Box(Rect position, string text)
        {
            GUI.Box(position, text);
        }
        public static Vector2 BeginScrollView(Rect position, Vector2 scroll_position, Rect view_rect)
        {
            return GUI.BeginScrollView(position, scroll_position, view_rect);
        }

        public static void EndScrollView()
        {
            GUI.EndScrollView();
        }

    }
}


