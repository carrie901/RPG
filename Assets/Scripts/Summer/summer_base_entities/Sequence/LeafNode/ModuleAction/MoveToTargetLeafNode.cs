using UnityEngine;

namespace Summer.Sequence
{
    /// <summary>
    /// 移动到目标点
    /// </summary>
    public class MoveToTargetLeafNode : SequenceLeafNode
    {
        public const string DES = "移动到目标点";
        public float speed;
        public float distance;                           // 完成的最小距离

        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            MoveToTargetPositionData data = EventDataFactory.Pop<MoveToTargetPositionData>();
            data.speed = 10f;
            data.distance = 1;
            RaiseEvent(E_EntityInTrigger.move_to_target_position, data);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public override string ToDes() { return DES; }

        public override void OnUpdate(float dt, BlackBoard blackboard)
        {
            //base.OnUpdate(dt, blackboard);
            DoMoveToTarget(dt);
        }

        public void DoMoveToTarget(float dt)
        {
            UpdateTargetPos();

            /*_source.transform.position = Vector3.MoveTowards(_source.transform.position, _target_pos, speed * Time.deltaTime);

            float distance = (_source.transform.position - _target_pos).magnitude;
            if (distance < distance)
            {
                Finish();
            }*/
        }

        public void UpdateTargetPos()
        {
            //_look_at_pos = _target.transform.position;
        }
    }
}

