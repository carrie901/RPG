

public class TSingleton<T> where T : class, new()
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    protected TSingleton()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {

    }
}
