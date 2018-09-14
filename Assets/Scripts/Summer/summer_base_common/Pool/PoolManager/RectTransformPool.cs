using UnityEngine;

namespace Summer
{
    public class RectTransformPool : PoolManager
    {
        public const string NAME = "RectTransformPool";

        private static RectTransformPool _instance;
        protected Transform _goRootTrans;
        protected GameObject _goRoot;
        public static RectTransformPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RectTransformPool();
                    if (_instance._goRoot == null)
                    {
                        _instance._goRoot = GameObjectHelper.CreateGameObject(NAME, true);
                        Canvas canvas = _instance._goRoot.AddComponent<Canvas>();
                        _instance._goRootTrans = _instance._goRoot.GetComponent<RectTransform>();
                        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    }
                    _instance.Init();
                }
                return _instance;
            }
        }

        public Transform FindTrans() { return _goRootTrans; }

        public override PoolBase GetDefaultFactory(string prefabName)
        {
            DefaultCanvasGameObjectFactory factory = new DefaultCanvasGameObjectFactory(prefabName);
            PoolBaseDefault poolBase = new PoolBaseDefault(factory);
            _map.Add(prefabName, poolBase);
            return poolBase;
        }

    }
}

