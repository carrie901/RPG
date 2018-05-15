using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 数值大于一定值
    /// </summary>
    public class SizeTextVaild : I_TableVaild
    {
        public int value;                                       // 数据
        public string compare;                                  // 比较的符号
        public string CheckVaild(List<string> infos)
        {
            return string.Empty;
        }
        public void SetVaildData(string[] datas)
        {

        }
    }
}
