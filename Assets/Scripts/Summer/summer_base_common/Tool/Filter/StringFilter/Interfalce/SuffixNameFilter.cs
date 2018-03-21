using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 后缀民过滤
    /// </summary>
    public class SuffixNameFilter : I_ContentFilter
    {
        public List<string> _filter_set = new List<string>();

        public SuffixNameFilter()
        {
        }

        public SuffixNameFilter(string suffix)
        {
            AddSuffix(suffix);
        }

        public void AddSuffix(List<string> suffixs)
        {
            int length = suffixs.Count;
            for (int i = 0; i < length; i++)
            {
                AddSuffix(suffixs[i]);
            }
        }

        public void AddSuffix(string suffix)
        {
            _filter_set.Add(suffix);
        }
        public bool FilterContent(string path)
        {
            bool result = false;
            int length = _filter_set.Count;
            for (int i = 0; i < length; i++)
            {
                if (path.EndsWith(_filter_set[i]))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
