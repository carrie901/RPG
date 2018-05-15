using System.Collections.Generic;
using System.IO;
namespace Summer
{
	/// <summary>
	/// 
	/// </summary>
	public class SpellInfoCnf : BaseCsv
	{
		/// <summary>
		/// ID(技能唯一id)
		/// </summary>
		public int id;

		/// <summary>
		/// 技能名字
		/// </summary>
		public string skill_name;

		/// <summary>
		/// PrefabName
		/// </summary>
		public string prefab_name;

		/// <summary>
		/// 技能类型
		/// </summary>
		public int skill_types;

		/// <summary>
		/// 攻击作用效果
		/// </summary>
		public int effect_list;

		/// <summary>
		/// 技能特效
		/// </summary>
		public string[] skill_effect;

		/// <summary>
		/// 动作
		/// </summary>
		public string anim_name;

		/// <summary>
		/// 技能模板
		/// </summary>
		public string process_template;


		//属性Local读取
		public void ToLocalRead(List<string> contents)
		{
			id = ByteHelper.ToInt(contents[0]);
			skill_name = ByteHelper.ToStr(contents[1]);
			prefab_name = ByteHelper.ToStr(contents[2]);
			skill_types = ByteHelper.ToInt(contents[3]);
			effect_list = ByteHelper.ToInt(contents[4]);
			skill_effect = ByteHelper.ToStrs(contents[5]);
			anim_name = ByteHelper.ToStr(contents[6]);
			process_template = ByteHelper.ToStr(contents[7]);
		}

		//属性Byte读取
		public void ToByteRead(BinaryReader br)
		{
			id = ByteHelper.ReadInt(br);
			skill_name = ByteHelper.ReadString(br);
			prefab_name = ByteHelper.ReadString(br);
			skill_types = ByteHelper.ReadInt(br);
			effect_list = ByteHelper.ReadInt(br);
			skill_effect = ByteHelper.ReadStringS(br);
			anim_name = ByteHelper.ReadString(br);
			process_template = ByteHelper.ReadString(br);
		}

		//属性Byte写入
		public void ToByteWrite(BinaryWriter bw)
		{
			ByteHelper.WriteInt(bw,id);
			ByteHelper.WriteString(bw,skill_name);
			ByteHelper.WriteString(bw,prefab_name);
			ByteHelper.WriteInt(bw,skill_types);
			ByteHelper.WriteInt(bw,effect_list);
			ByteHelper.WriteStringS(bw,skill_effect);
			ByteHelper.WriteString(bw,anim_name);
			ByteHelper.WriteString(bw,process_template);
		}
		public string ToLocalWrite()
		{
			return ByteHelper.ToOutInt(id)+", "+
				ByteHelper.ToOutStr(skill_name)+", "+
				ByteHelper.ToOutStr(prefab_name)+", "+
				ByteHelper.ToOutInt(skill_types)+", "+
				ByteHelper.ToOutInt(effect_list)+", "+
				ByteHelper.ToOutStrs(skill_effect)+", "+
				ByteHelper.ToOutStr(anim_name)+", "+
				ByteHelper.ToOutStr(process_template);
		}
	}
}

