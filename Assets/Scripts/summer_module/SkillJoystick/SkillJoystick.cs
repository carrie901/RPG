using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace Summer
{
    /// <summary>
    /// 一个简单版本的操纵杆
    /// </summary>
    public class SkillJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {

        public float outer_circle_radius = 100;

        Transform inner_circle_trans;
        Vector2 outer_circle_start_world_pos = Vector2.zero;

        public Action<Vector2> on_joystick_down_event;      // 按下事件
        public Action on_joystick_up_event;                 // 抬起事件
        public Action<Vector2> on_joystick_move_event;      // 滑动事件

        void Awake()
        {
            inner_circle_trans = transform.GetChild(0);
        }

        void Start()
        {
            outer_circle_start_world_pos = transform.position;
        }

        // 按下
        public void OnPointerDown(PointerEventData event_data)
        {
            inner_circle_trans.position = event_data.position;
            if (on_joystick_down_event != null)
                on_joystick_down_event(inner_circle_trans.localPosition / outer_circle_radius);
        }

        // 抬起
        public void OnPointerUp(PointerEventData event_data)
        {
            inner_circle_trans.localPosition = Vector3.zero;
            if (on_joystick_up_event != null)
                on_joystick_up_event();
        }

        // 滑动
        public void OnDrag(PointerEventData event_data)
        {
            Vector2 touch_pos = event_data.position - outer_circle_start_world_pos;
            if (Vector3.Distance(touch_pos, Vector2.zero) < outer_circle_radius)
                inner_circle_trans.localPosition = touch_pos;
            else
                inner_circle_trans.localPosition = touch_pos.normalized * outer_circle_radius;

            if (on_joystick_move_event != null)
                on_joystick_move_event(inner_circle_trans.localPosition / outer_circle_radius);
        }
    }
}

