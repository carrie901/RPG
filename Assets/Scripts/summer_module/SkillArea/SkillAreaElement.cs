using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public abstract class SkillAreaElement : MonoBehaviour
    {
        public E_SkillAreaElement area_element;
        public E_SkillAreaType area_type;

        public float radius = 2.5f;
        protected Transform _target;
        protected Transform _tran_self;
        protected virtual void Awake()
        {
            _tran_self = transform;
        }


        public void SetTarget(Transform target)
        {
            _target = target;
        }
        public abstract void OnUpdate(Vector3 delta_vec);
    }
}

