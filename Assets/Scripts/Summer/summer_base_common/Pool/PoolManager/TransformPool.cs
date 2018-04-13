
using UnityEngine;

namespace Summer
{
    public class TransformPool : PoolManager
    {
        private static TransformPool _instance;
        public const string NAME = "TransformPool";
        public static TransformPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransformPool();
                    if (_instance._go_root == null)
                    {
                        _instance._go_root = GameObjectHelper.CreateGameObject(NAME, false);
                        _instance._go_root_trans = _instance._go_root.transform;
                    }
                    _instance.Init();
                }
                return _instance;
            }
        }

        public Transform FindTrans() { return _go_root_trans; }

        public override PoolBase GetDefaultFactory(string prefab_name)
        {
            DefaultGameObjectFactory factory = new DefaultGameObjectFactory(prefab_name);
            PoolBaseDefault pool_base = new PoolBaseDefault(factory);
            _map.Add(prefab_name, pool_base);
            return pool_base;
        }
    }

}
