using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Summer;
using UnityEngine.UI;

public class TestMovementComponent : MonoBehaviour
{

    public SkillJoystick joystick;
    public EntityMovement movement;
    public BaseEntity entity;
    public Button button;

    public GameObject follow;

    public bool joystick_is_moveed = false;
    public bool key_board_is_input = false;

    // Use this for initialization
    void Start()
    {
        Application.targetFrameRate = 30;
        joystick.on_up_event += OnUpEvent;
        button.onClick.AddListener(OnClick);
    }
    public Vector3 move_direction = Vector3.zero;
    // Update is called once per frame
    //void FixedUpdate()
    void LateUpdate()
    {
        /*if (!joystick.is_touch) return;*/
        Vector2 direction = Vector2.zero;
        key_board_is_input = false;
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1.0f;
            key_board_is_input = true;
        }


        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1.0f;
            key_board_is_input = true;
        }


        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1.0f;
            key_board_is_input = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1.0f;
            key_board_is_input = true;
        }

        if (!key_board_is_input) return;
        if (entity == null)
            entity = EntitesManager.Instance.manual;


        //follow.transform.localPosition = new Vector3(direction.x, 0, direction.y);

        entity.ReceiveCommandMove(direction);
    }

    public void OnUpEvent()
    {
        //movement.RemoveDirection();
    }

    public void OnClick()
    {
        EntitesManager.Instance.manual.CastSkill();
    }

    void OnDrawGizmos()
    {
        if (follow == null) return;
        Vector3 follow_pos = follow.transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(follow_pos, 0.25f);

        /*Gizmos.color = Color.white;
        Gizmos.DrawSphere(_centerPos, 0.1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_centerPos, _followPos);*/
    }
}


public class PlayerInput
{
    public Vector2 direction;


}