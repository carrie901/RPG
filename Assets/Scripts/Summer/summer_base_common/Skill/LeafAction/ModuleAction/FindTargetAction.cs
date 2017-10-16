
namespace Summer
{

    /// <summary>
    /// 查找目标 
    /// TODO 通过依赖连来查找目标  范围/敌友/等等其他条件 一次次把结果传递
    /// </summary>
    public class FindTargetAction : SkillNodeAction
    {
        public const string DES = "查找目标 ";
        //TODO 希望能通过抽象来描述查找目标
        public float radius;        //距离
        public float degree;        //角度
        public override void OnEnter()
        {
            LogEnter();
        }

        public override void OnExit()
        {
            LogExit();
        }

        public override void OnUpdate(float dt)
        {
            base.OnUpdate(dt);
        }

        public override string ToDes()
        {
            return DES;
        }
    }

}
