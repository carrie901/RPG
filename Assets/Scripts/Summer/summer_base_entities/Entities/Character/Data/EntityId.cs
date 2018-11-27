namespace Summer
{
    public class EntityId
    {
        static int Iidex = 0;

        public static int Get()
        {
            return Iidex++;
        }
    }
}
