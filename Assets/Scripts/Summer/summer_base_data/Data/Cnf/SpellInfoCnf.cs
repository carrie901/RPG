using System.IO;
public class SpellInfoCnf : BaseCsv
{
    // ID(技能唯一id)
    public int ID;
    // 技能名字
    public string skillname;
    // PrefabName
    public string PrefabName;
    // 技能类型
    public int skilltypes;
    // 攻击作用效果
    public int effect_list;
    // 技能特效
    public string[] skill_effect;
    public string anim_name;
    public override int GetId()
    {
        return ID;
    }
    public override void InitByReader(BinaryReader reader)
    {
        
    }
    public override void InitByWriter(BinaryWriter writer)
    {
        
    }
}
