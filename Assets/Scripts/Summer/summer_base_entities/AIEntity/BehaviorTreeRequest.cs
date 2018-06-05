using UnityEngine;

namespace Summer.AI
{
    public class BehaviorTreeRequest
    {
        public BehaviorTreeRequest(float time_stamp, Vector3 next_moving_target)
        {
            TimeStamp = time_stamp;
            NextMovingTarget = next_moving_target;
        }
        public float TimeStamp { get; private set; }
        public Vector3 NextMovingTarget { get; private set; }

    }
}

