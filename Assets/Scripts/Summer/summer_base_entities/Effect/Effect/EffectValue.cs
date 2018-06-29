namespace Summer
{
    /// <summary>
    /// 数值修改
    /// </summary>
    public class EffectValue : SEffect
    {
        public EffectValueData _param = new EffectValueData();
        //public E_CharDataValueRegion _region;
        public override void _on_parse()
        {
            /*_region = (E_CharDataValueRegion)cnf.sub_type;
            _param.ParseParam(cnf.param1);*/
        }

        public override bool _on_excute()
        {
            return false;
        }

        public override void _on_reverse()
        {
        }
    }
}
