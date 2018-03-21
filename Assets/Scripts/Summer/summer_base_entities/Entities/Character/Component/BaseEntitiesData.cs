

namespace Summer
{
    public class BaseEntitiesData
    {
        public CharId _char_id;
        public BaseEntitesAttribute _attribute;             //属性
        public BaseEntitiesProperty _property;              //数值

        public BaseEntitiesData(CharId char_id)
        {
            _char_id = char_id;
            _attribute = new BaseEntitesAttribute(char_id);
            _property = new BaseEntitiesProperty(char_id);
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

