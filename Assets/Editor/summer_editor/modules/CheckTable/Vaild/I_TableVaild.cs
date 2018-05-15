
using System.Collections.Generic;


namespace SummerEditor
{
    // 数据校验
    public interface I_TableVaild
    {
        /// <summary>
        /// 被校验的对象数据
        /// </summary>
        /// <param name="infos"></param>
        string CheckVaild(List<string> infos);

        /// <summary>
        /// 校验类型的相关数据
        /// </summary>
        /// <param name="datas"></param>
        void SetVaildData(string[] datas);
    }
}
