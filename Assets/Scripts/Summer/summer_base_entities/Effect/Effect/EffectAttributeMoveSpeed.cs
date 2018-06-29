namespace Summer
{
    /// <summary>
    /// 属性移动速度的修改
    /// </summary>
    public class EffectAttributeMoveSpeed : EffectAttribute
    {

        public EffectAttributeParam _param = new EffectAttributeParam();
        public float _cumulative_data;

        public override string GetValueText()
        {
            return _param.GetValueText();
        }

        public override void _on_parse()
        {
            _param.ParseParam(_cnf);
        }

        public override bool _on_excute()
        {

            /*float old_speed = _owner.speed;
            float new_speed = 0;

            if (_param._calc_type == E_DataUpdateType.multiply_plus)
            {
                new_speed = _owner.ChangeMoveSpeedMultiplyPlus(_param._calc_data);
            }
            else if (_param._calc_type == E_DataUpdateType.plus)
            {
                new_speed = _owner.ChangeeMoveSpeed(_param._calc_data / 100f);
            }

            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.buff_effect_excute, this);
            //_send_event_data(old_speed, new_speed);
            Log("Effect Excute---> attribute:[{0}],before:[{1}],after[{2}]", _param._region, old_speed, new_speed);
            _cumulative_data += _param._calc_data;
*/
            return false;
        }

        public override void _on_reverse()
        {
            /*float old_speed = _owner.speed;
            if (_param._calc_type == E_DataUpdateType.multiply_plus)
            {
                _owner.ChangeMoveSpeedMultiplyPlus(-_cumulative_data);
            }
            else if (_param._calc_type == E_DataUpdateType.plus)
            {
                _owner.ChangeeMoveSpeed(-(_cumulative_data / 100f));
            }

            Log("Effect Reverse---> attribute:[{0}],before:[{1}],after[{2}]", _param._region, old_speed, _owner.speed);*/
        }

        public void _send_event_data(float old_value, float new_value)
        {
            //float end_value = property_value.Value;
            EffectAttributeEvent event_data = EventDataFactory.Pop<EffectAttributeEvent>();
            event_data._region = _param._region;
            event_data.target = _owner;
            event_data.data = (new_value - old_value);
            // CharacterEventManager.RaiseEvent(E_BattleCharacter.attribute, event_data);
            // EventDataFactory.Push(event_data);
        }
    }

}

