using System.IO;
public class SpellInfoCnf : BaseCsv
{
	 // ID(技能唯一id)
	 public int ID;

	 // 技能名字
	 public string skillname;

	 // 多版本名字id
	 public int OtherName;

	 // 小图标名字
	 public string IconName;

	 // PrefabName
	 public string PrefabName;

	 // 是否有下个技能
	 public int hasNextSkill;

	 // 下个技能id
	 public int[] nextSkillid;

	 // 技能类型
	 public int skilltypes;

	 // 技能持续时间
	 public int keepTime;

	 // 技能cd
	 public int skillCD;

	 // 技能权重(自动战斗的优先级刷选)
	 public int weight;

	 // 距离目标施法距离
	 public int skillRandge;

	 // 攻击作用效果
	 public int effectList;

	 // 击飞效果
	 public int[] canknock;

	 // 击飞高度
	 public int[] knockheight;

	 // 击飞距离
	 public int[] knockdictance;

	 // 添加buff
	 public int[] add_buff;

	 // 每段伤害效果
	 public int[] damage_effect;

	 // 每段削减霸体值
	 public int[] lose_super_armor;

	 // 每段伤害系数
	 public int[] damage_factor;

	 // 增加无双能量
	 public int add_sp;

	 // 消耗无双能量
	 public int cost_sp;

	public override int GetId()
	{
		return ID;
	}
	public override void InitByReader(BinaryReader reader)
	{
		ID = reader.ReadInt32();

		skillname = reader.ReadString();

		OtherName = reader.ReadInt32();

		IconName = reader.ReadString();

		PrefabName = reader.ReadString();

		hasNextSkill = reader.ReadInt32();

		int length_nextSkillid = reader.ReadInt32();
		nextSkillid = new int[length_nextSkillid];
		for(int i = 0; i < length_nextSkillid; i++)
		{
		nextSkillid[i] = reader.ReadInt32();
		}

		skilltypes = reader.ReadInt32();

		keepTime = reader.ReadInt32();

		skillCD = reader.ReadInt32();

		weight = reader.ReadInt32();

		skillRandge = reader.ReadInt32();

		effectList = reader.ReadInt32();

		int length_canknock = reader.ReadInt32();
		canknock = new int[length_canknock];
		for(int i = 0; i < length_canknock; i++)
		{
		canknock[i] = reader.ReadInt32();
		}

		int length_knockheight = reader.ReadInt32();
		knockheight = new int[length_knockheight];
		for(int i = 0; i < length_knockheight; i++)
		{
		knockheight[i] = reader.ReadInt32();
		}

		int length_knockdictance = reader.ReadInt32();
		knockdictance = new int[length_knockdictance];
		for(int i = 0; i < length_knockdictance; i++)
		{
		knockdictance[i] = reader.ReadInt32();
		}

		int length_add_buff = reader.ReadInt32();
		add_buff = new int[length_add_buff];
		for(int i = 0; i < length_add_buff; i++)
		{
		add_buff[i] = reader.ReadInt32();
		}

		int length_damage_effect = reader.ReadInt32();
		damage_effect = new int[length_damage_effect];
		for(int i = 0; i < length_damage_effect; i++)
		{
		damage_effect[i] = reader.ReadInt32();
		}

		int length_lose_super_armor = reader.ReadInt32();
		lose_super_armor = new int[length_lose_super_armor];
		for(int i = 0; i < length_lose_super_armor; i++)
		{
		lose_super_armor[i] = reader.ReadInt32();
		}

		int length_damage_factor = reader.ReadInt32();
		damage_factor = new int[length_damage_factor];
		for(int i = 0; i < length_damage_factor; i++)
		{
		damage_factor[i] = reader.ReadInt32();
		}

		add_sp = reader.ReadInt32();

		cost_sp = reader.ReadInt32();

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(ID);

		writer.Write(skillname);

		writer.Write(OtherName);

		writer.Write(IconName);

		writer.Write(PrefabName);

		writer.Write(hasNextSkill);

		int length_nextSkillid = nextSkillid.Length;
		writer.Write(length_nextSkillid);
		for(int i=0;i<length_nextSkillid; i++)
		{
				writer.Write(nextSkillid[i]);
		}

		writer.Write(skilltypes);

		writer.Write(keepTime);

		writer.Write(skillCD);

		writer.Write(weight);

		writer.Write(skillRandge);

		writer.Write(effectList);

		int length_canknock = canknock.Length;
		writer.Write(length_canknock);
		for(int i=0;i<length_canknock; i++)
		{
				writer.Write(canknock[i]);
		}

		int length_knockheight = knockheight.Length;
		writer.Write(length_knockheight);
		for(int i=0;i<length_knockheight; i++)
		{
				writer.Write(knockheight[i]);
		}

		int length_knockdictance = knockdictance.Length;
		writer.Write(length_knockdictance);
		for(int i=0;i<length_knockdictance; i++)
		{
				writer.Write(knockdictance[i]);
		}

		int length_add_buff = add_buff.Length;
		writer.Write(length_add_buff);
		for(int i=0;i<length_add_buff; i++)
		{
				writer.Write(add_buff[i]);
		}

		int length_damage_effect = damage_effect.Length;
		writer.Write(length_damage_effect);
		for(int i=0;i<length_damage_effect; i++)
		{
				writer.Write(damage_effect[i]);
		}

		int length_lose_super_armor = lose_super_armor.Length;
		writer.Write(length_lose_super_armor);
		for(int i=0;i<length_lose_super_armor; i++)
		{
				writer.Write(lose_super_armor[i]);
		}

		int length_damage_factor = damage_factor.Length;
		writer.Write(length_damage_factor);
		for(int i=0;i<length_damage_factor; i++)
		{
				writer.Write(damage_factor[i]);
		}

		writer.Write(add_sp);

		writer.Write(cost_sp);

	}
}
