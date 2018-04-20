using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 移动到目标点
    /// </summary>
    public class MoveToTarget : SkillLeafNode
    {
        public const string DES = "移动到目标点";
        public float speed;
        public float finish_distance;                           // 完成的最小距离
        public GameObject _source;
        public GameObject _target;
        public Vector3 _target_pos;

        public override void OnEnter()
        {
            LogEnter();
            //DoMoveToTarget(0);
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override string ToDes() { return DES; }

        public override void OnUpdate(float dt)
        {
            DoMoveToTarget(dt);
        }

        public void DoMoveToTarget(float dt)
        {
            UpdateTargetPos();

            _source.transform.position = Vector3.MoveTowards(_source.transform.position, _target_pos, speed * Time.deltaTime);

            float distance = (_source.transform.position - _target_pos).magnitude;
            if (distance < finish_distance)
            {
                Finish();
            }
        }

        public void UpdateTargetPos()
        {
            //_look_at_pos = _target.transform.position;
        }
    }
}

