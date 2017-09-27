using System;
using System.IO;
public class StaticCnfLoader
{
	public static void LoadAllCsvFile()
	{
		StaticCnf.Add(CsvLoader.LoadFile<areaCnf>("areaCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<attribute_type_configCnf>("attribute_type_configCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<avgCnf>("avgCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<BatchMonsterInfoCnf>("BatchMonsterInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<BuffCnf>("BuffCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<HeroInfoCnf>("HeroInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<MonsterInfoCnf>("MonsterInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<SkillTypeCDListCnf>("SkillTypeCDListCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<SpaceInfoCnf>("SpaceInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadFile<SpellInfoCnf>("SpellInfoCnf"));
	}

	public static void LoadAllCsvBinary()
	{
		StaticCnf.Add(CsvLoader.LoadBinary<areaCnf>("areaCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<attribute_type_configCnf>("attribute_type_configCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<avgCnf>("avgCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<BatchMonsterInfoCnf>("BatchMonsterInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<BuffCnf>("BuffCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<HeroInfoCnf>("HeroInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<MonsterInfoCnf>("MonsterInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<SkillTypeCDListCnf>("SkillTypeCDListCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<SpaceInfoCnf>("SpaceInfoCnf"));
		StaticCnf.Add(CsvLoader.LoadBinary<SpellInfoCnf>("SpellInfoCnf"));
	}
	public static void WriteAllCsvBinary(string root_path)
	{
		SaveBinaryFile<areaCnf>(root_path);
		SaveBinaryFile<attribute_type_configCnf>(root_path);
		SaveBinaryFile<avgCnf>(root_path);
		SaveBinaryFile<BatchMonsterInfoCnf>(root_path);
		SaveBinaryFile<BuffCnf>(root_path);
		SaveBinaryFile<HeroInfoCnf>(root_path);
		SaveBinaryFile<MonsterInfoCnf>(root_path);
		SaveBinaryFile<SkillTypeCDListCnf>(root_path);
		SaveBinaryFile<SpaceInfoCnf>(root_path);
		SaveBinaryFile<SpellInfoCnf>(root_path);
	}
	public static void SaveBinaryFile<T>(string path) where T : BaseCsv
	{
		Type t = typeof(T);
		string name = t.Name;
		FileStream fs = new FileStream(path + name+".bytes", FileMode.Create);
		BinaryWriter bw = new BinaryWriter(fs);
		CsvLoader.WriteBinary<T>(StaticCnf.FindMap<T>(), bw);
		bw.Flush();
		bw.Close();
		fs.Close();
	}
}
