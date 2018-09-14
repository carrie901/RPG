
using UnityEngine;

namespace Summer
{
    public class TransformPool : PoolManager
    {
        public const string NAME = "TransformPool";

        private static TransformPool _instance;

        protected Transform _goRootTrans;
        protected GameObject _goRoot;
        public static TransformPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransformPool();
                    if (_instance._goRoot == null)
                    {
                        _instance._goRoot = GameObjectHelper.CreateGameObject(NAME, false);
                        _instance._goRootTrans = _instance._goRoot.transform;
                    }
                    _instance.Init();
                }
                return _instance;
            }
        }

        public Transform FindTrans() { return _goRootTrans; }

        public override PoolBase GetDefaultFactory(string prefabName)
        {
            DefaultGameObjectFactory factory = new DefaultGameObjectFactory(prefabName);
            PoolBaseDefault poolBase = new PoolBaseDefault(factory);
            _map.Add(prefabName, poolBase);
            return poolBase;
        }
    }

}
