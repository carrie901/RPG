
namespace Summer
{
    public class EffectStateData : I_EffectParam
    {
        public int rate;            //几率
        public string value_text = string.Empty;
        public void ParseParam(EffCnf cnf)
        {
            /* bool result = int.TryParse(param, out rate);
             if (result)
                 rate = 100;*/
        }

        public string GetValueText()
        {
            return value_text;
        }

        public bool Random()
        {
            int result = (int)(UnityEngine.Random.value * 100);
            if (rate >= result) return true;
            return false;
        }
    }

}
