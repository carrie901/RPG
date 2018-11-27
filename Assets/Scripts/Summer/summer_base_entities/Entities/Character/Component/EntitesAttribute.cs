using System.Collections.Generic;

namespace Summer
{
    public class EntitesAttribute
    {

        public AttributeIntParam _maxHp = new AttributeIntParam();
        public AttributeIntParam _antiCri = new AttributeIntParam();
        public Dictionary<E_EntityAttributeType, AttributeIntParam> _param
            = new Dictionary<E_EntityAttributeType, AttributeIntParam>();

        public int _entityId;
        public EntitesAttribute(int entityIid)
        {
            _entityId = entityIid;
            _param.Add(E_EntityAttributeType.anti_cri, _antiCri);
            _antiCri.SetBase(1000);
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

