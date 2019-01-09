
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

using System;

namespace Summer
{
    /// <summary>
    /// UI特性（前缀相同的一组UI）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
    public class UIListAttribute : Attribute
    {
        /// <summary>
        /// 一组UI
        /// </summary>
        /// <param name="prefix">UI名称前缀</param>
        /// <param name="begin">开始索引</param>
        /// <param name="end">结束索引</param>
        public UIListAttribute(string prefix, int begin, int end)
        {
            _namePrefix = prefix;
            Begin = begin;
            End = end;
        }

        /// <summary>
        /// UI名称前缀
        /// </summary>
        protected string _namePrefix;

        public string NamePrefix
        {
            get { return _namePrefix; }
        }

        /// <summary>
        /// 开始位置
        /// </summary>
        public int Begin { get; private set; }

        /// <summary>
        /// 结束位置
        /// </summary>
        public int End { get; private set; }
    }
}