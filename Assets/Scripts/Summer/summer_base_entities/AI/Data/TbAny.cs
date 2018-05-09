
namespace Summer.AI
{
    public class TbAny 
    {
        public T As<T>() where T : TbAny
        {
            return this as T;
        }
    }

}
