using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer
{
    /// <summary>
    /// 技能黑箱数据
    /// </summary>
    public interface I_SkillBlackBorad
    {
        /// <summary>
        /// 黑箱数据 Key=string， Value= Object
        /// </summary>
        //T GetBlackBoradValue<T>(string Key, T default_value);
        BlackBorad GetBlackBorad();
    }
}
