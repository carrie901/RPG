using System.IO;
public class avgCnf : BaseCsv
{
	 // avg_id组
	 public int avg_id;

	 // 引用下一AVG序号
	 public int avg_next_id;

	 // AVG剧情脚本
	 public string avc_storyboard_mylook;

	 // avg_type剧情类型（100101-100109为示例剧情配置）（1-3D场景剧情  2-UI对话剧情）
	 public int avg_type;

	 // AVG场景镜头坐标（类型1专用）
	 public int[] avg_scene_camera;

	 // 场景模型ID（同索引位置的模型id如果相同则沿用-没有模型的填0）（类型1专用）
	 public int[] avg_scene_model_id;

	 // 模型绑定特效（数字%特效ID$数字%特效ID，数字代表第几个模型）
	 public string[] avg_scene_model_effect;

	 // 场景模型起始点坐标（使用原本位置的填[stay]-此位置没有模型的填[none]）（类型1专用）
	 public string[] avg_scene_model_position_start;

	 // 场景模型终止点坐标（类型1专用）
	 public string[] avg_scene_model_position_end;

	 // 场景模型对应动作（类型1专用）所有动作是到终点时播放的
	 public string[] avg_scene_model_act;

	 // 终点动作伴随特效(类型1)
	 public int avg_scene_model_act_effect;

	 // 情绪气泡（显示在对应索引位置的场景剧情模型头上）（填心情预置名-没有的填[none]）（类型2专用）
	 public string[] avg_ui_bubble_prefab;

	 // UI层模型（模型 %左右$模型%左右）（1左2右）（类型2专用）
	 public string[] avg_ui_model_id;

	 // 对话人名和对话内容(人名：内容)（类型2专用）
	 public string avg_ui_conversation;

	 // 剧情道具ICON（类型2专用）
	 public string avg_ui_item;

	 // AVG特殊光照坐标
	 public int[] avg_scene_light_positon;

	 // AVG光照颜色
	 public string avg_scene_light_color;

	 // 调用场景ID
	 public int avg_scene_id;

	 // 剧情BGM
	 public int avg_bgm;

	 // avg语音
	 public int avg_cv;

	 // avg加载页
	 public string avg_loading_bg;

	 // avg加载页文字颜色（大标题，小标题，正文）
	 public string[] avg_words_color;

	 // avg概要文本
	 public string avg_loading_outline;

	 // 类型３专用停留时间(s)
	 public int[] avg_stop_times;

	public override int GetId()
	{
		return avg_id;
	}
	public override void InitByReader(BinaryReader reader)
	{
		avg_id = reader.ReadInt32();

		avg_next_id = reader.ReadInt32();

		avc_storyboard_mylook = reader.ReadString();

		avg_type = reader.ReadInt32();

		int length_avg_scene_camera = reader.ReadInt32();
		avg_scene_camera = new int[length_avg_scene_camera];
		for(int i = 0; i < length_avg_scene_camera; i++)
		{
		avg_scene_camera[i] = reader.ReadInt32();
		}

		int length_avg_scene_model_id = reader.ReadInt32();
		avg_scene_model_id = new int[length_avg_scene_model_id];
		for(int i = 0; i < length_avg_scene_model_id; i++)
		{
		avg_scene_model_id[i] = reader.ReadInt32();
		}

		int length_avg_scene_model_effect = reader.ReadInt32();
		avg_scene_model_effect = new string[length_avg_scene_model_effect];
		for(int i = 0; i < length_avg_scene_model_effect; i++)
		{
		avg_scene_model_effect[i] = reader.ReadString();
		}

		int length_avg_scene_model_position_start = reader.ReadInt32();
		avg_scene_model_position_start = new string[length_avg_scene_model_position_start];
		for(int i = 0; i < length_avg_scene_model_position_start; i++)
		{
		avg_scene_model_position_start[i] = reader.ReadString();
		}

		int length_avg_scene_model_position_end = reader.ReadInt32();
		avg_scene_model_position_end = new string[length_avg_scene_model_position_end];
		for(int i = 0; i < length_avg_scene_model_position_end; i++)
		{
		avg_scene_model_position_end[i] = reader.ReadString();
		}

		int length_avg_scene_model_act = reader.ReadInt32();
		avg_scene_model_act = new string[length_avg_scene_model_act];
		for(int i = 0; i < length_avg_scene_model_act; i++)
		{
		avg_scene_model_act[i] = reader.ReadString();
		}

		avg_scene_model_act_effect = reader.ReadInt32();

		int length_avg_ui_bubble_prefab = reader.ReadInt32();
		avg_ui_bubble_prefab = new string[length_avg_ui_bubble_prefab];
		for(int i = 0; i < length_avg_ui_bubble_prefab; i++)
		{
		avg_ui_bubble_prefab[i] = reader.ReadString();
		}

		int length_avg_ui_model_id = reader.ReadInt32();
		avg_ui_model_id = new string[length_avg_ui_model_id];
		for(int i = 0; i < length_avg_ui_model_id; i++)
		{
		avg_ui_model_id[i] = reader.ReadString();
		}

		avg_ui_conversation = reader.ReadString();

		avg_ui_item = reader.ReadString();

		int length_avg_scene_light_positon = reader.ReadInt32();
		avg_scene_light_positon = new int[length_avg_scene_light_positon];
		for(int i = 0; i < length_avg_scene_light_positon; i++)
		{
		avg_scene_light_positon[i] = reader.ReadInt32();
		}

		avg_scene_light_color = reader.ReadString();

		avg_scene_id = reader.ReadInt32();

		avg_bgm = reader.ReadInt32();

		avg_cv = reader.ReadInt32();

		avg_loading_bg = reader.ReadString();

		int length_avg_words_color = reader.ReadInt32();
		avg_words_color = new string[length_avg_words_color];
		for(int i = 0; i < length_avg_words_color; i++)
		{
		avg_words_color[i] = reader.ReadString();
		}

		avg_loading_outline = reader.ReadString();

		int length_avg_stop_times = reader.ReadInt32();
		avg_stop_times = new int[length_avg_stop_times];
		for(int i = 0; i < length_avg_stop_times; i++)
		{
		avg_stop_times[i] = reader.ReadInt32();
		}

	}
	public override void InitByWriter(BinaryWriter writer)
	{
		writer.Write(avg_id);

		writer.Write(avg_next_id);

		writer.Write(avc_storyboard_mylook);

		writer.Write(avg_type);

		int length_avg_scene_camera = avg_scene_camera.Length;
		writer.Write(length_avg_scene_camera);
		for(int i=0;i<length_avg_scene_camera; i++)
		{
				writer.Write(avg_scene_camera[i]);
		}

		int length_avg_scene_model_id = avg_scene_model_id.Length;
		writer.Write(length_avg_scene_model_id);
		for(int i=0;i<length_avg_scene_model_id; i++)
		{
				writer.Write(avg_scene_model_id[i]);
		}

		int length_avg_scene_model_effect = avg_scene_model_effect.Length;
		writer.Write(length_avg_scene_model_effect);
		for(int i=0;i<length_avg_scene_model_effect; i++)
		{
				writer.Write(avg_scene_model_effect[i]);
		}

		int length_avg_scene_model_position_start = avg_scene_model_position_start.Length;
		writer.Write(length_avg_scene_model_position_start);
		for(int i=0;i<length_avg_scene_model_position_start; i++)
		{
				writer.Write(avg_scene_model_position_start[i]);
		}

		int length_avg_scene_model_position_end = avg_scene_model_position_end.Length;
		writer.Write(length_avg_scene_model_position_end);
		for(int i=0;i<length_avg_scene_model_position_end; i++)
		{
				writer.Write(avg_scene_model_position_end[i]);
		}

		int length_avg_scene_model_act = avg_scene_model_act.Length;
		writer.Write(length_avg_scene_model_act);
		for(int i=0;i<length_avg_scene_model_act; i++)
		{
				writer.Write(avg_scene_model_act[i]);
		}

		writer.Write(avg_scene_model_act_effect);

		int length_avg_ui_bubble_prefab = avg_ui_bubble_prefab.Length;
		writer.Write(length_avg_ui_bubble_prefab);
		for(int i=0;i<length_avg_ui_bubble_prefab; i++)
		{
				writer.Write(avg_ui_bubble_prefab[i]);
		}

		int length_avg_ui_model_id = avg_ui_model_id.Length;
		writer.Write(length_avg_ui_model_id);
		for(int i=0;i<length_avg_ui_model_id; i++)
		{
				writer.Write(avg_ui_model_id[i]);
		}

		writer.Write(avg_ui_conversation);

		writer.Write(avg_ui_item);

		int length_avg_scene_light_positon = avg_scene_light_positon.Length;
		writer.Write(length_avg_scene_light_positon);
		for(int i=0;i<length_avg_scene_light_positon; i++)
		{
				writer.Write(avg_scene_light_positon[i]);
		}

		writer.Write(avg_scene_light_color);

		writer.Write(avg_scene_id);

		writer.Write(avg_bgm);

		writer.Write(avg_cv);

		writer.Write(avg_loading_bg);

		int length_avg_words_color = avg_words_color.Length;
		writer.Write(length_avg_words_color);
		for(int i=0;i<length_avg_words_color; i++)
		{
				writer.Write(avg_words_color[i]);
		}

		writer.Write(avg_loading_outline);

		int length_avg_stop_times = avg_stop_times.Length;
		writer.Write(length_avg_stop_times);
		for(int i=0;i<length_avg_stop_times; i++)
		{
				writer.Write(avg_stop_times[i]);
		}

	}
}
