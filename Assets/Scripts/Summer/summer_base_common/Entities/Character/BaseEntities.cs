
namespace Summer
{
    /// <summary>
    /// 先把BaseEntities当做BaseCharacter用
    /// </summary>
    public class BaseEntities : I_EntitiesBuff, I_CharacterProperty
    {
        public BuffContainer _buff_container;                               // Buff容器
        public CharId char_id;                                              // character的唯一表示
        public EventSet<E_BuffTrigger, EventBuffSetData> _buff_event_set    // Buff的触发器
            = new EventSet<E_BuffTrigger, EventBuffSetData>();

        public BaseEntitiesData _entities_data;


        #region Buff

        public bool RegisterHandler(E_BuffTrigger key, EventSet<E_BuffTrigger, EventBuffSetData>.EventHandler handler)
        {
            return _buff_event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_BuffTrigger key, EventSet<E_BuffTrigger, EventBuffSetData>.EventHandler handler)
        {
            return _buff_event_set.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_BuffTrigger key, EventBuffSetData obj_info)
        {
            if (_buff_event_set == null) return;
            _buff_event_set.RaiseEvent(key, obj_info);
        }

        public BuffContainer GetBuffContainer()
        {
            return _buff_container;
        }

        #endregion

        #region Get Property Or Attribute

        public AttributeIntParam FindAttribute(E_CharAttributeType type)
        {
            return _entities_data.FindAttribute(type);
        }

        public float FindValue(E_CharValueType type)
        {
            return _entities_data.FindValue(type);
        }

        public void ChangeValue(E_CharValueType type, float value)
        {
            _entities_data.ChangeValue(type, value);
        }

        public void ResetValue(E_CharValueType type, float value)
        {
            _entities_data.ResetValue(type, value);
        }

        #endregion
    }
}

