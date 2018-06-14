using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Summer
{
    public abstract class SkillBaseSelectTarget
    {
        public virtual BaseEntity SelectTarget()
        {
            return null;
        }

        public virtual Vector3 SelectTargetDir()
        {
            return Vector3.zero;
        }
    }
}
