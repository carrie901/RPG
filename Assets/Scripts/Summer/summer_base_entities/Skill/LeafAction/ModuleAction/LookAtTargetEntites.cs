using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class LookAtTarget : SkillNodeAction
    {
        public const string DES = "朝向目标";
        public bool every_frame;

        public GameObject _source;
        public GameObject _target;
        public Vector3 _look_at_pos;

        public bool debug = false;
        public Color debug_line_color = Color.yellow;
        public override void OnEnter()
        {
            LogEnter();
            DoLookAt();
            if (!every_frame)
            {
                Finish();
            }
        }

        public override void OnExit()
        {
            LogExit();
            DoLookAt();
        }

        public override void OnUpdate(float dt)
        {
            //base.OnUpdate(dt);
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

        public override string ToDes() { return DES; }
    }
}

