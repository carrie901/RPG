
namespace Summer
{
    public class BuffFactory
    {
        static BuffFactory()
        {

        }

        public static Buff Create(int buff_id)
        {
            Buff buff = null;
            BuffCnf buff_cnf = StaticCnf.FindData<BuffCnf>(buff_id);
            E_BUFF_TYPE type = (E_BUFF_TYPE)buff_cnf.sub_type;
            switch (type)
            {
                case E_BUFF_TYPE.data_updater:
                    buff = new BuffDataUpdater();
                    break;
                case E_BUFF_TYPE.data_multiple_updater:
                    buff = new BuffMultipleAttribute();
                    break;
                case E_BUFF_TYPE.bleeding:
                    buff = new BuffBleeding();
                    break;
                case E_BUFF_TYPE.blood:
                    buff = new BuffBlood();
                    break;
                case E_BUFF_TYPE.vampire:
                    buff = new BuffVampire();
                    break;
                case E_BUFF_TYPE.passive_damage_health:
                    buff = new BuffPassiveDamageHealth();
                    break;
                case E_BUFF_TYPE.passive_damage:
                    buff = new BuffPassiveDamage();
                    break;
                case E_BUFF_TYPE.invincible:
                    buff = new BuffInvincible();
                    break;
                case E_BUFF_TYPE.shield:
                    buff = new BuffShield();
                    break;
                case E_BUFF_TYPE.exchange:
                    buff = new BuffExchange();
                    break;
                case E_BUFF_TYPE.peerless_add:
                    buff = new BuffPeerLessAdd();
                    break;
                case E_BUFF_TYPE.peerless_reduce:
                    buff = new BuffPeerLessRemove();
                    break;
            }

            if (buff != null)
            {
                buff.Init(buff_cnf);
            }


            return buff;
        }

    }
}

