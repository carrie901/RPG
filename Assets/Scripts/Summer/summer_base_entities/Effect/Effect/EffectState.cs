/*
namespace Summer
{
    /// <summary>
    /// 状态改变
    /// </summary>
    public abstract class EffectState : SEffect
    {
        // public EffectStateData _param = new EffectStateData();
        // public E_EffectState _state;
        public bool _rate_result;
        public override void _on_parse()
        {
            //_state = (E_EffectState)cnf.sub_type;
            //_param.ParseParam(cnf.param1);
        }

        public override bool _on_excute()
        {
            // _rate_result = _param.Random();
            _on_change_state();
            return false;
        }

        public override void _on_reverse()
        {
            if (_rate_result)
                _on_on_reverse_state();
        }

        public abstract void _on_change_state();
        public abstract void _on_on_reverse_state();
    }

}
*/
