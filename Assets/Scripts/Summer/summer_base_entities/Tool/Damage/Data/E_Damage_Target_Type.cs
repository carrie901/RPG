namespace Summer
{
    public enum E_Damage_Target_Type
    {
        none = 0,
        entity = 1,
        pos_circle = 2,         // 圆形
        dir_sector_ring = 3,    //	圆环切角，不是扇形，conf:back_radius, front_radius, half_angle
        entity_ring = 4,
        dir_sector = 5,         //扇形aoe
        dir_rect = 6,           //矩形aoe
    }
}