
namespace Summer
{
    public class EventSetEffectData : EventSetData
    {
        public BaseEntity entity;

        public override void Push()
        {
            entity = null;
        }
    }
}
