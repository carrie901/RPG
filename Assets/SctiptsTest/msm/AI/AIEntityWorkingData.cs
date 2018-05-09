using Summer.AI;
using UnityEngine;

namespace Summer.Test
{
    public class AIEntityWorkingData : TbWorkingData
    {
        public AIEntity Entity { get; set; }
        public Transform EntityTrans { get; set; }
        public Animator EntityAnimator { get; set; }
        public float GameTime { get; set; }
        public float DeltaTime { get; set; }
    }
}
