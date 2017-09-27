using System.Collections.Generic;
using Summer;

/// <summary>
/// 以血量换取某一种或者几种属性
/// </summary>
public class BuffExchange : Buff
{
    public BuffParamData _hp_param = new BuffParamData();                   //血量buff
    public List<BuffParamData> _params = new List<BuffParamData>();         //参数
    public List<int> _cumulative_datas = new List<int>();                   //已经累加


    #region test 数据

    public List<PropertyIntParam> _test_datas = new List<PropertyIntParam>();
    public List<int> _tmp_originals = new List<int>();

    public void _test()
    {
        LogManager.Log("兑换Buff,ID:{0}");
        int length = _params.Count;
        for (int i = 0; i < length; i++)
        {
            PropertyIntParam param = _target.FindAttribute(_params[i]._region);
            _test_datas.Add(param);
            _tmp_originals.Add(param.Value);
            LogManager.Log("属性类型{0},原始数据:{1}", _params[i]._region, param.Value);
        }
    }
    #endregion

    #region override  Buff 提供给BuffSst控制
    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        _refresh_param();
    }

    public override void OnAttach(iCharacterBaseController caster, iCharacterBaseController target)
    {
        base.OnAttach(caster, target);
        _test();
    }

    public override void OnDetach()
    {
        _data_reset();
        base.OnDetach();
    }

    public override void Use()
    {
        base.Use();
        data_hp();
        _data_update();
    }
    #endregion

    #region internal (Attach & Detach)

    public override bool AddLayer()
    {
        bool result = base.AddLayer();
        if (result)
            Use();
        return result;
    }

    #endregion

    public void data_hp()
    {
        float tmp_damage = 0;
        BuffHelper.Calc(_target.FindValue(E_CharValueType.hp), ref tmp_damage, _hp_param);
        //DamageInterFace.CalculaterBuffDamage(_target, (int)tmp_damage);
        LogManager.Log("BuffExchange血量消耗:{0}", tmp_damage);
    }

    // 更新数值
    public void _data_update()
    {
        int length = _params.Count;
        for (int i = 0; i < length; i++)
        {
            BuffParamData buff_param = _params[i];
            // 1.找到要更新的属性
            PropertyIntParam param = _target.FindAttribute(buff_param._region);
            // 2.根据层级计算属性具体伤害值（如果直接计算层级最终的结果。那么再重新增加层级的时候，进行reset操作，再添加）
            int result = buff_param._calc_data;
            // 3.按照百分比/固定值更新属性
            BuffHelper.Calc(param, buff_param._calc_type, result);
            // 4.计算累加值
            _cumulative_datas[i] += result;

            LogManager.Log("BuffExchange,属性类型[{3}]累加数据:[{0}],原始数据:[{1}],当前数据:[{2}]", _cumulative_datas[i], _tmp_originals[i], param.Value, buff_param._region);
        }

    }

    public void _data_reset()
    {
        for (int i = 0; i < _params.Count; i++)
        {
            // 1.找到要更新的属性
            PropertyIntParam param = _target.FindAttribute(_params[i]._region);
            // 2.得到之前的累加
            int result = -_cumulative_datas[i];
            // 3.按照百分比/固定值更新属性
            BuffHelper.Calc(param, _params[i]._calc_type, result);
            LogManager.Log("BuffExchange,属性类型[{3}]原始{0},结束{0},累加{2}", _tmp_originals[i], _test_datas[i].Value, _cumulative_datas[i], _params[i]._region);
        }
    }

    public void _refresh_param()
    {
        _hp_param.ParseParam(_conf.param1);
        _parse_param(_conf.param2);
        _parse_param(_conf.param3);
        _parse_param(_conf.param4);
        _parse_param(_conf.param5);
    }

    public void _parse_param(string content)
    {
        if (content.Length > 0)
        {
            BuffParamData param = new BuffParamData();
            param.ParseParam(content);
            _params.Add(param);
            _cumulative_datas.Add(0);
        }
    }

}
