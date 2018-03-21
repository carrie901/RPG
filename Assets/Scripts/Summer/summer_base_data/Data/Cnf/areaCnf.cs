using System.IO;
public class areaCnf : BaseCsv
{
	 // ID(竞技场机器人数据)
	 public int ID;

	 // 范围开始
	 public int begin;

	 // 范围结束
	 public int end;

	 // 阵位信息(npcid%等级%品质%星级$……)
	 public string formt;

	 // 机器人先手值
	 public int robot_first_attack;

	 // 前台显示用的假数据阵位信息(npcid%等级%品质%星级$……)
	 public string formt_observe;

	 // 用户头像
	 public int npc_hero_id;

	 // 关联头像表id
	 public int user_head_id;

	 // 机器人等级
	 public int robot_level;

	 // 机器人战斗力(基础）
	 public int robot_power;

	 // 机器人战斗力K值
	 public float robot_power_k;

	 // 机器人战斗力X值上限
	 public int rebot_x_max;

	public override int GetId()
	{
		return ID;
	}
	public override void InitByReader(BinaryReader reader)
	{
		ID = reader.ReadInt32();

		begin = reader.ReadInt32();

		end = reader.ReadInt32();

		formt = reader.ReadString();

		robot_first_attack = reader.ReadInt32();

		formt_observe = reader.ReadString();

		npc_hero_id = reader.ReadInt32();

		user_head_id = reader.ReadInt32();

		robot_level = reader.ReadInt32();

		robot_power = reader.ReadInt32();

		robot_power_k = reader.ReadSingle();

		rebot_x_max = reader.ReadInt32();

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(ID);

		writer.Write(begin);

		writer.Write(end);

		writer.Write(formt);

		writer.Write(robot_first_attack);

		writer.Write(formt_observe);

		writer.Write(npc_hero_id);

		writer.Write(user_head_id);

		writer.Write(robot_level);

		writer.Write(robot_power);

		writer.Write(robot_power_k);

		writer.Write(rebot_x_max);

	}
}
