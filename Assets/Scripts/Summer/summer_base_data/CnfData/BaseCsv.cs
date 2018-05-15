
using System.IO;

namespace Summer
{
    public class BaseCsv
    {
        public virtual int GetId()
        {
            return 0;
        }

        public virtual void InitByReader(BinaryReader reader)
        {

        }

        public virtual void InitByWriter(BinaryWriter writer)
        {

        }
    }
}



