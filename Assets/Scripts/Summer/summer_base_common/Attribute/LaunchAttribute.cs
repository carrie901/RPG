
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
using System.Linq;
using System.Reflection;

namespace Summer
{
    /// <summary>
    /// 游戏启动项
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class LaunchAttribute : Attribute
    {
        public int Index;
        public LaunchAttribute(int index)
        {
            this.Index = index;
        }
    }

    public class LaunchGame
    {
        public void Init()
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var t in types)
            {
                if (!t.IsClass) continue;
                System.Object[] attr = t.GetCustomAttributes(typeof(LaunchAttribute), false);
                if (attr.Length > 0) return;
                UnityEngine.Debug.Log("t.IsClass:" + t.IsClass);
                //Activator.CreateInstance(t);
            }
        }
    }


}

