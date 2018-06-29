
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 过滤器 优先级别
    /// </summary>
    public interface I_TargetSelector
    {
        void FilterTarget(List<BaseEntity> targets);
        void Init(TextNode textnode);

    }
}
