using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Summer;

public class TestMovementComponent : MonoBehaviour
{

    public SkillJoystick joystick;
    public EntityMovement entity;
    public AnimationSet anim_set;
    // Use this for initialization
    void Start()
    {
        joystick.on_up_event += OnUpEvent;

    }

    // Update is called once per frame
    void Update()
    {
        if (!joystick.is_touch) return;
        entity.AddDirection(joystick.direction);
        anim_set.Play(AnimationGroup.ClipType.clip_move1);
    }

    public void OnUpEvent()
    {
        entity.RemoveDirection();
        anim_set.Play(AnimationGroup.ClipType.clip_idle1);
    }

}
