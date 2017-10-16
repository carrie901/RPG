using UnityEngine;

namespace Summer
{
    public class SkillAreaSet : MonoBehaviour
    {
        #region prefab

        public SkillAreaElement pfb_circle;
        public SkillAreaElement pfb_cube;
        public SkillAreaElement pfb_sector_60;
        public SkillAreaElement pfb_sector_120;

        #endregion
        public SkillJoystick joystick;
        public SkillAreaElement curr_element;
        public Transform _owner;

        Vector3 _delta_vec;

        float outer_radius = 6;                 // 外圆半径
        float inner_radius = 2f;                // 内圆半径
        float cube_width = 2f;                  // 矩形宽度 （矩形长度使用的外圆半径）
        int angle = 60;                         // 扇形角度

        bool _is_pressed;

        #region mono

        void Start()
        {
            Register();
            //curr_element.SetTarget(_owner);
        }

        void Update()
        {

        }

        void LateUpdate()
        {
            if (_is_pressed)
                OnUpdate();
        }

        void OnDestroy()
        {
            UnRegister();
        }

        public void Register()
        {
            joystick.on_joystick_down_event += OnJoystickDownEvent;
            joystick.on_joystick_move_event += OnJoystickMoveEvent;
            joystick.on_joystick_up_event += OnJoystickUpEvent;
        }

        public void UnRegister()
        {
            // ReSharper disable once DelegateSubtraction
            joystick.on_joystick_down_event -= OnJoystickDownEvent;
            // ReSharper disable once DelegateSubtraction
            joystick.on_joystick_move_event -= OnJoystickMoveEvent;
            // ReSharper disable once DelegateSubtraction
            joystick.on_joystick_up_event -= OnJoystickUpEvent;
        }

        #endregion 

        #region Joystick Event

        private void OnJoystickUpEvent()
        {
            _is_pressed = false;
            HideElements();
        }

        private void OnJoystickMoveEvent(Vector2 delta_vec)
        {
            _delta_vec = new Vector3(delta_vec.x, 0, delta_vec.y);
        }

        private void OnJoystickDownEvent(Vector2 delta_vec)
        {
            _is_pressed = true;
            _delta_vec = new Vector3(delta_vec.x, 0, delta_vec.y);
        }
        #endregion

        public void OnUpdate()
        {
            if (curr_element != null)
                curr_element.OnUpdate(_delta_vec);
        }

        public void HideElements()
        {
            if (curr_element != null)
                curr_element.gameObject.SetActive(true);
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


