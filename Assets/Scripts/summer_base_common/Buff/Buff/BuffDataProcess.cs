
/// <summary>
/// 数值更新过程 重复性质的代码，后期合并或者拆
/// </summary>
public class BuffDataProcessValue : Buff
{
    public float _cumulative_data;                  //累加
    protected BuffParamValueData _param = new BuffParamValueData();
    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        _refresh_param();
    }

    public override void Use()
    {
        base.Use();
        _cumulative_data += _param._calc_data;
    }

    public void _refresh_param()
    {
        _param.ParseParam(_conf.param1);
    }
}

/// <summary>
/// 属性值更新过程
/// </summary>
public class BuffDataUpdateProcess : Buff
{
    protected BuffParamData _param = new BuffParamData();

    public float _cumulative_data;                  //累加

    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        _refresh_param();
    }

    public override void Use()
    {
        base.Use();
        _cumulative_data += _param._calc_data;
    }

    public void _refresh_param()
    {
        _param.ParseParam(_conf.param1);
    }
}
