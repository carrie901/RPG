using System.Collections.Generic;
using Summer;

public class PropertyIntParam
{
    public int _baseNum;                //
    public int _realNum;                //实际
    public int _multiplyPlus;           //+1%
    public int _plus;                    //+1

    public int Value
    {
        get { return _realNum; }
        set { _realNum = value; }
    }

    public void SetBase(int baseNum)
    {
        _baseNum = baseNum;
        _refresh_data();
    }

    public void SetPlus(int plusNum)
    {
        _plus += plusNum;
        _refresh_data();
    }

    public void SetMultiplyPlus(int multiplyPlus)
    {
        _multiplyPlus += multiplyPlus;
        _refresh_data();
    }

    public void _refresh_data()
    {
        _realNum = _baseNum + _plus + (_baseNum * _multiplyPlus) / 100;
    }
}

public class BuffHelper
{
    #region Calc

    public const int PER = 100;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="origin">原始数据</param>
    /// <param name="cur">结果</param>
    /// <param name="type">更新类型</param>
    /// <param name="param">针对百分比是100值</param>
    public static void Calc(float origin, ref float cur, E_DataUpdateType type, int param)
    {
        //这里有个bug，先把cur zero，然后再加乘一些original，则cur不为0，例如带着冰冻的buff，但是装备上加速度，角色还能跑。
        //这是因为缺乏优先级运算的先后顺序导致的
        //目前先不考虑这些东西吧。等后续慢慢上来
        switch (type)
        {
            case E_DataUpdateType.plus:
                {
                    cur += param;
                }
                break;

            case E_DataUpdateType.multiply_plus:
                {
                    cur += (origin * param) / PER;
                }
                break;

            case E_DataUpdateType.zero:
                {
                    cur = 0;
                }
                break;
            default:
                LogManager.Error("Buff Data update E_DataUpdateType Error. type:{0}", type);
                break;
        }
    }

    public static void Calc(PropertyIntParam info, E_DataUpdateType type, int param)
    {
        switch (type)
        {
            case E_DataUpdateType.plus:
                {
                    info.SetPlus(param);
                }
                break;

            case E_DataUpdateType.multiply_plus:
                {
                    info.SetMultiplyPlus(param);
                }
                break;

            case E_DataUpdateType.zero:
                {
                    //cur = 0;
                    LogManager.Error("原来还没有做呀");
                }
                break;
            default:
                LogManager.Error("Buff Data update E_DataUpdateType Error. type:{0}", type);
                break;
        }
    }

    /*
    public static void Calc(float origin, ref float cur, BuffParamData param)
    {
        switch (param._calc_type)
        {
            case E_DataUpdateType.plus:
                {
                    cur += param._calc_data;
                }
                break;

            case E_DataUpdateType.multiply_plus:
                {
                    cur += (origin * param._calc_data) / PER;
                }
                break;

            case E_DataUpdateType.zero:
                {
                    cur = 0;
                }
                break;
            default:
                LogManager.Error("Buff Data update E_DataUpdateType Error. type:{0}", param._calc_type);
                break;
        }
    }
    */

    #endregion

    #region Data

    public static Dictionary<int, BuffCnf> _buff;

    public static BuffCnf FindBuffById(int buffId)
    {
        if (_buff == null)
            _buff = StaticCnf.FindMap<BuffCnf>();
        if (_buff.ContainsKey(buffId))
            return _buff[buffId];
        return null;
    }

    #endregion
}
