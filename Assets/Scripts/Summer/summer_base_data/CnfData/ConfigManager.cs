using System;
using System.Collections.Generic;
using System.IO;
using Summer;
public class ConfigManager
{
	public static ConfigManager Instance = new ConfigManager();
	public Dictionary<int, BuffCnf> buffcnf = new Dictionary<int, BuffCnf>();
	public Dictionary<int, BuffEffectCnf> buffeffectcnf = new Dictionary<int, BuffEffectCnf>();
	public Dictionary<int, HeroInfoCnf> heroinfocnf = new Dictionary<int, HeroInfoCnf>();
	public Dictionary<int, SpellInfoCnf> spellinfocnf = new Dictionary<int, SpellInfoCnf>();

	public void ReadLocalConfig()
	{
		int length = 0;
		Dictionary<string, BaseCsvInfo> csv_infos = CodeGeneratorHelperE.LoadFileContent();
		BaseCsvInfo info = null;

		info = csv_infos["BuffCnf"];
		buffcnf.Clear();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			BuffCnf tmp = new BuffCnf();
			tmp.ToLocalRead(info.datas[i]);
			buffcnf.Add(tmp.id, tmp); 
		}

		info = csv_infos["BuffEffectCnf"];
		buffeffectcnf.Clear();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			BuffEffectCnf tmp = new BuffEffectCnf();
			tmp.ToLocalRead(info.datas[i]);
			buffeffectcnf.Add(tmp.id, tmp); 
		}

		info = csv_infos["HeroInfoCnf"];
		heroinfocnf.Clear();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			HeroInfoCnf tmp = new HeroInfoCnf();
			tmp.ToLocalRead(info.datas[i]);
			heroinfocnf.Add(tmp.id, tmp); 
		}

		info = csv_infos["SpellInfoCnf"];
		spellinfocnf.Clear();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			SpellInfoCnf tmp = new SpellInfoCnf();
			tmp.ToLocalRead(info.datas[i]);
			spellinfocnf.Add(tmp.id, tmp); 
		}
	}
	public void ReadByteConfig()
	{
		byte[] bytes = ResManager.instance.LoadByte(CodeGeneratorConstE.data_byte_path, E_GameResType.quanming);
		MemoryStream ms = new MemoryStream(bytes);
		BinaryReader br = new BinaryReader(ms);
		int length = 0;

		length = br.ReadInt32();
		buffcnf.Clear();
		for (int i = 0; i < length; i++)
		{
			BuffCnf tmp = new BuffCnf();
			tmp.ToByteRead(br);
			buffcnf.Add(tmp.id, tmp); 
		}

		length = br.ReadInt32();
		buffeffectcnf.Clear();
		for (int i = 0; i < length; i++)
		{
			BuffEffectCnf tmp = new BuffEffectCnf();
			tmp.ToByteRead(br);
			buffeffectcnf.Add(tmp.id, tmp); 
		}

		length = br.ReadInt32();
		heroinfocnf.Clear();
		for (int i = 0; i < length; i++)
		{
			HeroInfoCnf tmp = new HeroInfoCnf();
			tmp.ToByteRead(br);
			heroinfocnf.Add(tmp.id, tmp); 
		}

		length = br.ReadInt32();
		spellinfocnf.Clear();
		for (int i = 0; i < length; i++)
		{
			SpellInfoCnf tmp = new SpellInfoCnf();
			tmp.ToByteRead(br);
			spellinfocnf.Add(tmp.id, tmp); 
		}
		ms.Flush();
		ms.Close();
		br.Close();
		ms.Dispose();
	}
	public void WriteByteConfig()
	{
		FileInfo file_info = new FileInfo(CodeGeneratorConstE.data_byte_path);
			if (file_info.Exists)
		file_info.Delete();
		FileStream file_stream = file_info.Create();
		BinaryWriter bw = new BinaryWriter(file_stream);
		int length = 0;

		List<BuffCnf> tmp_buffcnf = new List<BuffCnf>(buffcnf.Values);
		length = tmp_buffcnf.Count;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			BuffCnf data = tmp_buffcnf[i];
			data.ToByteWrite(bw);
		}

		List<BuffEffectCnf> tmp_buffeffectcnf = new List<BuffEffectCnf>(buffeffectcnf.Values);
		length = tmp_buffeffectcnf.Count;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			BuffEffectCnf data = tmp_buffeffectcnf[i];
			data.ToByteWrite(bw);
		}

		List<HeroInfoCnf> tmp_heroinfocnf = new List<HeroInfoCnf>(heroinfocnf.Values);
		length = tmp_heroinfocnf.Count;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			HeroInfoCnf data = tmp_heroinfocnf[i];
			data.ToByteWrite(bw);
		}

		List<SpellInfoCnf> tmp_spellinfocnf = new List<SpellInfoCnf>(spellinfocnf.Values);
		length = tmp_spellinfocnf.Count;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			SpellInfoCnf data = tmp_spellinfocnf[i];
			data.ToByteWrite(bw);
		}
		file_stream.Flush();
		file_stream.Close();
		bw.Close();
		file_stream.Dispose();
	}
}
