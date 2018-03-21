using System;
using UnityEngine;

public class ThirdCamera : MonoBehaviour
{
    public Transform target = null;     // 目标玩家
    [SerializeField]
    [Range(0, 360)]
    float horizontal_angle = 270f;      // 水平角度
    [SerializeField]
    [Range(0, 20)]
    float initial_height = 2f;    // 人物在视野内屏幕中的位置设置

    [SerializeField]
    [Range(10, 90)]
    float initial_angle = 40f;   // 初始俯视角度
    [SerializeField]
    [Range(10, 90)]
    float maxAngle = 50f;     // 最高俯视角度
    [SerializeField]
    [Range(10, 90)]
    float minAngle = 35f;     // 最低俯视角度

    float initial_distance;    // 初始化相机与玩家的距离 根据角度计算
    [SerializeField]
    [Range(1, 100)]
    float maxDistance = 20f;        // 相机距离玩家最大距离
    [SerializeField]
    [Range(1, 100)]
    float minDistance = 5f;        // 相机距离玩家最小距离

    [SerializeField]
    [Range(1, 100)]
    float zoomSpeed = 50;       // 缩放速度

    [SerializeField]
    [Range(1f, 200)]
    float swipeSpeed = 50;      // 左右滑动速度

    float scroll_wheel;        // 记录滚轮数值
    float temp_angle;          // 临时存储摄像机的初始角度
    Vector3 temp_vector = new Vector3();

    void Start()
    {
        InitCamera();
    }

    void Update()
    {
        ZoomCamera();
        SwipeScreen();
    }

    void LateUpdate()
    {
        FollowPlayer();
        RotateCamera();
    }

    /// <summary>
    /// 初始化 相机与玩家距离
    /// </summary>
    void InitCamera()
    {
        temp_angle = initial_angle;

        initial_distance = Mathf.Sqrt((initial_angle - minAngle) / Calculate()) + minDistance;

        initial_distance = Mathf.Clamp(initial_distance, minDistance, maxDistance);

    }

    /// <summary>
    /// 相机跟随玩家
    /// </summary>
    void FollowPlayer()
    {
        float upRidus = Mathf.Deg2Rad * initial_angle;
        float flatRidus = Mathf.Deg2Rad * horizontal_angle;

        float x = initial_distance * Mathf.Cos(upRidus) * Mathf.Cos(flatRidus);
        float z = initial_distance * Mathf.Cos(upRidus) * Mathf.Sin(flatRidus);
        float y = initial_distance * Mathf.Sin(upRidus);

        transform.position = Vector3.zero;
        temp_vector.Set(x, y, z);
        temp_vector = temp_vector + target.position;
        transform.position = temp_vector;
        temp_vector.Set(target.position.x, target.position.y + initial_height, target.position.z);

        transform.LookAt(temp_vector);
    }

    /// <summary>
    /// 缩放相机与玩家距离
    /// </summary>
    void ZoomCamera()
    {
        scroll_wheel = GetZoomValue();
        if (Math.Abs(scroll_wheel) > float.Epsilon)
        {
            temp_angle = initial_angle - scroll_wheel * 2 * (maxAngle - minAngle);
            temp_angle = Mathf.Clamp(temp_angle, minAngle, maxAngle);
        }

        if (Math.Abs(temp_angle - initial_angle) > float.Epsilon)
        {
            initial_angle = Mathf.Lerp(initial_angle, temp_angle, Time.deltaTime * 10);

            initial_distance = Mathf.Sqrt((initial_angle - minAngle) / Calculate()) + minDistance;

            initial_distance = Mathf.Clamp(initial_distance, minDistance, maxDistance);
        }
    }

    float Calculate()
    {
        float dis = maxDistance - minDistance;
        float ang = maxAngle - minAngle;
        float line = ang / (dis * dis);
        return line;
    }

    bool is_mouse_press = false;
    Vector2 old_mouse_pos;
    Vector2 new_mouse_pos;
    Vector2 mouse_pos_offset;
    /// <summary>
    /// 滑动屏幕 旋转相机和缩放视野
    /// </summary>
    public void SwipeScreen()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            old_mouse_pos = Vector2.zero;
            is_mouse_press = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouse_pos_offset = Vector2.zero;
            is_mouse_press = false;
        }
        if (!is_mouse_press)
            return;

        new_mouse_pos = Input.mousePosition;
        if (old_mouse_pos != Vector2.zero)
        {
            mouse_pos_offset = old_mouse_pos - new_mouse_pos;
        }
        old_mouse_pos = new_mouse_pos;
    }

    /// <summary>
    /// 获取缩放视野数值  1.鼠标滚轮 2.屏幕上下滑动
    /// </summary>
    /// <returns></returns>
    float GetZoomValue()
    {
        float zoomValue = 0;
        // 使用鼠标滚轮
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            zoomValue = Input.GetAxis("Mouse ScrollWheel");
        }
        else if (mouse_pos_offset != Vector2.zero)
        {
            zoomValue = mouse_pos_offset.y * Time.deltaTime * zoomSpeed * 0.01f;
        }

        return zoomValue;
    }

    float xVelocity = 0;
    /// <summary>
    /// 旋转相机
    /// </summary>
    void RotateCamera()
    {
        horizontal_angle = Mathf.SmoothDamp(horizontal_angle, horizontal_angle + mouse_pos_offset.x * Time.deltaTime * swipeSpeed, ref xVelocity, 0.1f);
    }
}