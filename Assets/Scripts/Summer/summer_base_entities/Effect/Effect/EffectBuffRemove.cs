/*
namespace Summer
{
    /// <summary>
    /// 移除指定IDBuff
    /// </summary>
    public class EffectBuffRemove : SEffect
    {
        public E_EffectBuffTarget target_type = E_EffectBuffTarget.self;
        public int buff_id;

        public override void ResetInfo(System.Object obj)
        {
            BuffLayerMaxEventBuff data = obj as BuffLayerMaxEventBuff;
            if (data == null) return;
            buff_id = data.buff.info.Id;
        }

        public override void _on_parse()
        {
            /*string[] content = StringHelper.SplitString(_cnf.param1);
            buff_id = int.Parse(content[1]);
            target_type = (E_EffectBuffTarget)int.Parse(content[0]);#1#
        }

        public override bool _on_excute()
        {
            /*if (buff_id == 0) return false;
            if (target_type == E_EffectBuffTarget.self)
            {
                BaseEntity.RemoveBuffToTarget(_owner, buff_id);
                return true;
            }
            else
            {
                Error("EffectBuffRemove: 没做");
                return false;
            }#1#
            return false;
        }

        public override void _on_reverse() { }
    }
}
*/
