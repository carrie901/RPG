namespace Summer
{

    public class EffectPeerLess : SEffect
    {
        public int data;
        public override void _on_parse()
        {
            data = int.Parse(_cnf.datas[0]);
        }

        public override bool _on_excute()
        {
           /* // 最大值
            float origin = _owner.peerLess.FindMax();
            float cur = 0;
            // 当前值
            float old_value = _owner.peerLess.GetValue();
            // 通过参数计算数值
            BuffHelper.Calc(origin, ref cur, E_CharDataUpdateType.plus, data);
            // 计算数值
            if (cur > 0)
                _owner.peerLess.AddAnger(cur);
            else
                _owner.peerLess.ReduceAnger(cur);
            // 新的值
            float new_value = _owner.peerLess.GetValue();

            _send_event_data(old_value, new_value);
            Log("Effect Excute---> 无双值更新,before:[{0}],after[{1}]", old_value, new_value);*/
            return false;
        }

        public override void _on_reverse()
        {

        }

        public void _send_event_data(float old_value, float new_value)
        {
            /*float change_value = (new_value - old_value);
            if (change_value >= 1 || change_value <= -1)
            {
                EffectValueEvent event_data = EventDataFactory.Pop<EffectValueEvent>();
                event_data.data = change_value;
                event_data.target = _owner;
                CharacterEventManager.RaiseEvent(E_BattleCharacter.wushuang, event_data);
                EventDataFactory.Push(event_data);
            }*/
        }
    }

    /* // 参数不一样
    public class EffectPeerLess : SEffect
    {
        public EffectValueData _param = new EffectValueData();
        public override void _on_parse()
        {
            _param.ParseParam(_cnf);
        }

        public override bool _on_excute()
        {
            // 最大值
            float origin = _owner.peerLess.FindMax();
            float cur = 0;
            // 当前值
            float old_value = _owner.peerLess.GetValue();
            // 通过参数计算数值
            BuffHelper.Calc(origin, ref cur, _param._calc_type, _param._calc_data);
            // 计算数值
            if (cur > 0)
                _owner.peerLess.AddAnger(cur);
            else
                _owner.peerLess.ReduceAnger(cur);
            // 新的值
            float new_value = _owner.peerLess.GetValue();

            _send_event_data(old_value, new_value);
            Log("Effect Excute---> 无双值更新,before:[{1}],after[{2}]", old_value, new_value);
            return false;
        }

        public override void _on_reverse()
        {

        }

        public void _send_event_data(float old_value, float new_value)
        {
            float change_value = (new_value - old_value);
            if (change_value >= 1 || change_value <= -1)
            {
                EffectValueEvent event_data = EventDataFactory.Pop<EffectValueEvent>();
                event_data.data = change_value;
                event_data.target = _owner;
                CharacterEventManager.RaiseEvent(E_BattleCharacter.wushuang, event_data);
                EventDataFactory.Push(event_data);
            }

}*/
    

}
