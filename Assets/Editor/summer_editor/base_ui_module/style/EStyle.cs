using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace SummerEditor
{
    public class EStyle
    {
        public float LineHeight
        {
            get { return line_height; }
            set { line_height = value; }
        }
        float line_height = 25;

        public string GetSortMark(bool descending)
        {
            return descending ? " ▼" : " ▲";
        }

        public readonly static Color title_color = new Color32(38, 158, 111, 255);        // basically green
        public readonly static Color title_color_selected = new Color32(19, 80, 60, 255);  // dark green

        public readonly static Color selection_color = new Color32(62, 95, 150, 255);
        public readonly static Color selection_color_dark = new Color32(62, 95, 150, 128);
        public GUIStyle GetTitleStyle(bool selected)
        {
            if (style_title == null || title_ordinary == null || title_selected == null)
            {
                style_title = new GUIStyle(EditorStyles.whiteBoldLabel);
                style_title.alignment = TextAnchor.MiddleCenter;
                title_ordinary = GetColorTexture(title_color);
                title_selected = GetColorTexture(title_color_selected);
            }

            style_title.normal.background = selected ? title_selected : title_ordinary;
            style_title.normal.textColor = selected ? Color.yellow : Color.white;
            return style_title;
        }
        private GUIStyle style_title;
        private Texture2D title_ordinary;
        private Texture2D title_selected;

        public GUIStyle StyleLine
        {
            get
            {
                if (style_line == null)
                {
                    style_line = new GUIStyle(EditorStyles.whiteLabel);
                    style_line.normal.background = GetColorTexture(new Color(0.5f, 0.5f, 0.5f, 0.1f));
                    style_line.normal.textColor = Color.white;
                }
                return style_line;
            }
        }
        private GUIStyle style_line;

        public GUIStyle StyleLineAlt
        {
            get
            {
                if (style_line_alt == null)
                {
                    style_line_alt = new GUIStyle(EditorStyles.whiteLabel);
                    style_line_alt.normal.background = GetColorTexture(new Color(0.5f, 0.5f, 0.5f, 0.2f));
                    style_line_alt.normal.textColor = Color.white;
                }
                return style_line_alt;
            }
        }
        private GUIStyle style_line_alt;

        public GUIStyle StyleSelected
        {
            get
            {
                if (style_selected == null)
                {
                    style_selected = new GUIStyle(EditorStyles.whiteLabel);
                    style_selected.normal.background = GetColorTexture(selection_color);
                    style_selected.normal.textColor = Color.white;
                }
                return style_selected;
            }
        }
        private GUIStyle style_selected;

        public GUIStyle StyleSelectedCell
        {
            get
            {
                if (style_selected_cell == null)
                {
                    style_selected_cell = new GUIStyle(EditorStyles.whiteBoldLabel);
                    style_selected_cell.normal.background = GetColorTexture(selection_color_dark);
                    style_selected_cell.normal.textColor = Color.yellow;
                }
                return style_selected_cell;
            }
        }
        private GUIStyle style_selected_cell;

        private static readonly Dictionary<Color, Texture2D> s_color_textures = new Dictionary<Color, Texture2D>();
        public static Texture2D GetColorTexture(Color c)
        {
            Texture2D tex;
            s_color_textures.TryGetValue(c, out tex);
            if (tex == null) //Texture2D对象在游戏结束时为null
            {
                tex = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                tex.SetPixel(0, 0, c);
                tex.Apply();

                s_color_textures[c] = tex;
            }
            return tex;
        }
    }
}
