using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.Reflection;

public delegate void SelectionHandler(object selected, int col);

public partial class TableView : IDisposable
{
    // show internal sequential ID in the first column
    public bool ShowInternalSeqID = false;

    public event SelectionHandler OnSelected;

    public TableViewAppr Appearance { get { return _appearance; } }

    public TableView(EditorWindow hostWindow, Type itemType)
    {
        m_hostWindow = hostWindow;
        m_itemType = itemType;
    }

    public void Dispose()
    {

    }

    public void ClearColumns()
    {
        m_descArray.Clear();
    }

    public bool AddColumn(string colDataPropertyName, string colTitleText, float widthByPercent, TextAnchor alignment = TextAnchor.MiddleCenter, string fmt = "")
    {
        TableViewColDesc desc = new TableViewColDesc();
        desc.property_name = colDataPropertyName;
        desc.title_text = colTitleText;
        desc.Alignment = alignment;
        desc.width_in_percent = widthByPercent;
        desc.format = string.IsNullOrEmpty(fmt) ? null : fmt;
        desc.mem_info = m_itemType.GetField(desc.property_name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
        if (desc.mem_info == null)
        {
            desc.mem_info = m_itemType.GetProperty(desc.property_name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetProperty);
            if (desc.mem_info == null)
            {
                Debug.LogWarningFormat("Field '{0}' accessing failed.", desc.property_name);
                return false;
            }
        }

        m_descArray.Add(desc);
        return true;
    }

    public void RefreshData(List<object> entries, Dictionary<object, Color> specialTextColors = null)
    {
        m_lines.Clear();

        if (entries != null && entries.Count > 0)
        {
            m_lines.AddRange(entries);

            SortData();
        }

        m_specialTextColors = specialTextColors;
    }

    public void Draw(Rect area)
    {
        GUILayout.BeginArea(area);
        _scroll_pos = GUILayout.BeginScrollView(_scroll_pos, GUIStyle.none, GUI.skin.verticalScrollbar);
        {
            GUIStyle s = new GUIStyle();
            s.fixedHeight = _appearance.LineHeight * (m_lines.Count + 1);
            s.stretchWidth = true;
            Rect r = EditorGUILayout.BeginVertical(s);
            {
                // this silly line (empty label) is required by Unity to ensure the scroll bar appear as expected.
                PAEditorUtil.DrawLabel("", _appearance.Style_Line);

                DrawTitle(r.width);

                // these first/last calculatings are for smart clipping 
                int firstLine = Mathf.Max((int)(_scroll_pos.y / _appearance.LineHeight) - 1, 0);
                int shownLineCount = (int)(area.height / _appearance.LineHeight) + 2;
                int lastLine = Mathf.Min(firstLine + shownLineCount, m_lines.Count);

                for (int i = firstLine; i < lastLine; i++)
                {
                    DrawLine(i + 1, m_lines[i], r.width);
                }
            }
            EditorGUILayout.EndVertical();
        }
        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }

    public void SetSortParams(int sort_slot, bool descending)
    {
        _sort_slot = sort_slot;
        _descending = descending;
    }

    public void SetSelected(object obj)
    {
        m_selected = obj;

        if (OnSelected != null)
            OnSelected(obj, 0);
    }

    public object GetSelected()
    {
        return m_selected;
    }
}
