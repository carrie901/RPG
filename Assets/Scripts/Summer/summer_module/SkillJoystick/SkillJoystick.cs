using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-16 15:55:55
// FileName : SkillJoystick.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 必须在Canvas下 over模式
    /// TODO 需求
    ///     1.外形的改变，聚焦变化
    ///     2.静态和动态
    /// </summary>
    public class SkillJoystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler,IPointerExitHandler
    {
        public static SkillJoystick instance;
        #region 属性

        public float outer_circle_radius = 75;              // 外圈的半径
        public RectTransform thumb;                         // 移动的物体

        protected Canvas cache_root_canvas;                 // 父类必须是Canvas 
        protected RectTransform cache_recttrans;            // 当前的Tranform
        public bool is_touch = false;
        public Vector2 direction = Vector2.zero;            // 默认方向
       

        #region private

        protected Vector2 thumb_position;

        #endregion

        #region Action 事件

        public Action on_down_event;                        // 按下事件
        public Action on_up_event;                          // 抬起事件
        public Action<Vector2> on_move_event;               // 滑动事件

        #endregion

        #endregion

        void Awake()
        {
            cache_root_canvas = transform.parent.GetComponent<Canvas>();
            cache_recttrans = transform.GetComponent<RectTransform>();
            instance = this;
        }

        // 按下
        public void OnPointerDown(PointerEventData event_data)
        {
            is_touch = true;
            // 开始移动
            event_data.pressPosition = transform.position;
            OnDrag(event_data);
            if (on_down_event != null)
                on_down_event();
        }

        // 抬起
        public void OnPointerUp(PointerEventData event_data)
        {
            is_touch = false;
            direction = Vector2.zero;
            //JoystickController.mJoystickIsMoved = false;
            thumb.anchoredPosition = Vector2.zero;
            if (on_up_event != null)
                on_up_event();
        }

        // 滑动
        public void OnDrag(PointerEventData event_data)
        {
            
            float radius = GetRadius();

            thumb_position = (event_data.position - event_data.pressPosition) / cache_recttrans.localScale.x;
            thumb_position.x = Mathf.FloorToInt(thumb_position.x);
            thumb_position.y = Mathf.FloorToInt(thumb_position.y);

            
            if (thumb_position.magnitude > radius)
                thumb_position = thumb_position.normalized * radius;

            thumb.anchoredPosition = thumb_position;

            // 确定方向
            direction = new Vector2(thumb_position.x, thumb_position.y).normalized;
        }

        public void OnPointerEnter(PointerEventData event_data)
        {
            //Debug.Log("OnPointerEnter");
            /*if (joy_type == E_SkillJoystickType.dynamic_joy && !is_dynamic_actif /*&& _activated#1#)
            {
                event_data.pointerDrag = gameObject;
                event_data.pointerPress = gameObject;

                is_dynamic_actif = true;
            }

            if (joy_type == E_SkillJoystickType.dynamic_joy && !event_data.eligibleForClick)
            {
                
            }*/
            //OnPointerUp(event_data);
        }

        public void OnPointerExit(PointerEventData event_data)
        {

        }

        public void OnBeginDrag(PointerEventData event_data)
        {
            //Debug.Log("OnBeginDrag");
        }




        #region 私有方法

        public float GetRadius()
        {
            return outer_circle_radius;
        }

        #endregion

        
    }

    /*public enum E_SkillJoystickType
    {
        static_joy,                 // 静态
        dynamic_joy,                // 动态
    }*/
}
