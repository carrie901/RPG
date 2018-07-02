/*
//=============================================================================
// Author : mashao
// CreateTime : 2018-1-10 19:35:10
// FileName : EffectChangeModel.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 模型修改 
    /// 提供预设 1 2 3，4 每一种代表一组数据
    /// 一组数据包括2个值
    /// </summary>
    public class EffectChangeModel : EffectModelState
    {
        public override void _on_change_state()
        {
            Log("人物模型修改");
        }

        public override void _on_on_reverse_state()
        {
            Log("人物模型还原");
        }
    }
}
*/
