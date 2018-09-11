using System;
using System.Collections;
using System.Collections.Generic;

namespace Summer
{
    public class StaticCnf
    {
        private readonly static Dictionary<Type, IDictionary> _cnfMap = new Dictionary<Type, IDictionary>();

        public static Dictionary<int, T> FindMap<T>() where T : BaseCsv
        {
            Type type = typeof(T);

            if (!_cnfMap.ContainsKey(type))
            {
                LogManager.Error("not find: " + type);
                return null;
            }

            return _cnfMap[type] as Dictionary<int, T>;
        }

        public static T FindData<T>(int id) where T : BaseCsv
        {
            Type type = typeof(T);
            if (!_cnfMap.ContainsKey(type))
            {
                return null;
            }

            Dictionary<int, T> tmpMap = _cnfMap[type] as Dictionary<int, T>;
            if (tmpMap == null)
            {
                LogManager.Error(type.Name + "表结构有问题");
                return null;
            }
            if (!tmpMap.ContainsKey(id))
            {
                LogManager.Error(type.Name + "表中未找到Id为:" + id + "的行!");
                return null;
            }

            return tmpMap[id];
        }

        public static bool IsExists<T>(int id) where T : BaseCsv
        {

            Dictionary<int, T> dict = FindMap<T>();

            return dict.ContainsKey(id);
        }

        public static void Add<T>(Dictionary<int, T> cnf) where T : BaseCsv
        {
            Type type = typeof(T);
            _cnfMap.Add(type, cnf);
        }

        public static void Clear()
        {
            _cnfMap.Clear();
        }
    }
}

