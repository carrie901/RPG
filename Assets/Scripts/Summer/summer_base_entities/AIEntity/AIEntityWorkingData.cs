
namespace Summer.AI
{
    public class AIEntityWorkingData : BtWorkingData
    {
        public BtEntityAi EntityAi { get; set; }
        //public Transform EntityTrans { get; set; }
        //public Animator EntityAnimator { get; set; }
        public float GameTime { get; set; }
        public float DeltaTime { get; set; }
    }
}
