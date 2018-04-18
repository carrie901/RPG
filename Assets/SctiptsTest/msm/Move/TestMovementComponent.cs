using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Summer;
using UnityEngine.UI;

public class TestMovementComponent : MonoBehaviour
{

    public SkillJoystick joystick;
    public EntityMovement movement;
    public Button button;
    // Use this for initialization
    void Start()
    {
        joystick.on_up_event += OnUpEvent;
        button.onClick.AddListener(OnClick);
    }


    // Update is called once per frame
    void Update()
    {
        if (!joystick.is_touch) return;
        if (movement == null)
            movement = EntitesManager.Instance.manual.EntityController.movement;
        movement.AddDirection(joystick.direction);
    }

    public void OnUpEvent()
    {
        movement.RemoveDirection();
    }

    public void OnClick()
    {
        EntitesManager.Instance.manual.CastSkill();
    }
}
