using System.IO;
public class attribute_type_configCnf : BaseCsv
{
	 // 属性类型id
	 public int type_id;

	 // 属性名称自己看
	 public string name_mylook;

	 // 本身是否是值的概念，1是值，0是（率）
	 public bool is_value;

	 // 属性索引text表
	 public string attribute_text;

	 // 战斗时是否飘美术字(1显示、0不显示）
	 public bool is_observe;

	 // 模型身上箭头是否显示
	 public bool is_observe_arrow;

	public override int GetId()
	{
		return type_id;
	}
	public override void InitByReader(BinaryReader reader)
	{
		type_id = reader.ReadInt32();

		name_mylook = reader.ReadString();

is_value = reader.ReadBoolean();

		attribute_text = reader.ReadString();

is_observe = reader.ReadBoolean();

is_observe_arrow = reader.ReadBoolean();

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(type_id);

		writer.Write(name_mylook);

		writer.Write(is_value);

		writer.Write(attribute_text);

		writer.Write(is_observe);

		writer.Write(is_observe_arrow);

	}
}
