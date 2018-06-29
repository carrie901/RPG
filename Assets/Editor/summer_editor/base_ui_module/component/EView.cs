using System;
using UnityEngine;
using System.Collections;
using UnityEditor;
using Object = UnityEngine.Object;

namespace SummerEditor
{
    public class EView
    {
        public static void Label(Rect position, string text)
        {
            //GUI.Label(position, text);
            EditorGUI.LabelField(position, text);
        }

        public static string TextArea(Rect position, string text)
        {
            return EditorGUI.TextArea(position, text);
        }

        public static bool Button(Rect position, string text, GUIStyle style)
        {
            return GUI.Button(position, text);
        }

        public static bool Toggle(Rect position, bool value, string text)
        {
            return GUI.Toggle(position, value, text);
        }

        public static int Toolbar(Rect position, int selected, string[] texts)
        {
            return GUI.Toolbar(position, selected, texts);
        }

        public static void DrawTexture(Rect position, Texture image)
        {
            if (image == null) return;
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

        public static int IntInput(Rect position, int value)
        {
            return EditorGUI.IntField(position, value);
        }

        public static string Input(Rect position, string text)
        {
            return GUI.TextField(position, text);
        }

        public static Object ObjectFiled(Rect position, Object go)
        {
            Object game_object = EditorGUI.ObjectField(position, go, typeof(GameObject), false) as GameObject;
            return game_object;
        }

        public static Enum EnumPopup(Rect position, System.Enum selected)
        {
            return EditorGUI.EnumPopup(position, selected);
        }

        public static int EnumPopup(Rect position, string des, int index, string[] names)
        {
            int i = EditorGUI.Popup(position, des, index, names);
            return i;
        }


        public static void EndScrollView()
        {
            GUI.EndScrollView();
        }

    }
}


