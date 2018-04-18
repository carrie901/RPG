using System;
using UnityEngine;
using System.Collections;
using Summer;

public class BuffId
{
    private static long _iid_index = 1;
    public uint _char_id;                 //这个Buff 在哪个角色身上
    public long _iid;                       //buff的iid 唯一
    public int _buff_id;                    //buff的Id
    public BuffId(uint char_id, int buff_id)
    {
        //LogManager.Assert(char_id != null, "caster_id ==null ,buff id is:" + buff_id);
        LogManager.Assert(buff_id > 0, "buff id is error,id:" + buff_id);

        _char_id = char_id;
        _buff_id = buff_id;
        _iid = _iid_index;
        _iid_index++;

    }

    public bool CheckById(int buff_id)
    {
        if (buff_id == _buff_id) return true;
        return false;
    }

    public bool CheckByIid(BuffId bid)
    {
        return bid._iid == _iid;
    }
}