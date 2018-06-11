using System.Collections;
using Summer.AI;
using Summer.Test;
using UnityEngine;

namespace Summer.AI
{
    public class BtEntityAi : I_Update
    {
        //-----------------------------------------------
        public const string BBKEY_NEXTMOVINGPOSITION = "NextMovingPosition";
        //-----------------------------------------------
        protected BtAction _behavior_tree;
        protected AIEntityWorkingData _behavior_working_data;
        protected BtBlackBoard _blackboard;

        protected BehaviorTreeRequest _current_request;
        protected BehaviorTreeRequest _next_request;

        protected GameObject _target_dummy_object;

        protected float _nexttime_to_gen_moving_target;

        public BaseEntity BaseEntity { get; private set; }
        public BtEntityAi(BaseEntity entity)
        {
            BaseEntity = entity;
            Init();
        }


        public BtEntityAi Init()
        {
            _behavior_tree = BtEntityFactory.GetBehaviorTreeDemo1();

            _behavior_working_data = new AIEntityWorkingData();
            _behavior_working_data.EntityAi = this;
            _blackboard = new BtBlackBoard();

            _nexttime_to_gen_moving_target = 0;
            return this;
        }
        public T GetBbValue<T>(string key, T default_value)
        {
            return _blackboard.GetValue(key, default_value);
        }
        public int UpdateAi(float game_time, float delta_time)
        {
            if (game_time > _nexttime_to_gen_moving_target)
            {
                _next_request = new BehaviorTreeRequest(game_time, new Vector3(Random.Range(-15f, 15f), 0, Random.Range(-15f, 15f)));
                _nexttime_to_gen_moving_target = game_time + 20f + Random.Range(-5f, 5f);
            }
            return 0;
        }
        public int UpdateReqeust(float game_time, float delta_time)
        {
            if (_next_request != _current_request)
            {
                /* //reset bev tree
                 _behavior_tree.Transition(_behavior_working_data);
                 //assign to current
                 _current_request = _next_request;

                 //reposition and add a little offset
                 Vector3 targetPos = _current_request.NextMovingTarget + TMathUtils.GetDirection2D(_current_request.NextMovingTarget, transform.position) * 0.2f;
                 Vector3 startPos = new Vector3(targetPos.x, -1.4f, targetPos.z);
                 _target_dummy_object.transform.position = startPos;
                 LeanTween.move(_target_dummy_object, targetPos, 1f);*/
            }
            return 0;
        }
        public int UpdateBehavior(float game_time, float delta_time)
        {
            if (_current_request == null)
            {
                return 0;
            }
            //update working data
            //_behavior_working_data.EntityAnimator.speed = GameTimer.Instance.timeScale;
            _behavior_working_data.GameTime = game_time;
            _behavior_working_data.DeltaTime = delta_time;

            //test bb usage
            _blackboard.SetValue(BBKEY_NEXTMOVINGPOSITION, _current_request.NextMovingTarget);

            if (_behavior_tree.Evaluate(_behavior_working_data))
            {
                _behavior_tree.Update(_behavior_working_data);
            }
            else
            {
                _behavior_tree.Transition(_behavior_working_data);
            }
            return 0;
        }

        public void OnUpdate(float dt)
        {

        }
    }

}

