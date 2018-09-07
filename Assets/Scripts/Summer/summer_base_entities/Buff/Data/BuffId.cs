

namespace Summer
{
    public class BuffId
    {
        private static long _iidIndex = 1;
        public uint _charId;                    // 这个Buff 在哪个角色身上
        public long _iid;                       // buff的iid 唯一
        public int _buffId;                     // buff的Id

        public BuffId(uint charId, int buffId)
        {
            //LogManager.Assert(char_id != null, "caster_id ==null ,buff id is:" + buff_id);
            LogManager.Assert(buffId > 0, "buff id is error,id:" + buffId);

            _charId = charId;
            _buffId = buffId;
            _iid = _iidIndex;
            _iidIndex++;

        }

        public bool CheckById(int buffId)
        {
            if (buffId == _buffId) return true;
            return false;
        }

        public bool CheckByIid(BuffId bid)
        {
            return bid._iid == _iid;
        }
    }
}