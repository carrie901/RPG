
using Summer;
using UnityEngine;

namespace Summer
{
    public class BuffFactoryMethod
    {
        static BuffFactoryMethod()
        {

        }

        public static BaseBuff Create(int buff_id)
        {
            TestBuffFactoryMethod.CreateData();
            BuffCnf buff_obj = StaticCnf.FindData<BuffCnf>(buff_id);
            BaseBuff buff = new BaseBuff(TestBuffFactoryMethod.info);
            return buff;
        }

    }

    public class TestBuffFactoryMethod
    {
        public static BuffTemplateInfo info;
        public static void CreateData()
        {
            Object obj = Resources.Load("test_buff_dat");
            info = obj as BuffTemplateInfo;
        }
    }
}