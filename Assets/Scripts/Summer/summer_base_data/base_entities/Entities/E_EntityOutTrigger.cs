namespace Summer
{
	// TODO 概念 由外部触发，然后内部执行一些行为，比如我收到攻击了，我应该怎么办 可以变相的让E_BuffTrigger事件消失或者说更加纯粹华
    // 1. 比如播放动作 可以有外部触发播放一个动作，也有可能让内部触发播放一个动作
    // 2. 内部触发一个有生命周期的行为
    // 3. 这个和GameSystemEvent的差别在于 一个是全局性质的，一个是针对个人的
    public enum E_EntityOutTrigger
    {
        animation_event,                // 动作事件
    }
}