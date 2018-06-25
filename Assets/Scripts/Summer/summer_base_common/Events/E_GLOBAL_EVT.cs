using System;
using System.Collections.Generic;

namespace Summer
{
    public enum E_GLOBAL_EVT : int
    {
        none = 0,
        char_hp_update,                     //char血量更新
        char_armor_update,                  //char霸体值更新
        char_armor_store_update,            //char霸体储备值更新
        char_peerless_update,               //char无双值更新
        char_dead,                          //char死亡

        buff_detach,                        //
        buff_attach,                        //

        //TODO 是否可以把这个完全的消息机制剥离到单独某个地方
        //camera镜头特效 
        camera_effect_radial_blur,          // 径向模糊:图像旋转成从中心辐射。
        camera_effect_motion_blur,          // 运动模糊镜头特效
        camera_shake,                       // 镜头震动

        camera_set_player,                  // 
        camera_remove_player,               //

        camera_source_add,
        camera_source_rem,

        camera_view_set,
        camera_view_rem,
        max,
    };
}
