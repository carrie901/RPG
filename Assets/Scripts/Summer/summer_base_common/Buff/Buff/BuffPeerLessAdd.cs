using UnityEngine;
using System.Collections;

namespace Summer
{
    public class BuffPeerLessAdd : BuffDataProcessValue
    {
        public override void Use()
        {
            base.Use();
            _peerless();
        }

        public void _peerless()
        {
           /* float current_peerless = (float)_target.peerLess.GetValue();

            float tmp = 0;
            // 1.得到无双值
            float origin = 0;
            if (_param.by_value == E_CharDataByValue.current)
                origin = (float)_target.peerLess.GetValue();
            else
                origin = _target.peerLess.GetMaxPeerLess();

            // 2.根据当前的无双值，计算实际的无双值
            BuffHelper.Calc(origin, ref tmp, _param);
            IntValueEventBuff int_value = new IntValueEventBuff();
            int_value.value = (int)tmp;
            // 3.触发无双值更新回调
            _target.RaiseEvent(E_BuffTrigger.on_update_peerless, int_value);

            LogManager.Log("无双值当前值:[{0}],变化[{1}]改变值:[{2}]", current_peerless, tmp, _target.peerLess.GetValue());*/
        }
    }

    public class BuffPeerLessRemove : BuffDataProcessValue
    {
        public override void Use()
        {
            base.Use();
            _peerless();
        }

        public void _peerless()
        {
            /*float current_peerless = (float)_target.peerLess.GetValue();

            float tmp = 0;
            // 1.得到无双值
            float origin = 0;
            if (_param.by_value == E_CharDataByValue.current)
                origin = (float)_target.peerLess.GetValue();
            else
                origin = _target.peerLess.GetMaxPeerLess();

            // 2.根据当前的无双值，计算实际的无双值
            BuffHelper.Calc(origin, ref tmp, _param);
            IntValueEventBuff int_value = new IntValueEventBuff();
            int_value.value = (int)tmp;
            // 3.触发无双值更新回调
            _target.RaiseEvent(E_BuffTrigger.on_update_peerless, int_value);

            LogManager.Log("无双值当前值:[{0}],变化[{1}]改变值:[{2}]", current_peerless, tmp, _target.peerLess.GetValue());*/
        }
    }
}
