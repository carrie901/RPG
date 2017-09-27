using System.IO;
public class MonsterInfoCnf : BaseCsv
{
	 // ID(怪物模板id)
	 public int ID;

	 // 怪物名字
	 public string Name;

	 // 多版本名字id
	 public int OtherName;

	 // 小图标名字
	 public string IconName;

	 // PrefabName
	 public string PrefabName;

	 // 模型缩放
	 public string scale;

	 // 特效缩放
	 public int effectScale;

	 // 攻击类型
	 public int AtkType;

	 // 安全攻击距离
	 public int safeRange;

	 // 人物模型半径
	 public int radius;

	 // 死亡技能id
	 public float deadSkill;

	 // 移动速度
	 public int moveSpeed;

	 // 备战移动速度
	 public int atk_movespeed;

	 // 后撤速度
	 public int escape_movespeed;

	 // 技能id(普攻只记录第一下id)
	 public int[] skillidList;

	 // 是否允许被推挤
	 public int pushValid;

	 // 攻击力
	 public int damage;

	 // 攻击资质
	 public int dmg_add;

	 // 防御
	 public int defense;

	 // 防御资质
	 public int def_add;

	 // 血量
	 public int max_hp;

	 // 血量资质
	 public int hp_add;

	 // 暴击率
	 public int cri_rate;

	 // 抗暴率
	 public int anti_cri;

	 // 破击率
	 public int wreck;

	 // 格挡率
	 public int block;

	 // 攻速
	 public int atk_speed;

	 // 暴伤
	 public int cri_dmg;

	 // 抗暴伤
	 public int anti_cri_dmg;

	 // 格挡免伤率
	 public int block_intensity;

	 // 伤害率
	 public int atk_intensity;

	 // 免伤率
	 public int def_intensity;

	 // 吸血率
	 public int suck_blood;

	 // 吸血抵抗率
	 public int anti_suck_blood;

	 // 反伤率
	 public int reflect_dmg_per;

	 // 警戒圈R1
	 public int warning1;

	 // 警戒圈R2
	 public int warning2;

	 // 警戒圈R3
	 public int warning3;

	 // 警戒圈R4
	 public int warning4;

	 // 攻击完等待时间
	 public int waitleaveTime;

	 // 怪物类型
	 public int enemy_type;

	 // 霸体值上限
	 public int super_armor;

	 // 霸体值恢复速度
	 public int super_armor_recovery;

	public override int GetId()
	{
		return ID;
	}
	public override void InitByReader(BinaryReader reader)
	{
		ID = reader.ReadInt32();

		Name = reader.ReadString();

		OtherName = reader.ReadInt32();

		IconName = reader.ReadString();

		PrefabName = reader.ReadString();

		scale = reader.ReadString();

		effectScale = reader.ReadInt32();

		AtkType = reader.ReadInt32();

		safeRange = reader.ReadInt32();

		radius = reader.ReadInt32();

		deadSkill = reader.ReadSingle();

		moveSpeed = reader.ReadInt32();

		atk_movespeed = reader.ReadInt32();

		escape_movespeed = reader.ReadInt32();

		int length_skillidList = reader.ReadInt32();
		skillidList = new int[length_skillidList];
		for(int i = 0; i < length_skillidList; i++)
		{
		skillidList[i] = reader.ReadInt32();
		}

		pushValid = reader.ReadInt32();

		damage = reader.ReadInt32();

		dmg_add = reader.ReadInt32();

		defense = reader.ReadInt32();

		def_add = reader.ReadInt32();

		max_hp = reader.ReadInt32();

		hp_add = reader.ReadInt32();

		cri_rate = reader.ReadInt32();

		anti_cri = reader.ReadInt32();

		wreck = reader.ReadInt32();

		block = reader.ReadInt32();

		atk_speed = reader.ReadInt32();

		cri_dmg = reader.ReadInt32();

		anti_cri_dmg = reader.ReadInt32();

		block_intensity = reader.ReadInt32();

		atk_intensity = reader.ReadInt32();

		def_intensity = reader.ReadInt32();

		suck_blood = reader.ReadInt32();

		anti_suck_blood = reader.ReadInt32();

		reflect_dmg_per = reader.ReadInt32();

		warning1 = reader.ReadInt32();

		warning2 = reader.ReadInt32();

		warning3 = reader.ReadInt32();

		warning4 = reader.ReadInt32();

		waitleaveTime = reader.ReadInt32();

		enemy_type = reader.ReadInt32();

		super_armor = reader.ReadInt32();

		super_armor_recovery = reader.ReadInt32();

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(ID);

		writer.Write(Name);

		writer.Write(OtherName);

		writer.Write(IconName);

		writer.Write(PrefabName);

		writer.Write(scale);

		writer.Write(effectScale);

		writer.Write(AtkType);

		writer.Write(safeRange);

		writer.Write(radius);

		writer.Write(deadSkill);

		writer.Write(moveSpeed);

		writer.Write(atk_movespeed);

		writer.Write(escape_movespeed);

		int length_skillidList = skillidList.Length;
		writer.Write(length_skillidList);
		for(int i=0;i<length_skillidList; i++)
		{
				writer.Write(skillidList[i]);
		}

		writer.Write(pushValid);

		writer.Write(damage);

		writer.Write(dmg_add);

		writer.Write(defense);

		writer.Write(def_add);

		writer.Write(max_hp);

		writer.Write(hp_add);

		writer.Write(cri_rate);

		writer.Write(anti_cri);

		writer.Write(wreck);

		writer.Write(block);

		writer.Write(atk_speed);

		writer.Write(cri_dmg);

		writer.Write(anti_cri_dmg);

		writer.Write(block_intensity);

		writer.Write(atk_intensity);

		writer.Write(def_intensity);

		writer.Write(suck_blood);

		writer.Write(anti_suck_blood);

		writer.Write(reflect_dmg_per);

		writer.Write(warning1);

		writer.Write(warning2);

		writer.Write(warning3);

		writer.Write(warning4);

		writer.Write(waitleaveTime);

		writer.Write(enemy_type);

		writer.Write(super_armor);

		writer.Write(super_armor_recovery);

	}
}
