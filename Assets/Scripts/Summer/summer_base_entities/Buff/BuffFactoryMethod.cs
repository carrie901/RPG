
using Summer;

public class BuffFactoryMethod
{
    static BuffFactoryMethod()
    {

    }

    public static Buff Create(int buff_id)
    {
        Buff buff = new BuffNormal();
        BuffCnf buff_obj = StaticCnf.FindData<BuffCnf>(buff_id);
        if (buff_obj != null)
            buff.Init(buff_obj);
        return buff;
    }

}
