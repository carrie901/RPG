﻿
namespace Summer
{
    #region Int Float String 

    



    #endregion

    public class TargetEventBuff : EventSetData
    {

        public BaseEntity target;
        public TargetEventBuff() { }
    }

    public class BuffLayerMaxEventBuff : EventSetData
    {

       /* public Buff buff;
        public BuffLayerMaxEventBuff() { }
        public override void Reset() { buff = null; }*/
    }


    public class EffectAttributeEvent : EventSetData
    {
        public BaseEntity target;
        public E_CharAttributeRegion _region;
        public float data;
        public EffectAttributeEvent() { }
        public override void Reset()
        {
            target = null;
            _region = E_CharAttributeRegion.none;
        }
    }

    public class EffectValueEvent : EventSetData
    {
        public BaseEntity target;
        public float _value;
        public float data;
        public EffectValueEvent() { }
        public override void Reset()
        {
            target = null;
        }
    }

}



