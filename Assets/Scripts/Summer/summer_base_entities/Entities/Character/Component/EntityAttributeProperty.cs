

using MsgPb;

namespace Summer
{
    public class EntityAttributeProperty: I_CharacterProperty,I_RegisterHandler
    {
        public int _entityId;
        public EntitesAttribute _attribute;             //属性
        public BaseEntitiesProperty _property;              //数值

        public EntityAttributeProperty(int entityId)
        {
            _entityId = entityId;
            _attribute = new EntitesAttribute(entityId);
            _property = new BaseEntitiesProperty(entityId);
        }

        #region 得到Entity的属性和数值 想把这一块的东西转移到 EntitiesAttributeProperty内部来实现，整体思路死BaseEntity有很多内部组件，对应的功能在内部组件来实现 这块不好搞哦

        public AttributeIntParam FindAttribute(E_EntityAttributeType type)
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

        #endregion

        public void OnRegisterHandler()
        {
            
        }

        public void UnRegisterHandler()
        {
            
        }

        public bool IsDead()
        {
            return false;
        }
    }
}

