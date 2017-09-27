using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class LookAtTarget : ASkillAction
    {
        public bool every_frame;

        public GameObject _source;
        public GameObject _target;
        public Vector3 _look_at_pos;

        public bool debug = false;
        public Color debug_line_color = Color.yellow;
        public override void OnEnter()
        {
            DoLookAt();
            if (!every_frame)
            {
                Finish();
            }
        }

        public override void OnExit()
        {
            DoLookAt();
        }

        public override void OnUpdate(float dt)
        {
            _look_at_pos = _target.transform.position;
            _source.transform.LookAt(_look_at_pos, Vector3.up);

            if (debug)
            {
                Debug.DrawLine(_source.transform.position, _look_at_pos, debug_line_color);
            }
        }

        public void DoLookAt()
        {

        }
    }
}

