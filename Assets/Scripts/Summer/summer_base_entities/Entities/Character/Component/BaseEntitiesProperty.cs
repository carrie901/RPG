﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class BaseEntitiesProperty
    {
        /*public Dictionary<E_CharValueType, float> _param
               = new Dictionary<E_CharValueType, float>();*/
        public float hp;
        public int _entityId;

        public BaseEntitiesProperty(int entityId)
        {
            _entityId = entityId;
        }

        public float FindValue(E_CharValueType type)
        {
            if (type == E_CharValueType.HP)
            {
                return hp;
            }

            LogManager.Error("找不到对应的数值类型:[{0}]", type);

            return 0;
        }

        public void ChangeValue(E_CharValueType type, float value)
        {
            LogManager.Log("修改数值:[{0}],value:[{1}]", type, value);
        }

        public void ResetValue(E_CharValueType type, float value)
        {
            LogManager.Log("重置数值:[{0}],value:[{1}]", type, value);
        }
    }
}

