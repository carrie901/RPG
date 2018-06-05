using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Summer.AI
{
    /// <summary>
    /// AI 感知器
    /// 可以理解为具体的获取数据的方法，可以定义一个感知器的接口类
    /// 感知器也包括个体感知器和全局感知器
    /// </summary>
    public interface I_AISensor
    {
        void Update();
    }

    /// <summary>
    /// 更新决策层（做什么）- What to do
    /// 还有一种特殊的模块是属于这一层，那就是玩家输入，玩家的输入说起来，其实也是一种决策
    /// 只是这个决策是通过玩家来做出的。行为树就很容易处理这个情况，将玩家输入和AI决策可以融合成一体让所有的智能体共用：
    /// https://blog.csdn.net/kenkao/article/details/52610397
    /// </summary>
    public class AISensorManager
    {
        public static List<I_AISensor> _sensors = new List<I_AISensor>();

        public static void Update()
        {
            int length = _sensors.Count;
            for (int i = 0; i < length; i++)
            {
                _sensors[i].Update();
            }
        }

        public static void RegisterSensor(I_AISensor sensor)
        {
            if (_sensors.Contains(sensor))
            {
                LogManager.Error("感知器已经注册");
            }
            else
            {
                _sensors.Add(sensor);
            }
        }

        public static void UnRegisterSensor(I_AISensor sensor)
        {
            if (_sensors.Contains(sensor))
            {
                _sensors.Remove(sensor);
            }
            else
            {
                LogManager.Error("找不到对应的感知器");
            }
        }
    }


}
