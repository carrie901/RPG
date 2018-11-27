
namespace Summer
{
    public class EventSetEffectData : EventSetData
    {
        public BaseEntity _entity;

        public override void Push()
        {
            _entity = null;
        }
    }
}
