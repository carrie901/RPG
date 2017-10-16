using UnityEngine;
using System.Collections;


public enum E_Camera_EVENT
{
    none = 0,
    camera_set_player,
    camera_remove_player,
    camera_set_target,
    camera_shake,

    camera_offset,

    camera_qte,

    camera_source_add,
    camera_source_rem,
    camera_source_set,

    camera_view_set,
    camera_view_rem,

    max,
}

public enum E_CHAR_EVENT
{
    none = 0,

    hp_update,              //血量更新
    armor_update,           //霸体更新
    armor_store_update,     //霸气储备值更新
    peer_less_update,       //无双值更新
        
    max,
}