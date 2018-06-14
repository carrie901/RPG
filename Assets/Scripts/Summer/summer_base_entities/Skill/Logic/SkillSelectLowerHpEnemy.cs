using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Summer
{
    public class SkillSelectLowerHpEnemy: SkillBaseSelectTarget
    {
        public override BaseEntity SelectTarget()
        {
            return null;
        }

        public override Vector3 SelectTargetDir()
        {
            return Vector3.zero;
        }
    }
}
