using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    public class SelectorConst
    {
        #region 目标类型 敌方/我方/自身/全体

        /// <summary>
        /// 敌方
        /// </summary>
        public const int TARGET_TYPE_ENEMY = 1;
        /// <summary>
        /// 我方
        /// </summary>
        public const int TARGET_TYPE_OWNER = 2;
        /// <summary>
        /// 自身
        /// </summary>
        public const int TARGET_TYPE_SELF = 3;
        /// <summary>
        /// 全体
        /// </summary>
        public const int TARGET_TYPE_ALL = 4;

        #endregion

        #region 作用范围（1:单体  2:后排  3:一列  4:全体  5:T型  6:前排  7:一排  8:周围 ）

        public const int TARGET_AREA_1 = 1;
        public const int TARGET_AREA_2 = 2;
        public const int TARGET_AREA_3 = 3;
        public const int TARGET_AREA_4 = 4;
        public const int TARGET_AREA_5 = 5;
        public const int TARGET_AREA_6 = 6;
        public const int TARGET_AREA_7 = 7;
        public const int TARGET_AREA_8 = 8;

        #endregion

        #region 属性高或低（1、多   2、少  0、无）

        public const int TARGET_ATTRIBUTE_HIGHT = 1;
        public const int TARGET_ATTRIBUTE_LOW = 2;
        public const int TARGET_ATTRIBUTE_ZERO = 0;

        #endregion

        #region 目标职业（1攻、2防、3辅、4控、0无 ）

        public const int TARGET_CAREER_ATTACK = 1;
        public const int TARGET_CAREER_DEF = 2;
        public const int TARGET_CAREER_FUZHU = 3;
        public const int TARGET_CAREER_KONGZHI = 4;
        public const int TARGET_CAREER_NONE = 5;


        #endregion

        #region 目标性别（1:男  2:女 0:无 ）

        public const int TARGET_SEX_MAN = 1;
        public const int TARGET_SEX_WOMEN = 2;
        public const int TARGET_SEX_NONE = 0;

        #endregion

        #region 目标流派（0无  1忍者）

        public const int TARGET_PAI_0 = 0;
        public const int TARGET_PAI_1 = 0;

        #endregion
    }
}
