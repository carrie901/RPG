using System.IO;
public class SkillTypeCDListCnf : BaseCsv
{
	 // ID(技能类型)
	 public int ID;

	 // cd长度
	 public int cdLenth;

	 // 权重系数
	 public int weight;

	public override int GetId()
	{
		return ID;
	}
	public override void InitByReader(BinaryReader reader)
	{
		ID = reader.ReadInt32();

		cdLenth = reader.ReadInt32();

		weight = reader.ReadInt32();

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(ID);

		writer.Write(cdLenth);

		writer.Write(weight);

	}
}
