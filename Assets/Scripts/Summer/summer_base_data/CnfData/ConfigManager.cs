using System;
using System.Collections.Generic;
using System.IO;
using Summer;
public class ConfigManager
{

	public static void ReadLocalConfig()
	{
	StaticCnf.Clear();
		int length = 0;
		Dictionary<string, BaseCsvInfo> csv_infos = CnfHelper.LoadFileContent();
		BaseCsvInfo info = null;

		info = csv_infos["BuffCnf"];
		Dictionary<int, BuffCnf> buffcnf = new Dictionary<int, BuffCnf>();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			BuffCnf tmp = new BuffCnf();
			tmp.ToLocalRead(info.datas[i]);
			buffcnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(buffcnf);

		info = csv_infos["BuffEffectCnf"];
		Dictionary<int, BuffEffectCnf> buffeffectcnf = new Dictionary<int, BuffEffectCnf>();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			BuffEffectCnf tmp = new BuffEffectCnf();
			tmp.ToLocalRead(info.datas[i]);
			buffeffectcnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(buffeffectcnf);

		info = csv_infos["HeroInfoCnf"];
		Dictionary<int, HeroInfoCnf> heroinfocnf = new Dictionary<int, HeroInfoCnf>();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			HeroInfoCnf tmp = new HeroInfoCnf();
			tmp.ToLocalRead(info.datas[i]);
			heroinfocnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(heroinfocnf);

		info = csv_infos["SpellInfoCnf"];
		Dictionary<int, SpellInfoCnf> spellinfocnf = new Dictionary<int, SpellInfoCnf>();
		length = info.datas.Count;
		for (int i = 0; i < length; i++)
		{
			SpellInfoCnf tmp = new SpellInfoCnf();
			tmp.ToLocalRead(info.datas[i]);
			spellinfocnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(spellinfocnf);
	}
	public static void ReadByteConfig()
	{
	    StaticCnf.Clear();
		byte[] bytes = ResManager.instance.LoadByte(CnfConst.DATA_BYTE_NAME, E_GameResType.text_asset);
		MemoryStream ms = new MemoryStream(bytes);
		BinaryReader br = new BinaryReader(ms);
		int length = 0;

		length = br.ReadInt32();
		Dictionary<int, BuffCnf> buffcnf = new Dictionary<int, BuffCnf>();
		for (int i = 0; i < length; i++)
		{
			BuffCnf tmp = new BuffCnf();
			tmp.ToByteRead(br);
			buffcnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(buffcnf);

		length = br.ReadInt32();
		Dictionary<int, BuffEffectCnf> buffeffectcnf = new Dictionary<int, BuffEffectCnf>();
		for (int i = 0; i < length; i++)
		{
			BuffEffectCnf tmp = new BuffEffectCnf();
			tmp.ToByteRead(br);
			buffeffectcnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(buffeffectcnf);

		length = br.ReadInt32();
		Dictionary<int, HeroInfoCnf> heroinfocnf = new Dictionary<int, HeroInfoCnf>();
		for (int i = 0; i < length; i++)
		{
			HeroInfoCnf tmp = new HeroInfoCnf();
			tmp.ToByteRead(br);
			heroinfocnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(heroinfocnf);

		length = br.ReadInt32();
		Dictionary<int, SpellInfoCnf> spellinfocnf = new Dictionary<int, SpellInfoCnf>();
		for (int i = 0; i < length; i++)
		{
			SpellInfoCnf tmp = new SpellInfoCnf();
			tmp.ToByteRead(br);
			spellinfocnf.Add(tmp.id, tmp); 
		}
		StaticCnf.Add(spellinfocnf);
		ms.Flush();
		ms.Close();
		br.Close();
		ms.Dispose();
	}
	public static void WriteByteConfig()
	{
		FileInfo file_info = new FileInfo(CnfConst.data_byte_path);
			if (file_info.Exists)
		file_info.Delete();
		FileStream file_stream = file_info.Create();
		BinaryWriter bw = new BinaryWriter(file_stream);
		int length = 0;

		List<BuffCnf> tmp_buffcnf = new List<BuffCnf>(StaticCnf.FindMap<BuffCnf>().Values);
		length = tmp_buffcnf.Count;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			BuffCnf data = tmp_buffcnf[i];
			data.ToByteWrite(bw);
		}

		List<BuffEffectCnf> tmp_buffeffectcnf = new List<BuffEffectCnf>(StaticCnf.FindMap<BuffEffectCnf>().Values);
		length = tmp_buffeffectcnf.Count;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			BuffEffectCnf data = tmp_buffeffectcnf[i];
			data.ToByteWrite(bw);
		}

		List<HeroInfoCnf> tmp_heroinfocnf = new List<HeroInfoCnf>(StaticCnf.FindMap<HeroInfoCnf>().Values);
		length = tmp_heroinfocnf.Count;
		bw.Write(length);
		for (int i = 0; i < length; i++)
		{
			HeroInfoCnf data = tmp_heroinfocnf[i];
			data.ToByteWrite(bw);
		}

		List<SpellInfoCnf> tmp_spellinfocnf = new List<SpellInfoCnf>(StaticCnf.FindMap<SpellInfoCnf>().Values);
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
