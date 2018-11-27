using UnityEngine;
using System.Collections.Generic;
namespace Summer.Sequence
{
    /// <summary>
    /// 查找目标 
    /// TODO 通过依赖连来查找目标  范围/敌友/等等其他条件 一次次把结果传递
    /// </summary>
    public class FindTargetLeafNode : SequenceLeafNode
    {
        #region 属性

        public const string RADIUS = "Radius";
        public const string DEGREE = "Degree";

        public const string DES = "==查找目标==";
        //TODO 希望能通过抽象来描述查找目标
        public float _radius;        //距离
        public float _degree;        //角度

        #endregion

        #region override

        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            EntityFindTargetData data = EventDataFactory.Pop<EntityFindTargetData>();
            data.degree = _degree;
            data.radius = _radius;
            _find_target(blackboard as EntityBlackBoard, data);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
        }
        public override void SetConfigInfo(EdNode cnf)
        {
            _radius = cnf.GetAttribute(RADIUS).ToFloat();
            _degree = cnf.GetAttribute(DEGREE).ToFloat();
        }
        public override string ToDes()
        {
            return DES;
        }

        #endregion

        #region private

        public List<BaseEntity> _targetList;
        public void _find_target(EntityBlackBoard blackboard, EntityFindTargetData data)
        {
            if (data == null || blackboard == null) return;

            float angle = data.degree / 2;
            BaseEntity entity = blackboard.entity;
            // 1.得到自身的方向和世界坐标
            Vector3 direction = entity.EntityController.Direction;
            Vector3 worldPosition = entity.EntityController.WroldPosition;
            _targetList = blackboard.GetValue<List<BaseEntity>>(EntityBlackBoardConst.TARGET_LIST);
            int length = EntitesManager.Instance._entites.Count;
            for (int i = 0; i < length; i++)
            {
                BaseEntity tmpEntity = EntitesManager.Instance._entites[i];
                if (tmpEntity == entity) continue;
                // 2.双方之间的距离
                float distance = MathHelper.Distance2D(tmpEntity.EntityController.WroldPosition, worldPosition);
                // 3.距离大于指定长度
                if (distance > data.radius) continue;
                // 4.自己和目标之间的方向
                Vector3 targetDirection = tmpEntity.EntityController.WroldPosition - worldPosition;
                // 5.角度小于指定长度 敌我方向和自身的正前方之间的夹角
                float tmpAngle = MathHelper.GetAngle04(targetDirection, direction);
                if (tmpAngle > angle) continue;
                // 添加到目标
                _targetList.Add(tmpEntity);
                ActionLog.Log("找到目标:{0}", tmpEntity.EntityController.gameObject.name);
            }
        }

        #endregion
    }

}
