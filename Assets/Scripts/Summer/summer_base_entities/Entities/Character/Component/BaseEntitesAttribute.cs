using System.Collections;
using System.Collections.Generic;

namespace Summer
{
    public class BaseEntitesAttribute
    {

        public AttributeIntParam max_hp = new AttributeIntParam();
        public Dictionary<E_CharAttributeType, AttributeIntParam> _param
            = new Dictionary<E_CharAttributeType, AttributeIntParam>();

        public EntityId _entity_iid;
        public BaseEntitesAttribute(EntityId entity_iid)
        {
            _entity_iid = entity_iid;
            _param.Add(E_CharAttributeType.max_hp, max_hp);
        }

        public AttributeIntParam FindAttribute(E_CharAttributeType type)
        {
            AttributeIntParam param;
            if (_param.TryGetValue(type, out param))
            {
                return param;
            }

            LogManager.Error("找不到对应的属性:[{0}]", type);
            return null;
        }
    }

}

