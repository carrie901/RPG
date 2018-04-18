
namespace Summer
{
    /// <summary>
    /// 冰冻效果
    /// </summary>
    public class EffectFrozen : EffectState
    {

        public override void _on_change_state()
        {
           /* Log("状态改变--->开启冰冻效果");
            _owner.AiController.AttachBehavior(CharacterBehavior.Ice,5);
            _owner.Break();*/
        }

        public override void _on_on_reverse_state()
        {
            Log("状态还原--->解除冰冻效果");
        }


    }
}

