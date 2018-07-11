
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System.Collections.Generic;
namespace Summer
{
    public class BaseEffectFactory
    {
        public virtual BaseEffect Create() { return null; }
    }

    public class EffectFactory1<T> : BaseEffectFactory where T : BaseEffect, new()
    {
        public override BaseEffect Create()
        {
            return new T();
        }
    }

    public class EffectFactoryManager
    {
        public static readonly EffectFactoryManager Instance = new EffectFactoryManager();
        public Dictionary<string, BaseEffectFactory> _factory_map = new Dictionary<string, BaseEffectFactory>();

        private EffectFactoryManager()
        {
            Init();
        }

        public BaseEffect CreateTrigger(string type)
        {
            BaseEffect ret = null;
            BaseEffectFactory factory = _find_factory(type);
            if (factory == null)
                return null;
            ret = factory.Create();
            return ret;
        }
        public void Init()
        {
            _reigster_factory("EffectAttribute", new EffectFactory1<EffectAttribute>());
        }

        #region private

        public void _reigster_factory(string name, BaseEffectFactory factory)
        {
            if (!_factory_map.ContainsKey(name))
            {
                _factory_map.Add(name, factory);
            }
        }

        public BaseEffectFactory _find_factory(string type)
        {
            BaseEffectFactory ret = null;
            _factory_map.TryGetValue(type, out ret);
            return ret;
        }

        #endregion
    }
}