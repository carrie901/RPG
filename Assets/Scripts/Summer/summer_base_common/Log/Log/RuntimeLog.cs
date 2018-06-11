using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    public class RuntimeLog : MonoBehaviour, I_Log
    {
        protected static RuntimeLog _instance;
        public static RuntimeLog Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "RuntimeLog";
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<RuntimeLog>();
                }

                return _instance;
            }
        }

        #region Log

        public void Log(string message)
        {
            lines.Add(string.Format("[Log]:<color=#ffffff>  {0}</color>", message));
            LimitLogCount();
        }

        public void Log(string message, params object[] args)
        {
            lines.Add(string.Format("[Log]:<color=#ffffff>  {0}</color>", string.Format(message, args)));
            LimitLogCount();
        }

        public void Waring(string message)
        {
            lines.Add(string.Format("[Warning]:<color=#ffffff>  {0}</color>", message));
            LimitLogCount();
        }

        public void Warning(string message, params object[] args)
        {
            lines.Add(string.Format("[Warning]:<color=#ffffff>  {0}</color>", string.Format(message, args)));
            LimitLogCount();
        }

        public void Error(string message)
        {
            lines.Add(string.Format("[ERROR]:<color=#ff0000>  {0}</color>", message));
            LimitLogCount();
        }

        public void Error(string message, params object[] args)
        {
            lines.Add(string.Format("[ERROR]:<color=#ff0000>  {0}</color>", string.Format(message, args)));
            LimitLogCount();
        }

        public void Assert(bool condition, string message)
        {

        }

        public void Assert(bool condition, string message, params object[] args)
        {

        }

        public void Quit()
        {
            
        }

        #endregion




        public int max_logs = 1000;


        protected readonly List<string> lines = new List<string>();
        protected Vector2 scroll_position;
        protected bool visible;
        const string WINDOW_TITLE = "Console";
        readonly Rect title_bar_rect = new Rect(0, 0, 1000, 40);
        Rect window_rect = new Rect(20, 20, Screen.width - (20 * 2), Screen.height - (20 * 2));

        void OnGUI()
        {
            if (GUI.Button(new Rect(5, 5, 60, 40), "Console"))
                visible = !visible;

            if (visible)
            {
                window_rect = GUILayout.Window(123456, window_rect, DrawConsoleWindow, WINDOW_TITLE);
            }

            Color c = GUI.color;
        }

        public void DrawConsoleWindow(int window_id)
        {

            scroll_position = GUILayout.BeginScrollView(scroll_position);
            int length = lines.Count;
            for (var i = 0; i < length; i++)
                GUILayout.Label(lines[i]);

            GUILayout.EndScrollView();
            GUI.contentColor = Color.white;
            //
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("清除"))
                lines.Clear();
            GUILayout.EndHorizontal();
            // 拖拽活动区域
            GUI.DragWindow(title_bar_rect);
        }


        public void LimitLogCount()
        {
            int amount_to_remove = Mathf.Max(lines.Count - max_logs, 0);

            if (amount_to_remove == 0)
                return;
            lines.RemoveRange(0, amount_to_remove);
        }
    }
}

