

namespace Summer
{

    /// <summary>
    /// 数值更新过程 重复性质的代码，后期合并或者拆
    /// </summary>
    public class BuffDataProcessValue : Buff
    {
        public float _cumulative_data;                  //累加
        public override void Init(BuffCnf conf)
        {
            base.Init(conf);
            _param = new BuffParamValueData();
            _refresh_param();
        }

        public override void Use()
        {
            base.Use();
            if (_param is BuffParamValueData)
            {
                _cumulative_data += (_param as BuffParamValueData)._calc_data;
            }
        }

        public void _refresh_param()
        {
            _param.ParseParam(vbo.info.param1);
        }
    }

    /// <summary>
    /// 属性值更新过程
    /// </summary>
    public class BuffDataUpdateProcess : Buff
    {
        public float _cumulative_data;                  //累加

        public override void Init(BuffCnf conf)
        {
            base.Init(conf);
            _param = new BuffParamData();
            _refresh_param();
        }

        public override void Use()
        {
            base.Use();
            if (_param is BuffParamData)
            {
                _cumulative_data += (_param as BuffParamData)._calc_data;
            }
        }

        public void _refresh_param()
        {
            _param.ParseParam(vbo.info.param1);
        }
    }

}
