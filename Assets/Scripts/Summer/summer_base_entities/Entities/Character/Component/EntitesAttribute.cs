using System.Collections;
using System.Collections.Generic;

namespace Summer
{
    public class EntitesAttribute
    {

        public AttributeIntParam max_hp = new AttributeIntParam();
        public AttributeIntParam anti_cri = new AttributeIntParam();
        public Dictionary<E_EntityAttributeType, AttributeIntParam> _param
            = new Dictionary<E_EntityAttributeType, AttributeIntParam>();

        public EntityId _entity_iid;
        public EntitesAttribute(EntityId entity_iid)
        {
            _entity_iid = entity_iid;
            _param.Add(E_EntityAttributeType.anti_cri, anti_cri);
            anti_cri.SetBase(1000);
        }

        public AttributeIntParam FindAttribute(E_EntityAttributeType type)
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

