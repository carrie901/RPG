
namespace Summer
{
    /// <summary>
    /// 角色数值
    /// </summary>
    public enum E_CharValueType
    {
        none,
        hp,         //血量
        max,
    }

    /// <summary>
    /// 角色某种属性更新
    /// </summary>
    public enum E_EntityAttributeType
    {
        none = 0,

        atk = 1,                    //攻击力 
        defense = 2,                //防御  
        max_hp = 3,                 //最大血量  
        cri_rate = 4,               //暴击率 
        anti_cri = 5,               //抗暴率 
        wreck = 6,                  //破击率 
        block = 7,                  //格挡率 
        atk_speed = 8,              //攻速  
        cri_dmg = 9,                //暴伤  
        anti_cri_dmg = 10,          //抗暴伤 
        block_intensity = 11,       //格挡免伤率   
        atk_intensity = 12,         //伤害率 
        def_intensity = 13,         //免伤率 
        suck_blood = 14,            //吸血率 
        anti_suck_blood = 15,       //吸血抵抗率
        reflect_dmg_rate = 16,      //反伤率
                                    //hp = 17,                    //血量  血量和无双有点特殊性质
                                    //peerless = 18,              //无双
        move_speed = 19,              //移速
        max,

    }
}