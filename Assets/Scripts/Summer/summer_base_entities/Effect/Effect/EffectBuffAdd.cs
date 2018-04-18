

namespace Summer
{
    /// <summary>
    /// 效果为添加Buff
    /// </summary>
    public class EffectBuffAdd : SEffect
    {
        public int buff_id;
        public E_EffectBuffTarget target_type = E_EffectBuffTarget.self;
        public BaseEntity target;
        public override void ResetInfo(System.Object obj)
        {
            TargetEventBuff data = obj as TargetEventBuff;
            if (data == null) return;
            target = data.target;
        }

        public override void _on_parse()
        {
           /* string[] content = StringHelper.SplitString(_cnf.param1);
            buff_id = int.Parse(content[1]);
            target_type = (E_EffectBuffTarget)int.Parse(content[0]);*/
        }

        public override bool _on_excute()
        {
            /*if (target_type == E_EffectBuffTarget.self)
                BaseEntity.AddBuffToTarget(_owner, _owner, buff_id);
            else if(target!=null&& target_type == E_EffectBuffTarget.target)
            {
                BaseEntity.AddBuffToTarget(_owner, target, buff_id);
            }*/
            return false;
        }

        public override void _on_reverse()
        {

        }
    }

}
