

namespace Summer
{
    public class EntitiesAttributeProperty: I_CharacterProperty
    {
        public EntityId entity_id;
        public BaseEntitesAttribute _attribute;             //属性
        public BaseEntitiesProperty _property;              //数值

        public EntitiesAttributeProperty(EntityId entity_id)
        {
            entity_id = entity_id;
            _attribute = new BaseEntitesAttribute(entity_id);
            _property = new BaseEntitiesProperty(entity_id);
        }

        public AttributeIntParam FindAttribute(E_CharAttributeType type)
        {
            return _attribute.FindAttribute(type);
        }

        public float FindValue(E_CharValueType type)
        {
            return _property.FindValue(type);
        }

        public void ChangeValue(E_CharValueType type, float value)
        {
            _property.ChangeValue(type, value);
        }

        public void ResetValue(E_CharValueType type, float value)
        {
            _property.ResetValue(type, value);
        }
    }
}

