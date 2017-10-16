using Summer;


public class PropertyEventBuffData : EventBuffSetData
{
    public E_CharDataUpdateType type;
    public int value;
    public E_CharAttributeType region;
}


/// <summary>
/// 对用户数据的修改，哪个数据，修正多少，百分比修正
/// </summary>
public class BuffDataUpdater : Buff
{

    protected BuffParamData _param = new BuffParamData();


    public int _cumulative_data;

    #region test

    public PropertyIntParam _test_data;
    public int _tmp_original;

    public void _test()
    {
        _test_data = _target.FindAttribute(_param._region);
        _tmp_original = _test_data.Value;
    }
    #endregion

    #region Buff -init/add/remove

    public override void Init(BuffCnf conf)
    {
        base.Init(conf);
        //LogManager.Assert(conf.type == (int)E_BUFF_TYPE.data_updater, "当前为属性更新Buff,他的数据填写错误ID:{0},类型：{1}", conf.id, conf.type);
        _refresh_param();
        _cumulative_data = 0;

    }

    #endregion

    #region override  Buff 提供给BuffSst控制

    public override void OnAttach(BaseEntities caster, BaseEntities target)
    {
        base.OnAttach(caster, target);
        _test();
        LogManager.Log("添加属性Buff,ID:{0},属性类型{1},原始数据:{2}", vbo.Id, vbo.info.sub_type, _tmp_original);
    }

    public override void OnDetach()
    {
        _data_reset();
        LogManager.Log("移除属性Buff,原始{0},结束{0},累加{2}", _tmp_original, _test_data.Value, _cumulative_data);
        LogManager.Assert(_tmp_original == _test_data.Value, "属性更新结束,结果不相等:{0}", vbo.ToString());
        base.OnDetach();
    }

    public override void Use()
    {
        base.Use();
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

    // 解析数据
    public void _refresh_param()
    {
        _param.ParseParam(vbo.info.param1);
    }

    // 更新数值
    public void _data_update()
    {
        PropertyIntParam param = _target.FindAttribute(_param._region);

        // 1.生成参数
        PropertyEventBuffData property_data = new PropertyEventBuffData()
        {
            type = _param._calc_type,
            value = _param._calc_data,
            region = _param._region,
        };

        // 2.触发回调
        _target.RaiseEvent(E_BuffTrigger.on_update_property, property_data);

        // 3.计算累加值
        _cumulative_data += _param._calc_data;

        LogManager.Log("属性更新,累加数据:{0},原始数据:{1},当前数据:{2}", _cumulative_data, _tmp_original, param.Value);
    }

    // 数值回复
    public void _data_reset()
    {
        // 1.生成参数
        PropertyEventBuffData property_data = new PropertyEventBuffData()
        {
            type = _param._calc_type,
            region = _param._region,        //得到之前的累加
            value = -_cumulative_data,
        };

        // 2.触发回调
        _target.RaiseEvent(E_BuffTrigger.on_update_property, property_data);
    }

    public void _update_damage_func()
    {
        //伤害的计算方式和_calc的计算方式类型
        //在角色身上挂载一个角色额外伤害类，记录角色额外添加的属性
    }
}
