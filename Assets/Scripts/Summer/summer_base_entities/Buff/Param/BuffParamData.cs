
/*namespace Summer
{
    public class BuffParamData : I_BuffParam
    {
        private bool _is_positive;                      // 正负
        public E_CharAttributeRegion _region;           // 针对的属性
        public E_CharDataUpdateType _calc_type;         // +1 or +1%
        public int _calc_data;                          // value


        public virtual void ParseParam(EffectCnf cnf)
        {
            _is_positive = int.Parse(cnf.datas[0]) == 0;
            _region = (E_CharAttributeRegion)int.Parse(cnf.datas[1]);
            _calc_type = (E_CharDataUpdateType)int.Parse(cnf.datas[2]);
            _calc_data = int.Parse(cnf.datas[3]);
            if (!_is_positive)
                _calc_data = 0 - _calc_data;
        }
    }

}*/
