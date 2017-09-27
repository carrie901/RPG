using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillAreaSet : MonoBehaviour
    {
        #region prefab

        public GameObject pfb_circle;
        public GameObject pfb_cube;
        public GameObject pfb_sector_60;
        public GameObject pfb_sector_120;

        #endregion

        public Transform owner;
        public E_SkillAreaType area_type;       // 设置指示器类型

        Vector3 delta_vec;

        float outer_radius = 6;                 // 外圆半径
        float inner_radius = 2f;                // 内圆半径
        float cube_width = 2f;                  // 矩形宽度 （矩形长度使用的外圆半径）
        int angle = 60;                         // 扇形角度

        bool is_pressed = false;

        #region mono
        void Start()
        {

        }

        void Update()
        {

        }

        void LateUpdate()
        {
            if (is_pressed)
                UpdateElement();
        }

        #endregion 

        #region Joystick Event
        void OnJoystickDownEvent(Vector2 deltaVec)
        {
            is_pressed = true;
            delta_vec = new Vector3(deltaVec.x, 0, deltaVec.y);
            CreateSkillArea();
        }

        void OnJoystickUpEvent()
        {
            is_pressed = false;
            HideElements();
        }

        void OnJoystickMoveEvent(Vector2 deltaVec)
        {
            delta_vec = new Vector3(deltaVec.x, 0, deltaVec.y);
        }
        #endregion

        public void UpdateElement()
        {

        }

        public void UpdateElementPosition()
        {

        }

        public void HideElements()
        {

        }
        //创建技能区域展示
        public void CreateSkillArea()
        {

        }

        //建技能区域展示元素
        public void CreateElement(E_SkillAreaElement element)
        {

        }
    }
}


