namespace Summer
{
    /// <summary>
    /// 转换枚举：为系统中出现的状态转换分配标签
    /// </summary>
    public enum E_Transition
    {
        null_transition = 0, // Use this transition to represent a non-existing transition in your system
    }

    /// <summary>
    /// 状态枚举：StateID作为你的游戏中的状态id，你可以引用真实的状态类,但是真实的状态类不应该被代码直接访问到，
    /// 不要改变(移除)NullTransstion标签，FsmSystem会使用到
    /// </summary>
    public enum E_StateId
    {
        null_state_id = 0, // Use this ID to represent a non-existing State in your system	
    }
}
