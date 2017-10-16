using UnityEngine;
using System.Collections;


public class CharId
{
    static uint iidex = 0;
    public uint iid;

    public CharId()
    {
        iid = iidex;
        iidex++;
    }

    public bool EqualId(CharId id)
    {
        return iid == id.iid;
    }
}
