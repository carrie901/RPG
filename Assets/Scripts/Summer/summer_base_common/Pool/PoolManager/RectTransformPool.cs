using UnityEngine;

namespace Summer
{
    public class RectTransformPool : PoolManager
    {
        public const string NAME = "RectTransformPool";

        private static RectTransformPool _instance;

        public static RectTransformPool Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RectTransformPool();
                    if (_instance._go_root == null)
                    {
                        _instance._go_root = GameObjectHelper.CreateGameObject(NAME, true);
                        Canvas canvas = _instance._go_root.AddComponent<Canvas>();
                        _instance._go_root_trans = _instance._go_root.GetComponent<RectTransform>();
                        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    }
                    _instance.Init();
                }
                return _instance;
            }
        }

        public Transform FindTrans() { return _go_root_trans; }

        public override PoolBase GetDefaultFactory(string prefab_name)
        {
            DefaultCanvasGameObjectFactory factory = new DefaultCanvasGameObjectFactory(prefab_name);
            PoolBaseDefault pool_base = new PoolBaseDefault(factory);
            _map.Add(prefab_name, pool_base);
            return pool_base;
        }

    }
}

