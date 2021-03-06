﻿
namespace Summer
{
    /// <summary>
    /// 得到角色身上的属性值和属性
    /// </summary>
    public interface I_CharacterProperty
    {
        /// <summary>
        /// 角色属性
        /// </summary>
        /// <param name="type">力量/耐力/攻击力</param>
        /// <returns></returns>
        AttributeIntParam FindAttribute(E_EntityAttributeType type);

        /// <summary`>
        /// 角色数值
        /// </summary>
        /// <param name="type">血量/魔法</param>
        /// <returns></returns>
        float FindValue(E_CharValueType type);

        void ChangeValue(E_CharValueType type, float value);

        void ResetValue(E_CharValueType type, float value);
    }
}

