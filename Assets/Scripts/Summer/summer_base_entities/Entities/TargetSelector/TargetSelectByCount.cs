
using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 根据数量来过滤
    /// </summary>
    public class TargetSelectByCount : I_TargetSelector
    {
        public int count;
        public void FilterTarget(List<BaseEntity> targets)
        {
            int length = targets.Count;
            for (int i = length; i >= count; i++)
            {
                targets.RemoveAt(i);
            }
        }

        public void Init(TextNode textnode)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 根据范围来过滤
    /// </summary>
    public class TargetSelectByArea : I_TargetSelector
    {
        public void FilterTarget(List<BaseEntity> targets)
        {

        }

        public void Init(TextNode textnode)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 根据目标类型
    /// </summary>
    public class TargetSelectByType : I_TargetSelector
    {
        public void FilterTarget(List<BaseEntity> targets)
        {
            
        }

        public void Init(TextNode textnode)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 目标职业
    /// </summary>
    public class TargetSelectByCareer : I_TargetSelector
    {
        public void FilterTarget(List<BaseEntity> targets)
        {
            
        }

        public void Init(TextNode textnode)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 目标性别
    /// </summary>
    public class TargetSelectBySex : I_TargetSelector
    {
        public void FilterTarget(List<BaseEntity> targets)
        {
            
        }

        public void Init(TextNode textnode)
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// 目标流派（0无  1忍者）
    /// </summary>
    public class TargetSelectByPai : I_TargetSelector
    {
        public void FilterTarget(List<BaseEntity> targets)
        {
            
        }

        public void Init(TextNode textnode)
        {
            throw new System.NotImplementedException();
        }
    }
}
