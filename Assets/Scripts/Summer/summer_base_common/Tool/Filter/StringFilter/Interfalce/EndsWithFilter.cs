using System.Collections.Generic;

namespace Summer
{

    public class NameFilter : I_ContentFilter
    {
        public List<string> _filter_set = new List<string>();

        public NameFilter()
        {
        }

        public NameFilter(string suffix)
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

        #region 

        public virtual bool FilterContent(string path)
        {
            return false;
        }

        #endregion
    }

    /// <summary>
    /// 后缀民过滤
    /// </summary>
    public class EndsWithFilter : NameFilter
    {
        #region 

        public override bool FilterContent(string path)
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

        #endregion
    }

    public class StartsWithFilter : NameFilter
    {
        public StartsWithFilter(string suffix)
        {
            AddSuffix(suffix);
        }
        public override bool FilterContent(string path)
        {
            bool result = false;
            int length = _filter_set.Count;
            for (int i = 0; i < length; i++)
            {
                if (path.StartsWith(_filter_set[i]))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }

    public class NoEndsWithFilter : NameFilter
    {
        public NoEndsWithFilter(string suffix)
        {
            AddSuffix(suffix);
        }
        public override bool FilterContent(string path)
        {
            bool result = false;
            int length = _filter_set.Count;
            for (int i = 0; i < length; i++)
            {
                if (!path.EndsWith(_filter_set[i]))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
