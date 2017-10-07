namespace Summer
{
    public class BuffParamData : I_BuffParam
    {
        public E_CharAttributeType _region;                //针对的属性
        public E_CharDataUpdateType _calc_type;         //+1 or +1%
        public int _calc_data;                          //value
        private bool _is_positive;                       //正负

        public virtual void ParseParam(string param)
        {
            string[] contents = StringHelper.SplitString(param, "/");
            _is_positive = int.Parse(contents[0]) == 0;
            _region = (E_CharAttributeType)int.Parse(contents[1]);
            _calc_type = (E_CharDataUpdateType)int.Parse(contents[2]);
            _calc_data = int.Parse(contents[3]);
            if (!_is_positive)
                _calc_data = 0 - _calc_data;
        }
    }
}

