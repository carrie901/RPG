
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 过滤器 优先级别
    /// </summary>
    public interface I_TargetSelector
    {

        void FilterTarget(List<BaseEntity> targets);

    }

    /// <summary>
    /// 目标过滤
    /// </summary>
    public class TargetSelector : I_TargetSelector
    {
        public List<I_TargetSelector> _lists = new List<I_TargetSelector>(4);
        public void AddFilter(I_TargetSelector selector)
        {
            _lists.Add(selector);
        }

        public void FilterTarget(List<BaseEntity> targets)
        {
            int length = _lists.Count;
            for (int i = 0; i < length; i++)
            {
                _lists[i].FilterTarget(targets);
            }
        }
    }

}
