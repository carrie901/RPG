namespace Summer
{
    public interface I_EntityCommnad
    {
        //void OnEnter();
        void OnUpdate(BaseEntity entity_movement, float dt);
        //void OnExit();
        bool Finish();
    }
}
