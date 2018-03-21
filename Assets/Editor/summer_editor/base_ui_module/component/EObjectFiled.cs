using UnityEngine;
using System.Collections;
using UnityEditor;

namespace SummerEditor
{
    public class EObjectFiled : ERectItem
    {
        public EObjectFiled(float width, float height) : base(width, height)
        {
        }

        public GameObject _go;

        public void SetGameObject(GameObject go)
        {
            _go = go;
        }

        public override void Draw()
        {
            Object tmp_go = EView.ObjectFiled(_world_pos, _go);
            if (tmp_go != null && tmp_go is GameObject)
            {
                _go = tmp_go as GameObject;
            }

            if (tmp_go)
            {
                //if (GUI.Button(_world_pos, "Check Dependencies"))
                //    Selection.activeObject = tmp_go;
            }
            else
            {
                //EditorGUI.LabelField(_world_pos, "Missing:", "Select an object first");
            }
        }
    }
}
