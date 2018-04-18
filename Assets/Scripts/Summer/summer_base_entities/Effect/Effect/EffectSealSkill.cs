namespace Summer
{
    /// <summary>
    /// 封印技能
    /// </summary>
    public class EffectSealSkill : EffectState
    {
        public override void _on_change_state()
        {
            /*_owner.inSilence = true;
            Log("状态改变--->封印技能");*/
        }

        public override void _on_on_reverse_state()
        {
           /* _owner.inSilence = false;
            Log("状态还原--->解除技能的封印");*/
        }
    }
}

