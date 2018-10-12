
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

    public class BuffLevelLessRelationship : I_BuffRelationship
    {
        public bool ExcuteRelationship(List<BaseBuff> buffList, BuffCnf newCnf)
        {
            bool result = false;
            int length = buffList.Count;
            for (int i = length - 1; i >= 0; i--)
            {
                BaseBuff oldBuff = buffList[i];
                if (!oldBuff.Info.CheckGroupById(newCnf.groupID)) continue;

                int newLv = newCnf.level;
                BuffInfo oldInfo = oldBuff.Info;
                int lvInfo = oldInfo.CheckLevel(newLv);
                if (lvInfo == BuffInfo.LESS) // 新buff等级低
                {
                    result = true;
                    BuffLog.Log("新buff等级低 不处理,新Buff:[{0}],levell:[{1}],老Buff[{2}],level:[{3}]",
                        newCnf.desc, newCnf.level, oldInfo.ToDes(), oldInfo.Level);
                    break;
                }
            }

            return result;
        }
    }
    
    // 叠加/新增/覆盖/没有反应
    // 同一组
    // 1.如果等级比较低。那么直接没有效果
    // 2.如果等级相等
    //      如果可叠加
    //      不可叠加
    // 3.如果比原来的高
    //      覆盖
}

