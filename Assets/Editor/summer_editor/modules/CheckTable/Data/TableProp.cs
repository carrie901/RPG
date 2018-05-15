
using System.Collections.Generic;

namespace SummerEditor
{
    /// <summary>
    /// 属性 每一列的文本描述/属性名称 Key值，有效监测
    /// 包含具体数据
    /// </summary>
    public class TableProp
    {
        public string des;                              // 描述
        public string prop;                             // 属性名称
        public string key;                              // key值 也就是属性key
        public string vaild;                            // 有效监测

        public List<string> data = new List<string>();

        public void AddData(string text)
        {
            data.Add(text);
        }

        // 检测数据是否合格
        public string CheckVaild()
        {
            if (string.IsNullOrEmpty(vaild)) return string.Empty;
            I_TableVaild table_vaild = TableVaildFactory.Create(vaild);
            if (table_vaild == null)
                return string.Format(TableVaildConst.no_valid, vaild);
            string mess = table_vaild.CheckVaild(data);

            if (string.IsNullOrEmpty(mess))
                return mess;
            else
            {
                return des + "," + prop + "," + key + "," + vaild + "/n" + mess;
            }
        }

        // 是否存在这个数据，被外部印证是否拥有这个数据
        public bool IsExist(string text)
        {
            int length = data.Count;
            for (int i = 0; i < length; i++)
            {
                if (data[i] == text)
                    return true;
            }
            return false;
        }
    }

}
