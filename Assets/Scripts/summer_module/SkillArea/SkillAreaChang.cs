using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillAreaChang : SkillAreaElement
    {
        private float _length = 2;

        public Transform body;
        public Transform arrow;

        protected override void Awake()
        {
            base.Awake();
            _tran_self = gameObject.transform;
            ResetLength(2.5f);
        }


        public override void OnUpdate(Vector3 delta_vec)
        {
            Vector3 target_dir = Quaternion.Euler(0, 1f, 0) * delta_vec;
            _tran_self.LookAt(target_dir + _target.position);

        }

        public void ResetLength(float length)
        {
            _length = length;
            _reset_length();
        }

        public void _reset_length()
        {
            float body_length = _length - 0.5f;
            body.localPosition = new Vector3(0, 0, body_length / 2);
            body.localScale = new Vector3(1, body_length, 1);
            arrow.localPosition = new Vector3(0, 0, _length);
        }
    }
}

