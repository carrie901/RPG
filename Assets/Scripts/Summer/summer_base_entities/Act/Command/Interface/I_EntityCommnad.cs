namespace Summer
{
    public interface I_EntityCommnad
    {
        //void OnEnter();
        void OnUpdate(BaseEntity entityMovement, float dt);
        //void OnExit();
        bool Finish();
    }
}
