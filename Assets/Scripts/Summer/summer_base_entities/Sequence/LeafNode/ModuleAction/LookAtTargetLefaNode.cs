using UnityEngine;

namespace Summer.Sequence
{
    /// <summary>
    /// 朝向目标
    /// </summary>
    public class LookAtTargetLefaNode : SequenceLeafNode
    {
        public const string DES = "朝向目标";
        public bool _everyFrame;

        public GameObject _source;
        public GameObject _target;
        public Vector3 _lookAtPos;

        public bool _debug = false;
        public Color _debugLineColor = Color.yellow;
        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            DoLookAt();
            if (!_everyFrame)
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
            _source.transform.LookAt(_lookAtPos, Vector3.up);

            if (_debug)
            {
                Debug.DrawLine(_source.transform.position, _lookAtPos, _debugLineColor);
            }
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public void DoLookAt()
        {
            if (_debug)
            {
                //Debug.DrawLine(go.transform.position, lookAtPos, debugLineColor.Value);
            }
        }

        // 更新目标
        public void UpdateLookAtPosition()
        {
            _lookAtPos = _target.transform.position;
        }

        public override string ToDes() { return DES; }
    }
}

