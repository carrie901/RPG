using System.IO;
public class HeroInfoCnf : BaseCsv
{
    // ID(英雄模板id)
    public int id;

    // 英雄名字
    public string hero_name;

    // PrefabName
    public string prefab_name;

    // 死亡技能id
    public float dead_skill;

    // 移动速度
    public int move_speed;

    // 技能id(普攻只记录第一下id)
    public int[] skillid_list;

    // 出生技能
    public int born_skill;

    public override int GetId()
    {
        return id;
    }
    public override void InitByReader(BinaryReader reader) { }
    public override void InitByWriter(BinaryWriter writer) { }
}
