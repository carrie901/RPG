using UnityEngine;

namespace Summer
{
    public class SoundGameObjectFactory : PoolObjectFactory
    {
        protected GameObject _prefab;
        public SoundGameObjectFactory(string name, GameObject prefab) : base(name)
        {
            _factory_name = name;
            _prefab = prefab;
        }
        public override I_PoolObjectAbility Create()
        {
            GameObject go = GameObjectHelper.Instantiate(_prefab);
            PoolDefaultGameObject po = go.GetComponent<PoolDefaultGameObject>();
            po.SetParent(SoundManager.instance.sound_parent.transform);
            return po;
        }

        public override void ExtraOpertion(I_PoolObjectAbility ability)
        {

        }
    }
}

