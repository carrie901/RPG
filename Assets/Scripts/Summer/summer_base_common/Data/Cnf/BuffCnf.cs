using System.IO;
public class BuffCnf : BaseCsv
{
	 // ID
	 public int id;

	 // 名称
	 public string name;

	 // 描述
	 public string dsec;

	 // 图标
	 public string icon;

	 // 特效
	 public string effect;

	 // 音效
	 public string sound;

	 // Buff类型
	 public int buff_type;

	 // Buff子类型
	 public int sub_type;

	 // 作用间隔时间
	 public int interval_time;

	 // 持续时间
	 public int duration;

	 // 参数1
	 public string param1;

	 // 参数2
	 public string param2;

	 // 参数3
	 public string param3;

	 // 参数4
	 public string param4;

	 // 参数5
	 public string param5;

	 // 参数6
	 public string param6;

	 // 参数7
	 public string param7;

	 // 死亡是否消失
	 public bool death_delete;

	 // 作用对象
	 public int target;

	 // 优先作用
	 public int state_first;

	 // 叠加层数
	 public int over_lay;

	 // 结束后获得新buff
	 public int end_buff;

	public override int GetId()
	{
		return id;
	}
	public override void InitByReader(BinaryReader reader)
	{
		id = reader.ReadInt32();

		name = reader.ReadString();

		dsec = reader.ReadString();

		icon = reader.ReadString();

		effect = reader.ReadString();

		sound = reader.ReadString();

		buff_type = reader.ReadInt32();

		sub_type = reader.ReadInt32();

		interval_time = reader.ReadInt32();

		duration = reader.ReadInt32();

		param1 = reader.ReadString();

		param2 = reader.ReadString();

		param3 = reader.ReadString();

		param4 = reader.ReadString();

		param5 = reader.ReadString();

		param6 = reader.ReadString();

		param7 = reader.ReadString();

death_delete = reader.ReadBoolean();

		target = reader.ReadInt32();

		state_first = reader.ReadInt32();

		over_lay = reader.ReadInt32();

		end_buff = reader.ReadInt32();

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(id);

		writer.Write(name);

		writer.Write(dsec);

		writer.Write(icon);

		writer.Write(effect);

		writer.Write(sound);

		writer.Write(buff_type);

		writer.Write(sub_type);

		writer.Write(interval_time);

		writer.Write(duration);

		writer.Write(param1);

		writer.Write(param2);

		writer.Write(param3);

		writer.Write(param4);

		writer.Write(param5);

		writer.Write(param6);

		writer.Write(param7);

		writer.Write(death_delete);

		writer.Write(target);

		writer.Write(state_first);

		writer.Write(over_lay);

		writer.Write(end_buff);

	}
}
