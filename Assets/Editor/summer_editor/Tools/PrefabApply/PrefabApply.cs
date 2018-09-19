
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
using UnityEngine;
using System;
using System.Reflection;
using Summer;
using UnityEditor;

namespace SummerEditor
{
    /// <summary>
    /// Copy 项目代码 可用作Apply GameObjet的时候自动检测
    /// 不支持Ctrl+S 
    /// </summary>
    public class PrefabApply : UnityEditor.AssetModificationProcessor
    {
        public static List<string> _checkPath = new List<string>()
        {
            "Assets/Prefabs/GUI/",
        };

        [InitializeOnLoadMethod]
        static void StartInitializeOnLoadMethod()
        {
            PrefabUtility.prefabInstanceUpdated = delegate (GameObject instance)
            {
                //EditorUtility.DisplayDialog("保存失败", assetPath, "OK");
                UnityEngine.Object prefabParent = PrefabUtility.GetPrefabParent(instance);
                string assetPath = AssetDatabase.GetAssetPath(prefabParent);
                bool check = FilterPath(assetPath);
                if (check)
                {
                    GameObject go = prefabParent as GameObject;
                    if (go == null) return;
                    MonoBehaviour[] monos = go.GetComponentsInChildren<MonoBehaviour>();
                    int length = monos.Length;
                    for (int i = 0; i < length; i++)
                    {

                        CheckGameObject(monos[i]);
                    }
                }
            };
        }

        public static void CheckGameObject(MonoBehaviour mono)
        {
            Type type = mono.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (FieldInfo field in fields)
            {
                UIChildAttribute uiChildAttr = (UIChildAttribute)Attribute.GetCustomAttribute(field, typeof(UIChildAttribute));
                //是否有特性
                if (uiChildAttr == null) continue;

                //参数类型
                Type fieldType = field.FieldType;
                //函数
                MethodInfo method;
                //是否是自身的脚本
                if (uiChildAttr.ObjectName == "this")
                {
                    if (fieldType == typeof(GameObject))
                    {
                        FindChild(mono.gameObject, mono.name);
                        continue;
                    }
                    method = GetSelfComponentFunction(fieldType);

                    object[] getComponentParams = { mono.gameObject };
                    method.Invoke(null, getComponentParams);
                }
                else if (fieldType == typeof(GameObject)) //是否是GameObject
                {
                    FindChild(mono.gameObject, uiChildAttr.ObjectName);
                }
                else
                {
                    //获取对应的类型
                    method = GetFindChildWithComponentFuncion(fieldType);
                    object[] findChildParams = { mono.gameObject, uiChildAttr.ObjectName };
                    method.Invoke(null, findChildParams);
                }
            }
            foreach (var field in fields)
            {
                //是否有特性
                UIListAttribute uiListAttr = (UIListAttribute)Attribute.GetCustomAttribute(field, typeof(UIListAttribute));
                if (uiListAttr == null) continue;

                //排除非List
                if (!field.FieldType.IsGenericType) continue;

                Type fieldType = field.FieldType.GetGenericArguments()[0];
                Type listType = typeof(List<>).MakeGenericType(fieldType);
                ConstructorInfo constructorInfo = listType.GetConstructor(Type.EmptyTypes);

                if (constructorInfo == null) continue;
                for (int i = uiListAttr.Begin; i <= uiListAttr.End; i++)
                {
                    //是否是GameObject
                    if (fieldType == typeof(GameObject))
                    {
                        FindChild(mono.gameObject, uiListAttr.NamePrefix + i);
                        continue;
                    }
                    //获取对应的类型
                    var method = GetFindChildWithComponentFuncion(fieldType);
                    object[] findChildParams = { mono.gameObject, uiListAttr.NamePrefix + i };
                    method.Invoke(null, findChildParams);
                }
            }
        }

        #region

        public static T GetSelfComponent<T>(GameObject self) where T : Component
        {
            T component = self.GetComponent<T>();
            if (component == null)
            {
                EditorUtility.DisplayDialog("Prefab和脚本不对应", self.name + "找不到类型为" + typeof(T).Name + "怒q脚本程序", "OK");
            }
            return component;
        }

        public static T FindChildWithComponent<T>(GameObject go, string childName) where T : Component
        {
            go = FindChild(go, childName);
            T component = go.GetComponent<T>();
            if (component == null)
            {
                EditorUtility.DisplayDialog("Prefab和脚本不对应", go.name + "找不到类型为" + typeof(T).Name + "怒q脚本程序", "OK");
            }
            return component;
        }

        private static GameObject FindChild(GameObject go, string childGoName, int index = 0)
        {
            if (go.name == childGoName)
            {
                return go;
            }
            else
            {
                index++;

                for (int i = 0; i < go.transform.childCount; i++)
                {
                    GameObject tmpObj = FindChild(go.transform.GetChild(i).gameObject, childGoName, index);

                    if (tmpObj != null)
                    {
                        return tmpObj;
                    }
                }

                if (index == 1)
                {
                    EditorUtility.DisplayDialog("Prefab和脚本不对应", "找不到名字为" + childGoName + "的GameObject" + "\n请先怒q脚本程序", "OK");
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region private 

        public static BindingFlags _bingdingflag = BindingFlags.Public | BindingFlags.Static;
        public static MethodInfo _findChildWithComponentFunction;
        public const string FIND_CHILD_WITH_COMPONENT = "FindChildWithComponent";
        public static MethodInfo GetFindChildWithComponentFuncion(Type fieldType)
        {
            if (_findChildWithComponentFunction == null)
            {
                _findChildWithComponentFunction = typeof(PrefabApply).GetMethod(FIND_CHILD_WITH_COMPONENT, _bingdingflag);
            }
            if (_findChildWithComponentFunction == null) return null;

            var function = _findChildWithComponentFunction.MakeGenericMethod(fieldType);
            return function;
        }


        public static MethodInfo _getSelfComponentFunction;
        public const string GET_SELF_COMPONENT = "GetSelfComponent";
        public static MethodInfo GetSelfComponentFunction(Type fieldType)
        {
            if (_getSelfComponentFunction == null)
            {
                _getSelfComponentFunction = typeof(PrefabApply).GetMethod(GET_SELF_COMPONENT, _bingdingflag);
            }
            if (_getSelfComponentFunction == null) return null;

            var function = _getSelfComponentFunction.MakeGenericMethod(fieldType);
            return function;
        }

        public static bool FilterPath(string path)
        {
            int length = _checkPath.Count;
            for (int i = 0; i < length; i++)
            {
                if (path.Contains(_checkPath[i]))
                    return true;
            }
            return false;
        }

        #endregion
    }
}