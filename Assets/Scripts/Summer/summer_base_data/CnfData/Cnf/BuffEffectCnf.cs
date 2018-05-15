using System.Collections.Generic;
using System.IO;
namespace Summer
{
	/// <summary>
	/// 
	/// </summary>
	public class BuffEffectCnf : BaseCsv
	{
		/// <summary>
		/// ID
		/// </summary>
		public int id;

		/// <summary>
		/// 描述
		/// </summary>
		public string dsec;

		/// <summary>
		/// 效果类型
		/// </summary>
		public int type;

		/// <summary>
		/// 效果子类型
		/// </summary>
		public int sub_type;

		/// <summary>
		/// 参数1
		/// </summary>
		public string param1;


		//属性Local读取
		public void ToLocalRead(List<string> contents)
		{
			id = ByteHelper.ToInt(contents[0]);
			dsec = ByteHelper.ToStr(contents[1]);
			type = ByteHelper.ToInt(contents[2]);
			sub_type = ByteHelper.ToInt(contents[3]);
			param1 = ByteHelper.ToStr(contents[4]);
		}

		//属性Byte读取
		public void ToByteRead(BinaryReader br)
		{
			id = ByteHelper.ReadInt(br);
			dsec = ByteHelper.ReadString(br);
			type = ByteHelper.ReadInt(br);
			sub_type = ByteHelper.ReadInt(br);
			param1 = ByteHelper.ReadString(br);
		}

		//属性Byte写入
		public void ToByteWrite(BinaryWriter bw)
		{
			ByteHelper.WriteInt(bw,id);
			ByteHelper.WriteString(bw,dsec);
			ByteHelper.WriteInt(bw,type);
			ByteHelper.WriteInt(bw,sub_type);
			ByteHelper.WriteString(bw,param1);
		}
		public string ToLocalWrite()
		{
			return ByteHelper.ToOutInt(id)+", "+
				ByteHelper.ToOutStr(dsec)+", "+
				ByteHelper.ToOutInt(type)+", "+
				ByteHelper.ToOutInt(sub_type)+", "+
				ByteHelper.ToOutStr(param1);
		}
	}
}

