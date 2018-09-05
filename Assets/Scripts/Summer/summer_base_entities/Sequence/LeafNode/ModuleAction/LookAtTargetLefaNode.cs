using UnityEngine;

namespace Summer.Sequence
{
    /// <summary>
    /// 朝向目标
    /// </summary>
    public class LookAtTargetLefaNode : SequenceLeafNode
    {
        public const string DES = "朝向目标";
        public bool every_frame;

        public GameObject _source;
        public GameObject _target;
        public Vector3 _look_at_pos;

        public bool debug = false;
        public Color debug_line_color = Color.yellow;
        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            DoLookAt();
            if (!every_frame)
            {
                Finish();
            }
        }

        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
        }

        public override void OnUpdate(float dt, BlackBoard blackboard)
        {
            //base.OnUpdate(dt);
            UpdateLookAtPosition();
            _source.transform.LookAt(_look_at_pos, Vector3.up);

            if (debug)
            {
                Debug.DrawLine(_source.transform.position, _look_at_pos, debug_line_color);
            }
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public void DoLookAt()
        {
            if (debug)
            {
                //Debug.DrawLine(go.transform.position, lookAtPos, debugLineColor.Value);
            }
        }

        // 更新目标
        public void UpdateLookAtPosition()
        {
            _look_at_pos = _target.transform.position;
        }

        public override string ToDes() { return DES; }
    }
}

