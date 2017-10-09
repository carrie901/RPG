using UnityEngine;
using System.Collections;
using Summer;

public class BuffHelper
{
    public const int PER = 100;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="origin">原始数据</param>
    /// <param name="cur">结果</param>
    /// <param name="type">更新类型</param>
    /// <param name="param">针对百分比是100值</param>
    public static void Calc(float origin, ref float cur, E_CharDataUpdateType type, int param)
    {
        //这里有个bug，先把cur zero，然后再加乘一些original，则cur不为0，例如带着冰冻的buff，但是装备上加速度，角色还能跑。
        //这是因为缺乏优先级运算的先后顺序导致的
        //目前先不考虑这些东西吧。等后续慢慢上来
        switch (type)
        {
            case E_CharDataUpdateType.plus:
                {
                    cur += param;
                }
                break;

            case E_CharDataUpdateType.multiply_plus:
                {
                    cur += (origin * param) / PER;
                }
                break;

            case E_CharDataUpdateType.zero:
                {
                    cur = 0;
                }
                break;
            default:
                LogManager.Error("Buff Data update E_CharDataUpdateType Error. type:{0}", type);
                break;
        }
    }

    public static void Calc(PropertyIntParam info, E_CharDataUpdateType type, int param)
    {
        switch (type)
        {
            case E_CharDataUpdateType.plus:
                {
                    info.SetPlus(param);
                }
                break;

            case E_CharDataUpdateType.multiply_plus:
                {
                    info.SetMultiplyPlus(param);
                }
                break;

            case E_CharDataUpdateType.zero:
                {
                    //cur = 0;
                    LogManager.Error("原来还没有做呀");
                }
                break;
            default:
                LogManager.Error("Buff Data update E_CharDataUpdateType Error. type:{0}", type);
                break;
        }
    }

    public static void Calc(float origin, ref float cur, BuffParamData param)
    {
        switch (param._calc_type)
        {
            case E_CharDataUpdateType.plus:
                {
                    cur += param._calc_data;
                }
                break;

            case E_CharDataUpdateType.multiply_plus:
                {
                    cur += (origin * param._calc_data) / PER;
                }
                break;

            case E_CharDataUpdateType.zero:
                {
                    cur = 0;
                }
                break;
            default:
                LogManager.Error("Buff Data update E_CharDataUpdateType Error. type:{0}", param._calc_type);
                break;
        }
    }

    public static void Calc(float origin, ref float cur, BuffParamValueData param)
    {
        switch (param._calc_type)
        {
            case E_CharDataUpdateType.plus:
                {
                    cur += param._calc_data;
                }
                break;

            case E_CharDataUpdateType.multiply_plus:
                {
                    cur += (origin * param._calc_data) / PER;
                }
                break;

            case E_CharDataUpdateType.zero:
                {
                    cur = 0;
                }
                break;
            default:
                LogManager.Error("Buff Data update E_CharDataUpdateType Error. type:{0}", param._calc_type);
                break;
        }
    }


}
