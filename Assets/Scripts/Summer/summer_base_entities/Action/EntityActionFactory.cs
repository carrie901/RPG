
namespace Summer
{
    public class EntityActionFactory
    {
        //public static TypeFactory _factory = new TypeFactory();
        public static T Pop<T>() where T : I_EntityAction, new()
        {
            T t = new T();
            return t;
        }

        public static void Push<T>(T t) where T : I_EntityAction
        {

        }

        public static void OnAction<T>(BaseEntity entity, EventSetData data) where T : I_EntityAction, new()
        {
            I_EntityAction t = Pop<T>();
            t.OnAction(entity, data);
            data.Reset();
            
        }
    }
}

