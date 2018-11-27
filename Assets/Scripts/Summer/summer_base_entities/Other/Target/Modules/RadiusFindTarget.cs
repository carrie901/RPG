
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class RadiusFindTarget : I_FindTarget
    {
        public BaseEntity _self;
        public float _radius;        //距离
        public float _degree;        //角度
        public void FindTarget(List<BaseEntity> targets)
        {
            if (_self == null) return;
            Vector3 direction = _self.EntityController.Direction;
            Vector3 worldPosition = _self.EntityController.WroldPosition;
            float angle = _radius / 2;
            int length = EntitesManager.Instance._entites.Count;
            for (int i = 0; i < length; i++)
            {
                BaseEntity tmpEntity = EntitesManager.Instance._entites[i];
                if (tmpEntity == _self) continue;
                // 2.双方之间的距离
                float distance = MathHelper.Distance2D(tmpEntity.EntityController.WroldPosition, worldPosition);
                // 3.距离大于指定长度
                if (distance > _radius) continue;
                // 4.自己和目标之间的方向
                Vector3 targetDirection = tmpEntity.EntityController.WroldPosition - worldPosition;
                // 5.角度小于指定长度 敌我方向和自身的正前方之间的夹角
                float tmpAngle = MathHelper.GetAngle04(targetDirection, direction);
                if (tmpAngle > angle) continue;
                // 添加到目标
                targets.Add(tmpEntity);
            }
        }

        public void Clear()
        {
            _self = null;
        }
    }
}
