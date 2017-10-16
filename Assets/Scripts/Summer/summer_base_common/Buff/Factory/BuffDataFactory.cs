
namespace Summer
{
    /// <summary>
    /// 工厂模式
    /// </summary>
    public class BuffDataFactory
    {
        public static T Push<T>() where T : EventBuffSetData, new()
        {
            T t = new T();
            return t;
        }

        public static void Pop<T>(T t) where T : EventBuffSetData
        {
            t.Reset();
        }
    }

}

