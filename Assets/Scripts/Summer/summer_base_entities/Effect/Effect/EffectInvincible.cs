

namespace Summer
{
    /// <summary>
    /// 无敌效果
    /// </summary>
    public class EffectInvincible : EffectState
    {
        public override void _on_change_state()
        {
            /*Log("状态改变--->开启超人模式");
            _owner.m_isAvalidTarget = true;*/
        }

        public override void _on_on_reverse_state()
        {
            /*Log("状态还原--->取消超人模式");
            _owner.m_isAvalidTarget = false;*/
        }
    }

}
