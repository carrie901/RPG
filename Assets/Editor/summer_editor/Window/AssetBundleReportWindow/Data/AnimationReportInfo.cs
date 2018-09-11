
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

namespace SummerEditor
{
    public class AnimationReportInfo
    {
        public string AnimationName { get; set; }
        public int MemSize { get; set; }

        public string MemSizeT { get; set; }
        public int BeRefCount { get; set; }
        public List<string> BeRefs = new List<string>();

        public void SetInfo(List<string> content)
        {
            AnimationName = content[0];
            MemSize = int.Parse((content[1]));
            MemSizeT = EMemorySizeHelper.GetKb(MemSize);
            BeRefCount = content.Count - 2;
            BeRefs.Clear();
            for (int i = 2; i < content.Count; i++)
            {
                BeRefs.Add(content[i]);
            }
        }
    }

    public class TmpStringInfo
    {
        public string _param1;
        public string _param2;
        public string _param3;
        public string _param4;
        public string _param5;
    }

}

