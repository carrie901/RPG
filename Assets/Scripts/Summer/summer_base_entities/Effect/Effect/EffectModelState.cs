
//=============================================================================
// Author : mashao
// CreateTime : 2018-1-10 19:38:7
// FileName : EffectModelState.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 模型的修改
    /// </summary>
    public abstract class EffectModelState : SEffect
    {

        public override void _on_parse()
        {
            
        }

        public override bool _on_excute()
        {
            _on_change_state();
            return false;
        }

        public override void _on_reverse()
        {
            _on_on_reverse_state();
        }

        public abstract void _on_change_state();

        public abstract void _on_on_reverse_state();
    }
}
