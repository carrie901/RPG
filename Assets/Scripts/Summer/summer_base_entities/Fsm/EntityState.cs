namespace Summer
{
    public abstract class EntityState : FsmState
    {
        public BaseEntity entity;

        /*public virtual bool CanReceiveMove()
        {
            return false;
        }

        public virtual bool CanReceiveAttack()
        {
            return false;
        }

        public virtual bool CanReceiveSkill()
        {
            return false;
        }
        
        public void SetCommand(I_EntityCommnad command)
        {
            command.OnCommand();
        }*/

    }

}