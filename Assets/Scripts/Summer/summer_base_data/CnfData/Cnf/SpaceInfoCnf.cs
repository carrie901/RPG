using System.IO;
public class SpaceInfoCnf : BaseCsv
{
	 // ID(关卡唯一)
	 public int ID;

	 // 关卡名字
	 public string skillname;

	 // 多版本名字id
	 public int OtherName;

	 // 小图标名字
	 public string IconName;

	 // PrefabName
	 public string PrefabName;

	 // 关卡类型
	 public int type;

	 //  场景名字
	 public string mapName;

	 // 胜利条件
	 public int VictoryCondition;

	 // 胜利条件参数1
	 public int VictoryConditionPar1;

	 // 胜利条件参数2
	 public int VictoryConditionPar2;

	 // 失败条件1
	 public int DefeatCondition1;

	 // 失败1参数1
	 public int DefeatCondition1Par1;

	 // 失败1参数2
	 public int DefeatCondition1Par2;

	 // 失败条件2
	 public int DefeatCondition2;

	 // 失败2参数1
	 public int DefeatCondition2Par1;

	 // 失败2参数2
	 public int DefeatCondition2Par2;

	 // 评星条件1
	 public int StarCondition1;

	 // 评星1参数1
	 public int StarCondition1Par1;

	 // 评星1参数2
	 public int StarCondition1Par2;

	 // 评星条件2
	 public int StarCondition2;

	 // 评星2参数1
	 public int StarCondition2Par1;

	 // 评星2参数2
	 public int StarCondition2Par2;

	 // 关卡限定时间
	 public int TimeLimit;

	 // 关卡结算类型
	 public int CheckType;

	 // 主角出生点
	 public int[] bornPos;

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

		type = reader.ReadInt32();

		mapName = reader.ReadString();

		VictoryCondition = reader.ReadInt32();

		VictoryConditionPar1 = reader.ReadInt32();

		VictoryConditionPar2 = reader.ReadInt32();

		DefeatCondition1 = reader.ReadInt32();

		DefeatCondition1Par1 = reader.ReadInt32();

		DefeatCondition1Par2 = reader.ReadInt32();

		DefeatCondition2 = reader.ReadInt32();

		DefeatCondition2Par1 = reader.ReadInt32();

		DefeatCondition2Par2 = reader.ReadInt32();

		StarCondition1 = reader.ReadInt32();

		StarCondition1Par1 = reader.ReadInt32();

		StarCondition1Par2 = reader.ReadInt32();

		StarCondition2 = reader.ReadInt32();

		StarCondition2Par1 = reader.ReadInt32();

		StarCondition2Par2 = reader.ReadInt32();

		TimeLimit = reader.ReadInt32();

		CheckType = reader.ReadInt32();

		int length_bornPos = reader.ReadInt32();
		bornPos = new int[length_bornPos];
		for(int i = 0; i < length_bornPos; i++)
		{
		bornPos[i] = reader.ReadInt32();
		}

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(ID);

		writer.Write(skillname);

		writer.Write(OtherName);

		writer.Write(IconName);

		writer.Write(PrefabName);

		writer.Write(type);

		writer.Write(mapName);

		writer.Write(VictoryCondition);

		writer.Write(VictoryConditionPar1);

		writer.Write(VictoryConditionPar2);

		writer.Write(DefeatCondition1);

		writer.Write(DefeatCondition1Par1);

		writer.Write(DefeatCondition1Par2);

		writer.Write(DefeatCondition2);

		writer.Write(DefeatCondition2Par1);

		writer.Write(DefeatCondition2Par2);

		writer.Write(StarCondition1);

		writer.Write(StarCondition1Par1);

		writer.Write(StarCondition1Par2);

		writer.Write(StarCondition2);

		writer.Write(StarCondition2Par1);

		writer.Write(StarCondition2Par2);

		writer.Write(TimeLimit);

		writer.Write(CheckType);

		int length_bornPos = bornPos.Length;
		writer.Write(length_bornPos);
		for(int i=0;i<length_bornPos; i++)
		{
				writer.Write(bornPos[i]);
		}

	}
}
