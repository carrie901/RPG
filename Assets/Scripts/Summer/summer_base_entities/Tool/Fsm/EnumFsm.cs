namespace Summer
{

    /// <summary>
    /// 状态枚举：StateID作为你的游戏中的状态id，你可以引用真实的状态类,但是真实的状态类不应该被代码直接访问到，
    /// 不要改变(移除)NullTransstion标签，FsmSystem会使用到
    /// </summary>
    public enum E_StateId
    {
        //null_state_id = 0, // Use this ID to represent a non-existing State in your system	
        skill,
        attack,
        die,
        hurt,
        idle,
        move,
        sleep,
    }
}
