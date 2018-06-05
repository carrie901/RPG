
namespace Summer.AI
{
    public class BtAny 
    {
        public T As<T>() where T : BtAny
        {
            return this as T;
        }
    }

}
