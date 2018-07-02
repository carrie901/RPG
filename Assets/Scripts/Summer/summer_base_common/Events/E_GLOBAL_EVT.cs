using System;
using System.Collections.Generic;

namespace Summer
{
    public enum E_GLOBAL_EVT : int
    {
        none = 0,
        // 实体
        entity_born,
        entity_dead,                        // entity死亡

        // 某一个目标添加了buff
        buff_detach,                        //
        buff_attach,                        //

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
