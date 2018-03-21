using System;
using UnityEngine;
using System.Collections;
// ReSharper disable All

namespace Summer
{
    public class ViewMessageBox
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }

    public class MessageBoxData
    {
        public Action _on_left;
        public Action _on_right;
        public string Title { get; set; }

        public void AddListenerLeft(Action action)
        {
            _on_left = action;
        }

        public void AddListenerRight(Action action)
        {
            _on_right = action;
        }
    }

}


