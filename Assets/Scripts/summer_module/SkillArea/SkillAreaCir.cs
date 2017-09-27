using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillAreaCir : SkillAreaElement
    {
        public float area_length;
        public Transform body;

        protected override void Awake()
        {
            base.Awake();
            _tran_self = gameObject.transform;
            ResetLength(1f);
        }

        public override void OnUpdate(Vector3 delta_vec)
        {
            Vector3 target_dir = radius * delta_vec;
            _tran_self.position = target_dir + _target.position;
        }

        public void ResetLength(float length)
        {
            area_length = length;
            _reset_length();
        }

        public void _reset_length()
        {
            body.localScale = new Vector3(area_length, 1, area_length);
        }
    }

}
