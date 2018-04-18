namespace Summer
{
    /// <summary>
    /// 数值属性
    /// </summary>
    public class EffectValueData : I_EffectParam
    {
        public E_CharDataUpdateType _calc_type;         // +1 or +1%
        public int _calc_data;                          // value



        public void ParseParam(EffCnf cnf)
        {
            _calc_type = (E_CharDataUpdateType)int.Parse(cnf.datas[0]);
            _calc_data = int.Parse(cnf.datas[1]);
        }

        public string GetValueText()
        {
            return string.Empty;
        }
    }
}

