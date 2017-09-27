
using Summer;

public class iCharacterBaseController
{
    public CharId char_id;
    public BuffContainer _buff_container;

    public EventSet<E_BuffTrigger, EventBuffSetData> _buff_event_set = new EventSet<E_BuffTrigger, EventBuffSetData>();
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
        _buff_event_set.RaiseEvent(key, obj_info, false);
    }

    public float FindValue(E_CharValueType type)
    {
        return 0;
    }

    public PropertyIntParam FindAttribute(E_CharAttributeType type)
    {
        return null;
    }
}
