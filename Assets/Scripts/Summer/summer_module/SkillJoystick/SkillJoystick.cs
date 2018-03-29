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
    public class SkillJoystick : MonoBehaviour, IPointerEnterHandler, IDragHandler, IBeginDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        #region 属性

        public float outer_circle_radius = 100;
        public RectTransform thumb;


        protected Canvas cache_root_canvas;                  // 父类必须是Canvas 
        protected RectTransform cache_recttrans;
        public static SkillJoystick instance;

        protected Vector2 outer_circle_start_world_pos = Vector2.zero;


        #region Action 事件

        public Action on_down_event;                        // 按下事件
        public Action on_up_event;                          // 抬起事件
        public Action<Vector2> on_move_event;               // 滑动事件

        #endregion


        public bool is_on_drag;                               // 处于拖拽状态
        protected Vector2 _dir = Vector2.zero;

        #region private

        protected Vector2 thumb_position;
        protected Vector2 tmp_axis;
        protected Vector2 old_tmp_axis;
        protected bool is_on_touch;

        #endregion

        #endregion

        void Awake()
        {
            cache_root_canvas = transform.parent.GetComponent<Canvas>();
            cache_recttrans = transform.GetComponent<RectTransform>();
            instance = this;
        }

        void Start()
        {
            outer_circle_start_world_pos = transform.position;
        }

        // 按下
        public void OnPointerDown(PointerEventData event_data)
        {
            // 开始移动
            event_data.pressPosition = transform.position;
            OnDrag(event_data);
            if (on_down_event != null)
                on_down_event();
        }

        // 抬起
        public void OnPointerUp(PointerEventData event_data)
        {
            //JoystickController.mJoystickIsMoved = false;
            thumb.anchoredPosition = Vector2.zero;
            if (on_up_event != null)
                on_up_event();
            _dir = Vector2.zero;
        }


        // 滑动
        public void OnDrag(PointerEventData event_data)
        {
            is_on_drag = true;
            is_on_touch = true;

            float radius = GetRadius();

            thumb_position = (event_data.position - event_data.pressPosition) / cache_recttrans.localScale.x;


            thumb_position.x = Mathf.FloorToInt(thumb_position.x);
            thumb_position.y = Mathf.FloorToInt(thumb_position.y);

            if (thumb_position.magnitude > radius)
                thumb_position = thumb_position.normalized * radius;

            thumb.anchoredPosition = thumb_position;
        }


        public void OnPointerEnter(PointerEventData event_data)
        {
            /*if (joy_type == E_SkillJoystickType.dynamic_joy && !is_dynamic_actif /*&& _activated#1#)
            {
                event_data.pointerDrag = gameObject;
                event_data.pointerPress = gameObject;

                is_dynamic_actif = true;
            }

            if (joy_type == E_SkillJoystickType.dynamic_joy && !event_data.eligibleForClick)
            {
                
            }*/
            OnPointerUp(event_data);
        }

        public void OnBeginDrag(PointerEventData event_data)
        {

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
