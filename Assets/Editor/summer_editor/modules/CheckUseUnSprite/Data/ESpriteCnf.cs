using System.IO;
using System.Text.RegularExpressions;

namespace SummerEditor
{
    /// <summary>
    /// 图集的信息 图集的guid值 路径 packingtag值
    /// </summary>
	public class ESpriteCnf
    {
        public string guid;                 // sprite的guid值
        public string path;                 // 
        public string packingtag;


        public void SetPath(string full_path)
        {
            // //Regex packingtag_reg = new Regex(@"  spritePackingTag: ([a-f0-9]{32})");
            path = full_path.Substring(0, full_path.Length - 5);
            string meta_text = File.ReadAllText(full_path);
            packingtag = ESpriteHelper.FindSpritePackingTag(meta_text);

            Regex guid_reg = new Regex(@"guid: ([a-f0-9]{32})");
            Match matcher = guid_reg.Match(meta_text);

            guid = matcher.Groups[1].Value;
        }

    }
}
