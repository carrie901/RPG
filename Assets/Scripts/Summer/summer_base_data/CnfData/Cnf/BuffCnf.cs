using System.Collections.Generic;
using System.IO;
namespace Summer
{
	/// <summary>
	/// 
	/// </summary>
	public class BuffCnf : BaseCsv
	{
		/// <summary>
		/// ID
		/// </summary>
		public int id;

		/// <summary>
		/// 名称
		/// </summary>
		public string name;

		/// <summary>
		/// 描述
		/// </summary>
		public string desc;

		/// <summary>
		/// 属于某一个圈
		/// </summary>
		public string groupID;

		/// <summary>
		/// 等级
		/// </summary>
		public int level;

		/// <summary>
		/// 图标
		/// </summary>
		public string icon;

		/// <summary>
		/// 特效
		/// </summary>
		public string effect;

		/// <summary>
		/// 特效挂载点
		/// </summary>
		public string bind_pos;

		/// <summary>
		/// 音效
		/// </summary>
		public string sound;

		/// <summary>
		/// Buff类型
		/// </summary>
		public int buff_type;

		/// <summary>
		/// Buff子类型
		/// </summary>
		public int sub_type;

		/// <summary>
		/// 过程描述
		/// </summary>
		public string buff_des;

		/// <summary>
		/// 作用间隔时间
		/// </summary>
		public int interval_time;

		/// <summary>
		/// 持续时间
		/// </summary>
		public int duration;

		/// <summary>
		/// 效果1
		/// </summary>
		public int effect_1;

		/// <summary>
		/// 触发点1
		/// </summary>
		public int callback_1;

		/// <summary>
		/// 效果2
		/// </summary>
		public int effect_2;

		/// <summary>
		/// 触发点2
		/// </summary>
		public int callback_2;

		/// <summary>
		/// 效果3
		/// </summary>
		public int effect_3;

		/// <summary>
		/// 触发点3
		/// </summary>
		public int callback_3;

		/// <summary>
		/// 效果4
		/// </summary>
		public int effect_4;

		/// <summary>
		/// 效果5
		/// </summary>
		public int effect_5;

		/// <summary>
		/// 效果6
		/// </summary>
		public int effect_6;

		/// <summary>
		/// 效果7
		/// </summary>
		public string buff_param;

		/// <summary>
		/// 死亡是否消失
		/// </summary>
		public bool death_delete;

		/// <summary>
		/// 作用对象
		/// </summary>
		public int target;

		/// <summary>
		/// 国家限制
		/// </summary>
		public int country_limit;

		/// <summary>
		/// 职业限制
		/// </summary>
		public int profession_limit;

		/// <summary>
		/// 性别限制
		/// </summary>
		public int sex_limit;

		/// <summary>
		/// 优先作用
		/// </summary>
		public int state_first;

		/// <summary>
		/// 叠加层数
		/// </summary>
		public int over_lay;


		//属性Local读取
		public void ToLocalRead(List<string> contents)
		{
			id = ByteHelper.ToInt(contents[0]);
			name = ByteHelper.ToStr(contents[1]);
			desc = ByteHelper.ToStr(contents[2]);
			groupID = ByteHelper.ToStr(contents[3]);
			level = ByteHelper.ToInt(contents[4]);
			icon = ByteHelper.ToStr(contents[5]);
			effect = ByteHelper.ToStr(contents[6]);
			bind_pos = ByteHelper.ToStr(contents[7]);
			sound = ByteHelper.ToStr(contents[8]);
			buff_type = ByteHelper.ToInt(contents[9]);
			sub_type = ByteHelper.ToInt(contents[10]);
			buff_des = ByteHelper.ToStr(contents[11]);
			interval_time = ByteHelper.ToInt(contents[12]);
			duration = ByteHelper.ToInt(contents[13]);
			effect_1 = ByteHelper.ToInt(contents[14]);
			callback_1 = ByteHelper.ToInt(contents[15]);
			effect_2 = ByteHelper.ToInt(contents[16]);
			callback_2 = ByteHelper.ToInt(contents[17]);
			effect_3 = ByteHelper.ToInt(contents[18]);
			callback_3 = ByteHelper.ToInt(contents[19]);
			effect_4 = ByteHelper.ToInt(contents[20]);
			effect_5 = ByteHelper.ToInt(contents[21]);
			effect_6 = ByteHelper.ToInt(contents[22]);
			buff_param = ByteHelper.ToStr(contents[23]);
			death_delete = ByteHelper.ToBool(contents[24]);
			target = ByteHelper.ToInt(contents[25]);
			country_limit = ByteHelper.ToInt(contents[26]);
			profession_limit = ByteHelper.ToInt(contents[27]);
			sex_limit = ByteHelper.ToInt(contents[28]);
			state_first = ByteHelper.ToInt(contents[29]);
			over_lay = ByteHelper.ToInt(contents[30]);
		}

		//属性Byte读取
		public void ToByteRead(BinaryReader br)
		{
			id = ByteHelper.ReadInt(br);
			name = ByteHelper.ReadString(br);
			desc = ByteHelper.ReadString(br);
			groupID = ByteHelper.ReadString(br);
			level = ByteHelper.ReadInt(br);
			icon = ByteHelper.ReadString(br);
			effect = ByteHelper.ReadString(br);
			bind_pos = ByteHelper.ReadString(br);
			sound = ByteHelper.ReadString(br);
			buff_type = ByteHelper.ReadInt(br);
			sub_type = ByteHelper.ReadInt(br);
			buff_des = ByteHelper.ReadString(br);
			interval_time = ByteHelper.ReadInt(br);
			duration = ByteHelper.ReadInt(br);
			effect_1 = ByteHelper.ReadInt(br);
			callback_1 = ByteHelper.ReadInt(br);
			effect_2 = ByteHelper.ReadInt(br);
			callback_2 = ByteHelper.ReadInt(br);
			effect_3 = ByteHelper.ReadInt(br);
			callback_3 = ByteHelper.ReadInt(br);
			effect_4 = ByteHelper.ReadInt(br);
			effect_5 = ByteHelper.ReadInt(br);
			effect_6 = ByteHelper.ReadInt(br);
			buff_param = ByteHelper.ReadString(br);
			death_delete = ByteHelper.ReadBool(br);
			target = ByteHelper.ReadInt(br);
			country_limit = ByteHelper.ReadInt(br);
			profession_limit = ByteHelper.ReadInt(br);
			sex_limit = ByteHelper.ReadInt(br);
			state_first = ByteHelper.ReadInt(br);
			over_lay = ByteHelper.ReadInt(br);
		}

		//属性Byte写入
		public void ToByteWrite(BinaryWriter bw)
		{
			ByteHelper.WriteInt(bw,id);
			ByteHelper.WriteString(bw,name);
			ByteHelper.WriteString(bw,desc);
			ByteHelper.WriteString(bw,groupID);
			ByteHelper.WriteInt(bw,level);
			ByteHelper.WriteString(bw,icon);
			ByteHelper.WriteString(bw,effect);
			ByteHelper.WriteString(bw,bind_pos);
			ByteHelper.WriteString(bw,sound);
			ByteHelper.WriteInt(bw,buff_type);
			ByteHelper.WriteInt(bw,sub_type);
			ByteHelper.WriteString(bw,buff_des);
			ByteHelper.WriteInt(bw,interval_time);
			ByteHelper.WriteInt(bw,duration);
			ByteHelper.WriteInt(bw,effect_1);
			ByteHelper.WriteInt(bw,callback_1);
			ByteHelper.WriteInt(bw,effect_2);
			ByteHelper.WriteInt(bw,callback_2);
			ByteHelper.WriteInt(bw,effect_3);
			ByteHelper.WriteInt(bw,callback_3);
			ByteHelper.WriteInt(bw,effect_4);
			ByteHelper.WriteInt(bw,effect_5);
			ByteHelper.WriteInt(bw,effect_6);
			ByteHelper.WriteString(bw,buff_param);
			ByteHelper.WriteBool(bw,death_delete);
			ByteHelper.WriteInt(bw,target);
			ByteHelper.WriteInt(bw,country_limit);
			ByteHelper.WriteInt(bw,profession_limit);
			ByteHelper.WriteInt(bw,sex_limit);
			ByteHelper.WriteInt(bw,state_first);
			ByteHelper.WriteInt(bw,over_lay);
		}
		public string ToLocalWrite()
		{
			return ByteHelper.ToOutInt(id)+", "+
				ByteHelper.ToOutStr(name)+", "+
				ByteHelper.ToOutStr(desc)+", "+
				ByteHelper.ToOutStr(groupID)+", "+
				ByteHelper.ToOutInt(level)+", "+
				ByteHelper.ToOutStr(icon)+", "+
				ByteHelper.ToOutStr(effect)+", "+
				ByteHelper.ToOutStr(bind_pos)+", "+
				ByteHelper.ToOutStr(sound)+", "+
				ByteHelper.ToOutInt(buff_type)+", "+
				ByteHelper.ToOutInt(sub_type)+", "+
				ByteHelper.ToOutStr(buff_des)+", "+
				ByteHelper.ToOutInt(interval_time)+", "+
				ByteHelper.ToOutInt(duration)+", "+
				ByteHelper.ToOutInt(effect_1)+", "+
				ByteHelper.ToOutInt(callback_1)+", "+
				ByteHelper.ToOutInt(effect_2)+", "+
				ByteHelper.ToOutInt(callback_2)+", "+
				ByteHelper.ToOutInt(effect_3)+", "+
				ByteHelper.ToOutInt(callback_3)+", "+
				ByteHelper.ToOutInt(effect_4)+", "+
				ByteHelper.ToOutInt(effect_5)+", "+
				ByteHelper.ToOutInt(effect_6)+", "+
				ByteHelper.ToOutStr(buff_param)+", "+
				ByteHelper.ToOutBool(death_delete)+", "+
				ByteHelper.ToOutInt(target)+", "+
				ByteHelper.ToOutInt(country_limit)+", "+
				ByteHelper.ToOutInt(profession_limit)+", "+
				ByteHelper.ToOutInt(sex_limit)+", "+
				ByteHelper.ToOutInt(state_first)+", "+
				ByteHelper.ToOutInt(over_lay);
		}
	}
}

