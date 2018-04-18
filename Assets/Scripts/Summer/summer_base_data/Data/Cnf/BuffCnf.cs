using System.IO;
public class BuffCnf : BaseCsv
{
	 // ID
	 public int id;

    // 名称
    //replace start
    /// <summary>
    /// buff组编号
    /// </summary>
    public string groupID;

    /// <summary>
    /// 名称
    /// </summary>
    public string name;

    /// <summary>
    /// 名称美术字
    /// </summary>
    public string buff_name_res;

    /// <summary>
    /// 等级
    /// </summary>
    public int level;

    /// <summary>
    /// 描述[给策划自己看的]
    /// </summary>
    public string desc;

    /// <summary>
    /// 是否显示图标
    /// </summary>
    public int show_icon;

    /// <summary>
    /// 图标
    /// </summary>
    public string icon;

    /// <summary>
    /// buff持续时间计算类型
    /// </summary>
    public int time_type;

    /// <summary>
    /// Buff类型
    /// </summary>
    public int buff_type;

    /// <summary>
    /// 持续时间[单位ms]
    /// </summary>
    public int duration;

    /// <summary>
    /// 效果类型
    /// </summary>
    public int effect_type;

    /// <summary>
    /// 效果赋值
    /// </summary>
    public string effect_value;

    /// <summary>
    /// 死亡是否消失
    /// </summary>
    public bool death_delete;

    /// <summary>
    /// 叠加层数
    /// </summary>
    public int over_lay;

    /// <summary>
    /// 特效
    /// </summary>
    public string effect;

    /// <summary>
    /// 特效挂载点
    /// </summary>
    public string bind_pos;

    /// <summary>
    /// 效果改变模型颜色
    /// </summary>
    public int model_effect;

    /// <summary>
    /// 效果改变武器颜色
    /// </summary>
    public int weapon_effect;

    /// <summary>
    /// 音效
    /// </summary>
    public string sound;

    public override int GetId()
	{
		return id;
	}
	public override void InitByReader(BinaryReader reader)
	{
		

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		

	}
}
