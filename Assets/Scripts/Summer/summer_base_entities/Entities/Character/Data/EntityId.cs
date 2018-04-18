namespace Summer
{
    public class EntityId
    {
        static uint iidex = 0;
        protected uint iid;

        public uint Eid { get { return iid; } }

        public EntityId()
        {
            iid = iidex;
            iidex++;
        }



        public bool EqualId(EntityId id)
        {
            return iid == id.iid;
        }
    }

}
