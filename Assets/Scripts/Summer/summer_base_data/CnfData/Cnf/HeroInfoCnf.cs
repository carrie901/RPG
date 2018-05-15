using System.Collections.Generic;
using System.IO;
namespace Summer
{
	/// <summary>
	/// 
	/// </summary>
	public class HeroInfoCnf : BaseCsv
	{
		/// <summary>
		/// 编号
		/// </summary>
		public int id;

		/// <summary>
		/// 名称
		/// </summary>
		public string hero_name;

		/// <summary>
		/// 模型prefab
		/// </summary>
		public string prefab_name;

		/// <summary>
		/// 死亡技能id
		/// </summary>
		public float dead_skill;

		/// <summary>
		/// 移动速度
		/// </summary>
		public int move_speed;

		/// <summary>
		/// 技能列表
		/// </summary>
		public int[] skillid_list;

		/// <summary>
		/// 出生技能
		/// </summary>
		public int born_skill;


		//属性Local读取
		public void ToLocalRead(List<string> contents)
		{
			id = ByteHelper.ToInt(contents[0]);
			hero_name = ByteHelper.ToStr(contents[1]);
			prefab_name = ByteHelper.ToStr(contents[2]);
			dead_skill = ByteHelper.ToFloat(contents[3]);
			move_speed = ByteHelper.ToInt(contents[4]);
			skillid_list = ByteHelper.ToInts(contents[5]);
			born_skill = ByteHelper.ToInt(contents[6]);
		}

		//属性Byte读取
		public void ToByteRead(BinaryReader br)
		{
			id = ByteHelper.ReadInt(br);
			hero_name = ByteHelper.ReadString(br);
			prefab_name = ByteHelper.ReadString(br);
			dead_skill = ByteHelper.ReadFloat(br);
			move_speed = ByteHelper.ReadInt(br);
			skillid_list = ByteHelper.ReadIntS(br);
			born_skill = ByteHelper.ReadInt(br);
		}

		//属性Byte写入
		public void ToByteWrite(BinaryWriter bw)
		{
			ByteHelper.WriteInt(bw,id);
			ByteHelper.WriteString(bw,hero_name);
			ByteHelper.WriteString(bw,prefab_name);
			ByteHelper.WriteFloat(bw,dead_skill);
			ByteHelper.WriteInt(bw,move_speed);
			ByteHelper.WriteIntS(bw,skillid_list);
			ByteHelper.WriteInt(bw,born_skill);
		}
		public string ToLocalWrite()
		{
			return ByteHelper.ToOutInt(id)+", "+
				ByteHelper.ToOutStr(hero_name)+", "+
				ByteHelper.ToOutStr(prefab_name)+", "+
				ByteHelper.ToOutFloat(dead_skill)+", "+
				ByteHelper.ToOutInt(move_speed)+", "+
				ByteHelper.ToOutInts(skillid_list)+", "+
				ByteHelper.ToOutInt(born_skill);
		}
	}
}

