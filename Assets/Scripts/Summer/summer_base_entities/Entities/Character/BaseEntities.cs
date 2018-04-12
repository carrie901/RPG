﻿using System.Collections.Generic;
namespace Summer
{
    /// <summary>
    /// 先把BaseEntities当做BaseCharacter用
    /// TODO 4.12
    /// QAQ:
    ///     根据高内聚原则,entity相关的方法最好内聚在本类，如果需求是多样化的。那么提供get方法让外部进行操作，但最总基础的方法还是在本类
    /// QAQ:
    ///     关于涉及到生命周期的一些东西，比如技能特效，声音，目前不太好处理，目前把这些东西放到SkillModule中
    /// </summary>
    public class BaseEntities : I_EntitiesBuff, I_CharacterProperty, I_Update
    {
        #region 属性

        public BuffContainer _buff_container;                               // Buff容器
        public SkillContainer _skill_container;                             // 技能容器
        public CharId char_id;                                              // character的唯一表示
        public EventSet<E_BuffTrigger, EventBuffSetData> _buff_event_set    // Buff的触发器
            = new EventSet<E_BuffTrigger, EventBuffSetData>();

        public List<I_Update> update_list = new List<I_Update>();
        public EntitiesAttributeProperty entities_attribute_property;
        public BaseEntityController _entity_controller;
        #endregion

        #region 构造

        public BaseEntities(BaseEntityController entity_controller)
        {
            _entity_controller = entity_controller;
            _skill_container = new SkillContainer(this);
            update_list.Add(_skill_container);
        }

        #endregion

        #region public

        public void CastSkill()
        {
            if (_skill_container == null) return;
            _skill_container.CastSkill(1);
        }

        public I_EntityInTrigger GetTrigger()
        {
            return _entity_controller;
        }


        public void OnUpdate(float dt)
        {
            int length = update_list.Count;
            for (int i = 0; i < length; i++)
            {
                update_list[i].OnUpdate(dt);
            }
        }


        #endregion

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
            return entities_attribute_property.FindAttribute(type);
        }

        public float FindValue(E_CharValueType type)
        {
            return entities_attribute_property.FindValue(type);
        }

        public void ChangeValue(E_CharValueType type, float value)
        {
            entities_attribute_property.ChangeValue(type, value);
        }

        public void ResetValue(E_CharValueType type, float value)
        {
            entities_attribute_property.ResetValue(type, value);
        }

        #endregion

    }
}

